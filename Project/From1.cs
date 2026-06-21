using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Drawing.Drawing2D;

namespace Project
{
    public partial class From1 : Form
    {
        public From1()
        {
            InitializeComponent();
        }

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


        //password encryption
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




        private void RoundButton(Button btn)
        {
            GraphicsPath path = new GraphicsPath();
            int radius = 30;

            path.StartFigure();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(btn.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(btn.Width - radius, btn.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, btn.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();

            btn.Region = new Region(path);
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            RoundPanel(PanelLeft, 30);
            RoundPanel(PanelRight, 30);
            RoundButton(button1);

            txtPassword.BorderStyle = BorderStyle.None;
            CurveTextBoxCorners(txtPassword, 15);
            txtUserName.BorderStyle = BorderStyle.None;
            CurveTextBoxCorners(txtUserName, 15);

        }

        private void CurveTextBoxCorners(TextBox tb, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            Rectangle rect = new Rectangle(0, 0, tb.Width, tb.Height);
            int d = radius * 2;

            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90); path.CloseFigure();

            tb.Region = new Region(path);
        }

        private void PanelRight_Paint(object sender, PaintEventArgs e)
        {
            string user_name = txtUserName.Text.ToString();
            string user_password = txtPassword.Text.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both Username and Password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString = "Server=localhost;Port=3306;Database=school_ams;Uid=root;Pwd=;";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT UserRole, UserID FROM users WHERE Username = @User AND Password = @Pass";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        string hashedPassword = HashPassword(password);
                        cmd.Parameters.AddWithValue("@User", username);
                        cmd.Parameters.AddWithValue("@Pass", hashedPassword);


                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string userRole = reader["UserRole"].ToString();
                                int userId = Convert.ToInt32(reader["UserID"]);


                                Project.Teacher_profile.Session.LoggedInUsername = username;
                                Project.Teacher_profile.Session.LoggedInUserID = userId;
                                Project.Class_Teacher_Profile.Session.LoggedInUserID = userId;
                                Project.Class_Teacher_Profile.Session.LoggedInUsername = username;
                                Project.Admin_profile.Session.LoggedInUsername = username;
                                Project.Admin_profile.Session.LoggedInUserID=userId;

                                if (userRole == "Admin")
                                {
                                    AdminDashboard adminForm = new AdminDashboard();
                                    adminForm.Show();
                                    this.Hide();
                                }

                                else if (userRole == "ClassTeacher")
                                {
                                    Class_Teacher_Profile classteacherFrom = new Class_Teacher_Profile();
                                    classteacherFrom.Show();
                                    this.Hide();
                                }

                                else if (userRole == "Teacher")
                                {
                                    Teacher_profile teacherForm = new Teacher_profile();
                                    teacherForm.Show();
                                    this.Hide();
                                }
                                else if (userRole == "Pending")
                                {
                                    MessageBox.Show("Your account is pending approval.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Invalid Username or Password!", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message);
            }
        }

        private void register_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Register register = new Register();
            register.Show();
            this.Hide();
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {

        }
        // Show and hide password
        private void passwordHide_Click(object sender, EventArgs e)
        {

            txtPassword.UseSystemPasswordChar = true;
            passwordShow.Visible = true;
            passwordHide.Visible = false;
        }

        private void passwordShow_Click(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = false;
            passwordHide.Visible = true;
            passwordShow.Visible = false;
        }

        private void PanelLeft_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
