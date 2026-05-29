using System.Data.SqlClient;
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


            string connectionString = @"Server=LAPTOP-U0AVEUM3;Database=School_AMS;Integrated Security=True;";

            try
            {
                
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT UserRole FROM Users WHERE Username = @User AND Password = @Pass";

                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        string hashedPassword = HashPassword(password);
                        cmd.Parameters.AddWithValue("@User", username);
                        cmd.Parameters.AddWithValue("@Pass", hashedPassword);

                        object roleObj = cmd.ExecuteScalar();

                        if (roleObj != null)
                        {
                            string userRole = roleObj.ToString();

                            if (userRole == "Admin")
                            {
                                AdminDashboard adminForm = new AdminDashboard();
                                adminForm.Show();
                            }
                            else if (userRole == "Teacher")
                            {
                                TeacherDashboard teacherForm = new TeacherDashboard();
                                teacherForm.Show();
                            }
                            else if (userRole == "ClassTeacher")
                            {
                                ClassTeacherDashboard classTeacherForm = new ClassTeacherDashboard();
                                classTeacherForm.Show();
                            }
                            else if (userRole == "Pending")
                            {
                                MessageBox.Show("Your account is pending approval. Please contact the Principle.", "Account Pending", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                        }
                        else
                        {
                            MessageBox.Show("Invalid Username or Password!", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Connection Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
