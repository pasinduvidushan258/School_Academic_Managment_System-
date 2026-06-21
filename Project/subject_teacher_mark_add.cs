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
    public partial class subject_teacher_mark_add : Form
    {
        private string connectionString = "Server=localhost;Port=3307;Database=school_ams;Uid=root;Pwd=;";
        private int _classID;
        private int _term;
        private int _subjectID;
        private int _teacherID;
        public subject_teacher_mark_add(int classID, int term)
        {
            InitializeComponent();
            this._classID = classID;
            this._term = term;
            

            this._teacherID = Class_Teacher_Profile.Session.LoggedInUserID;

            ApplyCustomGridTheme();
            LoadMarksManagementGrid();
        }

        private void ApplyCustomGridTheme()
        {
            // Changes the white space background areas to match your dark-gray theme
            dataGridView_marksAdd.EnableHeadersVisualStyles = false;
            dataGridView_marksAdd.BackgroundColor = Color.FromArgb(128, 128, 128); // Gray background space

            // Style the header cells beautifully
            dataGridView_marksAdd.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(100, 100, 100);
            dataGridView_marksAdd.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView_marksAdd.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(100, 100, 100);
        }

        private void LoadMarksManagementGrid()
        {
            // Pulls the student data list and filters records strictly by the assigned SubjectID
            string query = @"
                SELECT 
                    s.StudentNo AS 'Admission No', 
                    CONCAT(s.FirstName, ' ', s.LastName) AS 'Student Name', 
                    MAX(em.Marks) AS 'Marks'
                FROM students s
                LEFT JOIN exam_marks em ON s.StudentNo = em.StudentNo 
                    AND em.ClassID = s.CurrentClassID 
                    AND em.Term = @Term 
                    AND em.SubjectID = @SubjectID
                WHERE s.CurrentClassID = @ClassID
                GROUP BY s.StudentNo, s.FirstName, s.LastName";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ClassID", _classID);
                        cmd.Parameters.AddWithValue("@Term", _term);
                        cmd.Parameters.AddWithValue("@SubjectID", _subjectID);

                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        // Clear read-only memory blocks from composite datasets
                        dt.Columns["Admission No"].ReadOnly = false;
                        dt.Columns["Student Name"].ReadOnly = false;
                        dt.Columns["Marks"].ReadOnly = false;

                        dataGridView_marksAdd.DataSource = dt;
                        dataGridView_marksAdd.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                        // Lock metadata details so user only changes the actual Marks column cells
                        dataGridView_marksAdd.Columns["Admission No"].ReadOnly = true;
                        dataGridView_marksAdd.Columns["Student Name"].ReadOnly = true;
                        dataGridView_marksAdd.Columns["Marks"].ReadOnly = false;

                        dataGridView_marksAdd.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erroe: " + ex.Message);
                }
            }
        }
        private void subject_teacher_mark_add_Load(object sender, EventArgs e)
        {

        }

        private void btnSaveMarks_Click(object sender, EventArgs e)
        {
            dataGridView_marksAdd.EndEdit();

            // Saves new marks row data or updates existing marks instantly if a database record already exists
            string upsertQuery = @"
                INSERT INTO exam_marks (StudentNo, ClassID, SubjectID, TeacherID, Term, Marks) 
                VALUES (@StudentNo, @ClassID, @SubjectID, @TeacherID, @Term, @Marks)
                ON DUPLICATE KEY UPDATE Marks = @Marks;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Iterate cleanly through every row edited inside your GridView panel
                    foreach (DataGridViewRow row in dataGridView_marksAdd.Rows)
                    {
                        if (row.Cells["Admission No"].Value == null) continue;

                        string studentNo = row.Cells["Admission No"].Value.ToString();
                        object marksValue = row.Cells["Marks"].Value;

                        // Skip rows where no marks have been typed yet
                        if (marksValue == null || string.IsNullOrWhiteSpace(marksValue.ToString()))
                            continue;

                        decimal marks = Convert.ToDecimal(marksValue);

                        // Validate to ensure marks stay within realistic grading thresholds
                        if (marks < 0 || marks > 100)
                        {
                            MessageBox.Show($"{studentNo} Not valid mark! (0 - 100)", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        using (MySqlCommand cmd = new MySqlCommand(upsertQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@StudentNo", studentNo);
                            cmd.Parameters.AddWithValue("@ClassID", _classID);
                            cmd.Parameters.AddWithValue("@SubjectID", _subjectID);
                            cmd.Parameters.AddWithValue("@TeacherID", _teacherID);
                            cmd.Parameters.AddWithValue("@Term", _term);
                            cmd.Parameters.AddWithValue("@Marks", marks);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Subject Marks added Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close(); // Close the input sheet and go back to dashboard view
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database save execution fail:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
