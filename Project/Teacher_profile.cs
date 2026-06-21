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
using BCrypt.Net;
using System.Security.Cryptography;

namespace Project
{

    public partial class Teacher_profile : Form
    {
        public static class Session
        {
            public static string LoggedInUsername = "";
            public static int LoggedInUserID = 0;
        }

        private string connectionString = "Server=localhost;Port=3306;Database=school_ams;Uid=root;Pwd=;";
        public Teacher_profile()
        {
            InitializeComponent();
            txtCurrentPassword.UseSystemPasswordChar = true;
            txtNewPassword.UseSystemPasswordChar = true;
            txtConfirmPassword.UseSystemPasswordChar = true;

            this.Load += new System.EventHandler(this.Teacher_profile_Load);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            TeacherDashboard teacherDashboard = new TeacherDashboard();
            teacherDashboard.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Subject_Marks subjectMarks = new Subject_Marks();
            subjectMarks.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Analytics analytics = new Analytics();
            analytics.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Top_Students topStudents = new Top_Students();
            topStudents.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            From1 loginForm = new From1();
            loginForm.Show();
            this.Close();
        }

        private void passwordShow_Click(object sender, EventArgs e)
        {
            txtCurrentPassword.UseSystemPasswordChar = false;
            passwordHide.Visible = true;
            passwordShow.Visible = false;
        }

        private void passwordHide_Click(object sender, EventArgs e)
        {
            txtCurrentPassword.UseSystemPasswordChar = true;
            passwordShow.Visible = true;
            passwordHide.Visible = false;
        }

        private void pictureBox35_Click(object sender, EventArgs e)
        {
            txtNewPassword.UseSystemPasswordChar = false;
            pictureBox35.Visible = false;
            pictureBox37.Visible = true;
        }

        private void pictureBox37_Click(object sender, EventArgs e)
        {
            txtNewPassword.UseSystemPasswordChar = true;
            pictureBox37.Visible = false;
            pictureBox35.Visible = true;
        }

        private void pictureBox36_Click(object sender, EventArgs e)
        {
            txtConfirmPassword.UseSystemPasswordChar = false;
            pictureBox36.Visible = false;
            pictureBox38.Visible = true;
        }

        private void pictureBox38_Click(object sender, EventArgs e)
        {
            txtConfirmPassword.UseSystemPasswordChar = true;
            pictureBox38.Visible = false;
            pictureBox36.Visible = true;
        }

        private void leftPanel_Paint(object sender, PaintEventArgs e)
        {

        }


        private void ClearFields()
        {
            txtCurrentPassword.Clear();
            txtNewPassword.Clear();
            txtConfirmPassword.Clear();
        }

        private void LoadAssignedClasses(int teacherId)
        {
            // Query linking your allocations, classes, and subjects tables together
            string query = @"SELECT c.Grade AS 'Grade', 
                            c.ClassName AS 'Class Name', 
                            s.SubjectName AS 'Subject' 
                     FROM teacher_allocations ta
                     INNER JOIN classes c ON ta.ClassID = c.ClassID
                     INNER JOIN subjects s ON ta.SubjectID = s.SubjectID
                     WHERE ta.TeacherID = @TeacherID";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TeacherID", teacherId);

                    try
                    {
                        conn.Open();

                        // Use MySqlDataAdapter to fill a DataTable directly
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            // Bind the data table data directly to your exact DataGridView name
                            dataGridViewAssignedClass.DataSource = dt;

                            // Visual formatting fixes for a clean UI
                            dataGridViewAssignedClass.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                            dataGridViewAssignedClass.AllowUserToAddRows = false;
                            dataGridViewAssignedClass.ReadOnly = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading assigned classes: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void LoadCurrentSubjects(int teacherId)
        {
            // Clear any default design-time items first
            listBoxCurrentSubjects.Items.Clear();

            // Query links allocations to subjects and ensures unique names using DISTINCT
            string query = @"SELECT DISTINCT s.SubjectName 
                     FROM teacher_allocations ta
                     INNER JOIN subjects s ON ta.SubjectID = s.SubjectID
                     WHERE ta.TeacherID = @TeacherID 
                     ORDER BY s.SubjectName ASC";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TeacherID", teacherId);

                    try
                    {
                        conn.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            bool hasSubjects = false;

                            while (reader.Read())
                            {
                                hasSubjects = true;
                                string subjectName = reader["SubjectName"].ToString();

                                // Add each subject string line-by-line into the ListBox
                                listBoxCurrentSubjects.Items.Add(subjectName);
                            }

                            // If the teacher has no allocations assigned yet
                            if (!hasSubjects)
                            {
                                listBoxCurrentSubjects.Items.Add("No assigned subjects found.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading teaching subjects: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void Teacher_profile_Load(object sender, EventArgs e)
        {
            string currentLoggedInUser = Session.LoggedInUsername;

            if (string.IsNullOrEmpty(currentLoggedInUser))
            {
                MessageBox.Show("No active user session found. Please log in again.", "Session Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string query = "SELECT UserID, FirstName, LastName, Email, MobileNumber, Gender, DOB FROM users WHERE Username = @Username";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", currentLoggedInUser);

                    try
                    {
                        conn.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int loggedInTeacherID = Convert.ToInt32(reader["UserID"]);
                                disTeacherId.Text = reader["UserID"].ToString();
                                string firstName = reader["FirstName"] != DBNull.Value ? reader["FirstName"].ToString() : "";
                                string lastName = reader["LastName"] != DBNull.Value ? reader["LastName"].ToString() : "";
                                string gender = reader["Gender"] != DBNull.Value ? reader["Gender"].ToString().Trim() : "";

                                string prefix = "";
                                if (gender.Equals("Male", StringComparison.OrdinalIgnoreCase))
                                {
                                    prefix = "Mr. ";
                                }
                                else if (gender.Equals("Female", StringComparison.OrdinalIgnoreCase))
                                {
                                    prefix = "Miss ";
                                }

                                Teacher_name.Text = $"{prefix}{firstName} {lastName}".Trim();

                                disFullName.Text = $"{firstName} {lastName}".Trim();
                                disEmail.Text = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : "N/A";
                                disMobile.Text = reader["MobileNumber"] != DBNull.Value ? reader["MobileNumber"].ToString() : "N/A";
                                disGender.Text = reader["Gender"] != DBNull.Value ? reader["Gender"].ToString() : "N/A";
                                disGender.Text = string.IsNullOrEmpty(gender) ? "N/A" : gender;

                                if (reader["DOB"] != DBNull.Value)
                                {
                                    DateTime dob = Convert.ToDateTime(reader["DOB"]);
                                    disDOB.Text = dob.ToString("yyyy-MM-dd");
                                }
                                else
                                {
                                    disDOB.Text = "N/A";
                                }

                                LoadAssignedClasses(loggedInTeacherID);
                                LoadCurrentSubjects(loggedInTeacherID);
                            }
                            else
                            {
                                MessageBox.Show("Profile details could not be found in the database.", "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred while loading profile information: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnUpdatePassword_Click(object sender, EventArgs e)
        {
            string currentPassword = txtCurrentPassword.Text.Trim();
            string newPassword = txtNewPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();

            if (string.IsNullOrEmpty(currentPassword) ||
                string.IsNullOrEmpty(newPassword) ||
                string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("The new password and confirmation password do not match.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (currentPassword == newPassword)
            {
                MessageBox.Show("Your new password cannot be the same as your current password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string currentLoggedInUser = Session.LoggedInUsername;

            try
            {
                if (VerifyCurrentPassword(currentLoggedInUser, currentPassword))
                {
                    // STEP 2: Save the new hashed password
                    SaveNewPasswordToDatabase(currentLoggedInUser, newPassword);

                    MessageBox.Show("Password updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("The current password you entered is incorrect.", "Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An database error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool VerifyCurrentPassword(string username, string inputPassword)
        {
            string query = "SELECT Password FROM users WHERE Username = @Username";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    conn.Open();

                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        string storedHash = result.ToString().Trim();

                        // 1. Hash the user's input password using SHA-256
                        string inputHash = ComputeSHA256Hash(inputPassword);

                        // 2. Compare the two hashes (ignoring case differences)
                        return string.Equals(storedHash, inputHash, StringComparison.OrdinalIgnoreCase);
                    }
                }
            }
            return false;
        }

        private string ComputeSHA256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash returns a byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert the byte array to a lowercase hex string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void SaveNewPasswordToDatabase(string username, string newPassword)
        {
            // Convert the new password to SHA-256 format to match your table format
            string secureHash = ComputeSHA256Hash(newPassword);

            string query = "UPDATE users SET Password = @NewPassword WHERE Username = @Username";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@NewPassword", secureHash);
                    cmd.Parameters.AddWithValue("@Username", username);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
