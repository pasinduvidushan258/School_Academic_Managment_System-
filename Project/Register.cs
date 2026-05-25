using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
//password encryption
using System.Security.Cryptography;
using System.Text;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        // Method to hash the password using SHA256
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
        // Method to round the corners of a panel
        private void RoundPanel(Panel pnl, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();

            // curve for each corner
            path.AddArc(0, 0, radius, radius, 180, 90); // left-top
            path.AddArc(pnl.Width - radius, 0, radius, radius, 270, 90); // right-top
            path.AddArc(pnl.Width - radius, pnl.Height - radius, radius, radius, 0, 90); // right-bottom
            path.AddArc(0, pnl.Height - radius, radius, radius, 90, 90); // left-bottom

            path.CloseFigure();
            pnl.Region = new Region(path);
        }


        private void buttonRegister_Click(object sender, EventArgs e)
        {
            // Validate input fields
            string fName = txtFirstName.Text.Trim();
            string lName = txtLastName.Text.Trim();
            string username = txtUserName.Text.Trim();
            string password = txtPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();
            string mobile = txtMobileNumber.Text.Trim();
            string email = txtEmail.Text.Trim();

            // check the radio button in gender wise
            string gender = "";
            if (Male.Checked) { gender = "Male"; }
            else if (Female.Checked) { gender = "Female"; }

            string dob = dateOfBirthday.Value.ToString("yyyy-MM-dd");

            string role = "Pending";

            if (string.IsNullOrEmpty(fName) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(gender))
            {
                MessageBox.Show("Please fill all required fields.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string connectionString = "Server=localhost;Database=school_ams;Uid=root;Pwd=1234;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();


                    string checkUserQuery = "SELECT COUNT(*) FROM users WHERE Username = @User";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkUserQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@User", username);
                        int userExists = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (userExists > 0)
                        {
                            MessageBox.Show("This Username is already taken! Please choose another one.", "Username Exists", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }


                    string hashedPassword = HashPassword(password);


                    string insertQuery = "INSERT INTO users (FirstName, LastName, Email, Username, Password, MobileNumber, Gender, DOB, UserRole) " +
                                         "VALUES (@FName, @LName, @Email, @User, @Pass, @Mobile, @Gender, @DOB, @Role)";

                    using (MySqlCommand cmd = new MySqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@FName", fName);
                        cmd.Parameters.AddWithValue("@LName", lName);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@User", username);
                        cmd.Parameters.AddWithValue("@Pass", hashedPassword);
                        cmd.Parameters.AddWithValue("@Mobile", mobile);
                        cmd.Parameters.AddWithValue("@Gender", gender);
                        cmd.Parameters.AddWithValue("@DOB", dob);
                        cmd.Parameters.AddWithValue("@Role", role);

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Registration successful! Waiting for Admin approval.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


                            From1 loginForm = new From1();
                            loginForm.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Registration failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            From1 loginForm = new From1();
            loginForm.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtMobileNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void panelTop_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Register_Load(object sender, EventArgs e)
        {
            RoundPanel(panelTop, 30);
            RoundPanel(panelBottom, 30);
        }

        // Show and hide password

        private void passwordHide_Click_1(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
            passwordShow.Visible = true;
            passwordHide.Visible = false;
        }

        private void passwordShow_Click_1(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = false;
            passwordHide.Visible = true;
            passwordShow.Visible = false;
        }

        private void passwordShow1_Click(object sender, EventArgs e)
        {
            txtConfirmPassword.UseSystemPasswordChar = false;
            passwordHide1.Visible = true;
            passwordShow1.Visible = false;
        }

        private void passwordHide1_Click(object sender, EventArgs e)
        {
            txtConfirmPassword.UseSystemPasswordChar = true;
            passwordShow1.Visible = true;
            passwordHide1.Visible = false;
        }
    }
}
