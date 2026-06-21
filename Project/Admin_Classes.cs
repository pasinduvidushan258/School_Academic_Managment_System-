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
    public partial class Admin_Classes : Form
    {
        // Database Connection String
        string connectionString = "Server=localhost;Port=3307;Database=school_ams;Uid=root;Pwd=;";

        // A variable to remember the row being edited
        private int editIndex = -1;

        public Admin_Classes()
        {
            InitializeComponent();

            // First, we make the DataGridView non-editable.
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AdminDashboard adminDashboard = new AdminDashboard();
            adminDashboard.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Admin_profile Adminprofile = new Admin_profile();
            Adminprofile.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Admin_Teacher admin_Teacher = new Admin_Teacher();
            admin_Teacher.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Admin_student admin_student = new Admin_student();
            admin_student.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Admin_Classes admin_Classes = new Admin_Classes();
            admin_Classes.Show();
            this.Hide();
        }

        private void Class_Analytics_Click(object sender, EventArgs e)
        {

        }

        private void Discipline_Click(object sender, EventArgs e)
        {
            
        }

        private void Extra_Curricular_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            From1 from1 = new From1();
            from1.Show();
            this.Hide();
        }

        private void Class_Analytics_Click_1(object sender, EventArgs e)
        {
            
        }

        private void grade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (grade.SelectedItem != null)
            {
                LoadClasses(grade.SelectedItem.ToString());
            }
        }

        private void Admin_Classes_Load(object sender, EventArgs e)
        {
            LoadFilterGrades();
            LoadInputGrades();
            LoadTeachersToComboBox();
            GenerateNextClassID();
            // First, call "All" to show all classes
            LoadClasses("All");
        }

        
        private void LoadFilterGrades()
        {
            grade.Items.Clear();
            grade.Items.Add("All");
            for (int i = 6; i <= 13; i++)
            {
                grade.Items.Add(i.ToString());
            }
            grade.SelectedIndex = 0;
        }

        
        private void LoadInputGrades()
        {
            comboBox_Grade.Items.Clear();
            comboBox_Grade.Items.Add("Select Grade");
            for (int i = 6; i <= 13; i++)
            {
                comboBox_Grade.Items.Add(i.ToString());
            }
            comboBox_Grade.SelectedIndex = 0;
        }

        
        private void LoadTeachersToComboBox()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    // Fetch only those with the role ClassTeacher or Teacher
                    string query = "SELECT UserID, CONCAT(Username, ' - ', Email) AS TeacherInfo FROM users WHERE UserRole IN ('Teacher', 'ClassTeacher') ORDER BY Username ASC";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        DataRow row = dt.NewRow();
                        row["UserID"] = 0;
                        row["TeacherInfo"] = "Select Teacher";
                        dt.Rows.InsertAt(row, 0);

                        comboBoxClass_Teacher.DataSource = dt;
                        comboBoxClass_Teacher.DisplayMember = "TeacherInfo";
                        comboBoxClass_Teacher.ValueMember = "UserID"; 
                        comboBoxClass_Teacher.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error loading teachers: " + ex.Message); }
        }

        
        private void GenerateNextClassID()
        {
            Class_ID.ReadOnly = true; 
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT MAX(ClassID) FROM classes";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        int nextId = 1;

                        if (result != DBNull.Value && result != null)
                        {
                            nextId = Convert.ToInt32(result) + 1; 
                        }
                        Class_ID.Text = nextId.ToString();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error generating Class ID: " + ex.Message); }
        }



        
        private void LoadClasses(string gradeFilter)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    
                    string query = @"
                SELECT c.ClassID, c.Grade, c.ClassName, IFNULL(u.Username, 'Not Assigned') AS 'ClassTeacher' 
                FROM classes c
                LEFT JOIN users u ON c.ClassTeacherID = u.UserID";

                    
                    if (gradeFilter != "All" && !string.IsNullOrEmpty(gradeFilter))
                    {
                        query += " WHERE c.Grade = @grade";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        if (gradeFilter != "All" && !string.IsNullOrEmpty(gradeFilter))
                        {
                            cmd.Parameters.AddWithValue("@grade", gradeFilter);
                        }

                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        dataGridView1.Columns.Clear();
                        dataGridView1.DataSource = dt;

                        // Stop manually changing columns (Lock)
                        dataGridView1.Columns["ClassID"].ReadOnly = true;
                        dataGridView1.Columns["ClassTeacher"].ReadOnly = true;

                        // Adding the Edit button
                        DataGridViewLinkColumn editCol = new DataGridViewLinkColumn();
                        editCol.Name = "EditAction";
                        editCol.HeaderText = "Actions";
                        editCol.Text = "Edit";
                        editCol.UseColumnTextForLinkValue = true;
                        editCol.LinkColor = Color.DodgerBlue;
                        dataGridView1.Columns.Add(editCol);

                        // Adding the Delete (X) button
                        DataGridViewLinkColumn delCol = new DataGridViewLinkColumn();
                        delCol.Name = "DeleteAction";
                        delCol.HeaderText = "";
                        delCol.Text = "X";
                        delCol.UseColumnTextForLinkValue = true;
                        delCol.LinkColor = Color.Red;
                        dataGridView1.Columns.Add(delCol);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error loading classes: " + ex.Message); }
        }

        private void save_Click(object sender, EventArgs e)
        {
            
            if (comboBox_Grade.SelectedIndex <= 0 || string.IsNullOrWhiteSpace(Class_Name.Text))
            {
                MessageBox.Show("Please,Entwr the class name and Grade", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string insertQuery = "INSERT INTO classes (Grade, ClassName, ClassTeacherID) VALUES (@grade, @cName, @teacherId)";

                    using (MySqlCommand cmd = new MySqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@grade", comboBox_Grade.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@cName", Class_Name.Text.Trim());

                        // If no teacher is selected (Select Teacher is called), NULL is returned.
                        if (comboBoxClass_Teacher.SelectedIndex <= 0)
                            cmd.Parameters.AddWithValue("@teacherId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@teacherId", Convert.ToInt32(comboBoxClass_Teacher.SelectedValue));

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Class successfully entered.!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                      
                        Class_Name.Clear();
                        comboBox_Grade.SelectedIndex = 0;
                        comboBoxClass_Teacher.SelectedIndex = 0;

                      
                        GenerateNextClassID();
                        string currentFilter = grade.SelectedItem?.ToString() ?? "All";
                        LoadClasses(currentFilter);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error saving class: " + ex.Message); }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string classId = dataGridView1.Rows[e.RowIndex].Cells["ClassID"].Value.ToString();

            
            if (dataGridView1.Columns[e.ColumnIndex].Name == "DeleteAction")
            {
                if (MessageBox.Show("Are you sure you want to delete this class? This may damage the data associated with it.", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        using (MySqlConnection conn = new MySqlConnection(connectionString))
                        {
                            conn.Open();
                            // To avoid Database Error, other data related to the class must be updated/deleted first
                            new MySqlCommand("UPDATE students SET CurrentClassID = NULL WHERE CurrentClassID = " + classId, conn).ExecuteNonQuery();
                            new MySqlCommand("DELETE FROM exam_marks WHERE ClassID = " + classId, conn).ExecuteNonQuery();

                            // Delete the class
                            new MySqlCommand("DELETE FROM classes WHERE ClassID = " + classId, conn).ExecuteNonQuery();

                            MessageBox.Show("Succesfully Deleted!");
                            string currentFilter = grade.SelectedItem?.ToString() ?? "All";
                            LoadClasses(currentFilter);
                        }
                    }
                    catch (Exception ex) { MessageBox.Show("Error deleting: " + ex.Message); }
                }
            }

           
            else if (dataGridView1.Columns[e.ColumnIndex].Name == "EditAction")
            {
                DataGridViewLinkCell linkCell = (DataGridViewLinkCell)dataGridView1.Rows[e.RowIndex].Cells["EditAction"];

                if (linkCell.Value.ToString() == "Edit")
                {
                   
                    dataGridView1.ReadOnly = false;

                    
                    if (editIndex != -1 && editIndex != e.RowIndex)
                    {
                        dataGridView1.Rows[editIndex].ReadOnly = true;
                        ((DataGridViewLinkCell)dataGridView1.Rows[editIndex].Cells["EditAction"]).Value = "Edit";
                    }

                    
                    dataGridView1.Rows[e.RowIndex].ReadOnly = false;

                    dataGridView1.Rows[e.RowIndex].Cells["ClassID"].ReadOnly = true;
                    dataGridView1.Rows[e.RowIndex].Cells["ClassTeacher"].ReadOnly = true;

                    linkCell.Value = "Save";
                    editIndex = e.RowIndex;
                }
                else if (linkCell.Value.ToString() == "Save")
                {
                    try
                    {
                        string cGrade = dataGridView1.Rows[e.RowIndex].Cells["Grade"].Value?.ToString();
                        string cName = dataGridView1.Rows[e.RowIndex].Cells["ClassName"].Value?.ToString();

                        using (MySqlConnection conn = new MySqlConnection(connectionString))
                        {
                            conn.Open();
                            string updateQuery = "UPDATE classes SET Grade = @grade, ClassName = @cName WHERE ClassID = @cId";
                            using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@grade", cGrade);
                                cmd.Parameters.AddWithValue("@cName", cName);
                                cmd.Parameters.AddWithValue("@cId", classId);
                                cmd.ExecuteNonQuery();

                                MessageBox.Show("Updated successfully!");
                            }
                        }

                        // Re-locking
                        dataGridView1.ReadOnly = true;
                        linkCell.Value = "Edit";
                        editIndex = -1;
                    }
                    catch (Exception ex) { MessageBox.Show("Update Error: " + ex.Message); }
                }
            }
        }
    }
}
