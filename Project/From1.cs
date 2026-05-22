using MySql.Data.MySqlClient;

namespace Project
{
    public partial class From1 : Form
    {
        public From1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

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


            string connectionString = "Server=localhost;Database=School_AMS;Uid=root;Pwd=1234;";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT UserRole FROM Users WHERE Username = @User AND Password = @Pass";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@User", username);
                        cmd.Parameters.AddWithValue("@Pass", password);

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

                            this.Hide();
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
    }
}
