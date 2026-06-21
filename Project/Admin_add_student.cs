using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data;

namespace Project
{


    public partial class Admin_add_student : Form
    {
        // Database Connection String
        string connectionString = "Server=localhost;Port=3307;Database=school_ams;Uid=root;Pwd=;";
        public Admin_add_student()
        {
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Admin_add_student_Load(object sender, EventArgs e)
        {
            // 1. Making the Student No Textbox Read-Only (so that typing is not possible)
            Student_No.ReadOnly = true;

            // 2. Find the new Student No and put it in the TextBox
            GenerateNextStudentNo();

            // 3. Bringing Class Names into the Dropdown
            LoadClasses();
        }

        private void GenerateNextStudentNo()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    // Take the largest number in the students table 
                    string query = "SELECT MAX(CAST(SUBSTRING(StudentNo, 5) AS UNSIGNED)) FROM students";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        int nextNumber = 11001; 

                        if (result != DBNull.Value && result != null)
                        {
                            nextNumber = Convert.ToInt32(result) + 1; 
                        }

                        // It is combined with STU and displayed in the TextBox.
                        Student_No.Text = "STU-" + nextNumber.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating Student No: " + ex.Message);
            }
        }


        private void LoadClasses()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                   
                    string query = "SELECT ClassID, ClassName FROM classes ORDER BY Grade ASC, ClassName ASC";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        class_name.DataSource = dt;
                        class_name.DisplayMember = "ClassName"; 
                        class_name.ValueMember = "ClassID";    
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading classes: " + ex.Message);
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(First_name.Text) || class_name.SelectedValue == null)
            {
                MessageBox.Show("Please enter all required data.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                   
                    string insertQuery = @"
                        INSERT INTO students (StudentNo, FirstName, LastName, DOB, Gender, Address, GuardianName, GuardianContact, CurrentClassID)
                        VALUES (@stuNo, @fName, @lName, @dob, @gender, @address, @gName, @gContact, @classId)";

                    using (MySqlCommand cmd = new MySqlCommand(insertQuery, conn))
                    {
                        // Give the parameters the names in your TextBoxes.
                        cmd.Parameters.AddWithValue("@stuNo", Student_No.Text);
                        cmd.Parameters.AddWithValue("@fName", First_name.Text);
                        cmd.Parameters.AddWithValue("@lName", textBox2.Text); 

                      
                        cmd.Parameters.AddWithValue("@dob", DOB.Text);

                        cmd.Parameters.AddWithValue("@gender", gender.Text);
                        cmd.Parameters.AddWithValue("@address", address.Text);
                        cmd.Parameters.AddWithValue("@gName", guardian_name.Text);
                        cmd.Parameters.AddWithValue("@gContact", guardian_contact.Text);

                        
                        cmd.Parameters.AddWithValue("@classId", Convert.ToInt32(class_name.SelectedValue));

                        cmd.ExecuteNonQuery(); 

                        MessageBox.Show("Student successfully enrolled!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                      
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving data: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

