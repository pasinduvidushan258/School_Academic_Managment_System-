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
    public partial class AdminDashboard : Form
    {
        string connectionString = "Server=localhost;Port=3307;Database=school_ams;Uid=root;Pwd=;";

        public AdminDashboard()
        {
            InitializeComponent();

            
            this.Load += AdminDashboard_Load;
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

        private void leftPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AdminDashboard_Load(object sender, EventArgs e)
        {
            LoadDashboardStats();
        }


        private void LoadDashboardStats()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    Classes.Text = GetCount(conn, "SELECT COUNT(*) FROM classes");
                    Student.Text = GetCount(conn, "SELECT COUNT(*) FROM students");
                    label9.Text = GetCount(conn, "SELECT COUNT(*) FROM users WHERE LOWER(UserRole) IN ('teacher', 'classteacher')");

                    
                    label10.Text = GetCount(conn, "SELECT COUNT(*) FROM subjects");

                    Admins.Text = GetCount(conn, "SELECT COUNT(*) FROM users WHERE LOWER(UserRole) = 'admin'");
                    Class_Teachers.Text = GetCount(conn, "SELECT COUNT(*) FROM users WHERE LOWER(UserRole) = 'classteacher'");
                    Subject_Teachers.Text = GetCount(conn, "SELECT COUNT(*) FROM users WHERE LOWER(UserRole) = 'teacher'");
                    pending_Approval.Text = GetCount(conn, "SELECT COUNT(*) FROM users WHERE LOWER(UserRole) = 'pending'");

                    
                    string qGrade6to9 = "SELECT COUNT(*) FROM students s JOIN classes c ON s.CurrentClassID = c.ClassID WHERE CAST(c.Grade AS UNSIGNED) BETWEEN 6 AND 9";
                    label18.Text = GetCount(conn, qGrade6to9);

                    string qGrade10to11 = "SELECT COUNT(*) FROM students s JOIN classes c ON s.CurrentClassID = c.ClassID WHERE CAST(c.Grade AS UNSIGNED) BETWEEN 10 AND 11";
                    label19.Text = GetCount(conn, qGrade10to11);

                    string qGrade12to13 = "SELECT COUNT(*) FROM students s JOIN classes c ON s.CurrentClassID = c.ClassID WHERE CAST(c.Grade AS UNSIGNED) BETWEEN 12 AND 13";
                    label20.Text = GetCount(conn, qGrade12to13);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading dashboard stats: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetCount(MySqlConnection conn, string query)
        {
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                    {
                        return result.ToString();
                    }
                }
            }
            catch
            {
               
            }
            return "0";
        }
    }
}
