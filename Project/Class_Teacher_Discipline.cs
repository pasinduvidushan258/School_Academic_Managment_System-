using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class Class_Teacher_Discipline : Form
    {
        private string connectionString = "Server=localhost;Port=3307;Database=school_ams;Uid=root;Pwd=;";
        private DataTable disciplineTable = new DataTable();

        public Class_Teacher_Discipline()
        {
            InitializeComponent();

            dgvDiscipline.AutoGenerateColumns = false;

            // ALLOW FULL GRID EDITING: Unlocks individual cell interaction triggers
            dgvDiscipline.ReadOnly = false;
            dgvDiscipline.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;

            LoadAllDisciplineRecords();
        }

        private void LoadAllDisciplineRecords(string searchKey = "")
        {
            // SQL query maps explicitly to DataPropertyNames
            string query = @"
                SELECT 
                    d.DisciplineID,
                    d.StudentNo AS 'Student Number', 
                    CONCAT(s.FirstName, ' ', s.LastName) AS 'Student Name', 
                    d.DisciplineDescription AS 'Discipline'
                FROM student_discipline d
                INNER JOIN students s ON d.StudentNo = s.StudentNo
                WHERE d.IsCancelled = 0";

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                query += " AND d.StudentNo LIKE @SearchKey";
            }

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        if (!string.IsNullOrWhiteSpace(searchKey))
                        {
                            cmd.Parameters.AddWithValue("@SearchKey", "%" + searchKey.Trim() + "%");
                        }

                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        // Unlock datatable column fields in internal memory
                        foreach (DataColumn col in dt.Columns)
                        {
                            col.ReadOnly = false;
                        }

                        dgvDiscipline.DataSource = dt;

                        // Explicit cell locks configuration at the Grid UI Level
                        if (dgvDiscipline.Columns.Contains("colStudentNo")) dgvDiscipline.Columns["colStudentNo"].ReadOnly = false;
                        if (dgvDiscipline.Columns.Contains("colStudentName")) dgvDiscipline.Columns["colStudentName"].ReadOnly = false;
                        if (dgvDiscipline.Columns.Contains("colDiscipline")) dgvDiscipline.Columns["colDiscipline"].ReadOnly = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error displaying discipline grid list: " + ex.Message, "Error View", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSaveAll_Click(object sender, EventArgs e)
        {
            // Commit active cell modifications before starting database execution loop
            dgvDiscipline.EndEdit();

            // Handles adding a new or updating an existing discipline tracking description row
            string upsertDisciplineQuery = @"
                INSERT INTO student_discipline (DisciplineID, StudentNo, DisciplineDescription, IsCancelled)
                VALUES (@DisciplineID, @StudentNo, @Description, 0)
                ON DUPLICATE KEY UPDATE DisciplineDescription = @Description;";

            // Updates student metadata profile names on the master record table
            string updateStudentQuery = @"
                UPDATE students 
                SET FirstName = @FirstName, LastName = @LastName 
                WHERE StudentNo = @StudentNo;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    int modifiedRowsCount = 0;

                    foreach (DataGridViewRow row in dgvDiscipline.Rows)
                    {
                        // Skip completely empty grid template rows at the bottom
                        if (row.Cells["colStudentNo"].Value == null || string.IsNullOrWhiteSpace(row.Cells["colStudentNo"].Value.ToString()))
                            continue;

                        // Safely parse cell entries
                        string studentNo = row.Cells["colStudentNo"].Value.ToString().Trim();
                        string fullName = row.Cells["colStudentName"].Value?.ToString().Trim() ?? "";
                        string disciplineDescription = row.Cells["colDiscipline"].Value?.ToString().Trim() ?? "";

                        // UI Validation: Block incomplete rows from executing queries
                        if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(disciplineDescription))
                        {
                            MessageBox.Show($"Student Name and Discipline description text fields cannot be empty for ID: {studentNo}", "Validation Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // Split full name string safely into DB FirstName and LastName fields
                        string firstName = fullName;
                        string lastName = "";
                        int spaceIndex = fullName.IndexOf(' ');
                        if (spaceIndex > 0)
                        {
                            firstName = fullName.Substring(0, spaceIndex);
                            lastName = fullName.Substring(spaceIndex + 1);
                        }

                        // Verify hidden tracking primary keys
                        object dbDisciplineID = DBNull.Value;
                        if (dgvDiscipline.Columns.Contains("DisciplineID") && row.Cells["DisciplineID"]?.Value != null && row.Cells["DisciplineID"].Value != DBNull.Value)
                        {
                            int id = Convert.ToInt32(row.Cells["DisciplineID"].Value);
                            if (id > 0) dbDisciplineID = id;
                        }

                        // A. Synchronize updated student name profiles directly
                        using (MySqlCommand cmdStudent = new MySqlCommand(updateStudentQuery, conn))
                        {
                            cmdStudent.Parameters.AddWithValue("@StudentNo", studentNo);
                            cmdStudent.Parameters.AddWithValue("@FirstName", firstName);
                            cmdStudent.Parameters.AddWithValue("@LastName", lastName);
                            cmdStudent.ExecuteNonQuery();
                        }

                        // B. Save or edit discipline history records safely
                        using (MySqlCommand cmdDiscipline = new MySqlCommand(upsertDisciplineQuery, conn))
                        {
                            cmdDiscipline.Parameters.AddWithValue("@DisciplineID", dbDisciplineID);
                            cmdDiscipline.Parameters.AddWithValue("@StudentNo", studentNo);
                            cmdDiscipline.Parameters.AddWithValue("@Description", disciplineDescription);
                            cmdDiscipline.ExecuteNonQuery();
                        }

                        modifiedRowsCount++;
                    }

                    if (modifiedRowsCount > 0)
                    {
                        MessageBox.Show("Student entries and discipline history updated and saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadAllDisciplineRecords(txtSearchStudentNo.Text);
                    }
                    else
                    {
                        MessageBox.Show("No active tracking changes found to update.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database save execution failure:\n" + ex.Message, "Error Database Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvDiscipline_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvDiscipline.Columns[e.ColumnIndex].Name == "colCancel")
            {
                // Grab dataset values safely from Bound Items mapping configurations
                DataRowView currentRow = (DataRowView)dgvDiscipline.Rows[e.RowIndex].DataBoundItem;

                if (currentRow == null) return;

                int disciplineID = Convert.ToInt32(currentRow["DisciplineID"]);
                string studentNum = currentRow["Student Number"].ToString();

                DialogResult result = MessageBox.Show($"Are you sure you want to cancel the discipline record for Student: {studentNum}?",
                    "Confirm Cancellation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    CancelDisciplineRecord(disciplineID);
                }
            }
        }

        private void CancelDisciplineRecord(int disciplineID)
        {
            string updateQuery = "UPDATE student_discipline SET IsCancelled = 1 WHERE DisciplineID = @DisciplineID";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@DisciplineID", disciplineID);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Discipline entry has been successfully cancelled.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAllDisciplineRecords(txtSearchStudentNo.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to cancel record: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadAllDisciplineRecords(txtSearchStudentNo.Text);
        }

        // --- Navigation Menu Routing Events ---
        private void button5_Click(object sender, EventArgs e)
        {
            Class_Teacher_Profile classTeacherProfile = new Class_Teacher_Profile();
            classTeacherProfile.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Class_Teacher_Subject_marks classTeacherSubjectMarks = new Class_Teacher_Subject_marks();
            classTeacherSubjectMarks.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Class_Teacher_Analytics classTeacherAnalytics = new Class_Teacher_Analytics();
            classTeacherAnalytics.Show();
            this.Hide();
        }

        private void Class_Analytics_Click(object sender, EventArgs e)
        {
            Class_Teacher_Class_Analytics classTeacherAnalytics = new Class_Teacher_Class_Analytics();
            classTeacherAnalytics.Show();
            this.Hide();
        }

        private void Discipline_Click(object sender, EventArgs e)
        {
            Class_Teacher_Discipline classTeacherDiscipline = new Class_Teacher_Discipline();
            classTeacherDiscipline.Show();
            this.Hide();
        }

        private void Extra_Curricular_Click(object sender, EventArgs e)
        {
            Class_Teacher_Extra_curricular classTeacherExtraCurricular = new Class_Teacher_Extra_curricular();
            classTeacherExtraCurricular.Show();
            this.Hide();
        }

        private void Class_Top_Students_Click(object sender, EventArgs e)
        {
            Class_Teacher_Class_Top_Students classTeacherClassTopStudents = new Class_Teacher_Class_Top_Students();
            classTeacherClassTopStudents.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ClassTeacherDashboard classTeacherDashboard = new ClassTeacherDashboard();
            classTeacherDashboard.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Class_Teacher_Top_Students classTeacherTopStudents = new Class_Teacher_Top_Students();
            classTeacherTopStudents.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            From1 from1 = new From1();
            from1.Show();
            this.Hide();
        }

        private void leftPanel_Paint(object sender, PaintEventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }

        private void txtSearchStudentNo_TextChanged(object sender, EventArgs e)
        {

        }
    }
}