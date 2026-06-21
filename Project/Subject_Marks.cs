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

namespace Project
{
    public partial class Subject_Marks : Form
    {
        string connectionString = "Server=localhost;Port=3306;Database=school_ams;Uid=root;Pwd=;";
        public Subject_Marks()
        {
            InitializeComponent();

            // 1. Dropdown nalli type maduvudannu tadeyalu (Locking)
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;

            // 2. Form open aadavaga data direct aagi load maduvudu
            LoadClasses();
            LoadTerms();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            TeacherDashboard teacherDashboard = new TeacherDashboard();
            teacherDashboard.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Teacher_profile teacherProfile = new Teacher_profile();
            teacherProfile.Show();
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

        private void LoadClasses()
        {

            string query = @"
                SELECT DISTINCT c.ClassID, c.ClassName 
                FROM classes c 
                INNER JOIN teacher_allocations ta ON c.ClassID = ta.ClassID 
                WHERE ta.TeacherID = @TeacherID";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TeacherID", Teacher_profile.Session.LoggedInUserID);

                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        comboBox1.DisplayMember = "ClassName";
                        comboBox1.ValueMember = "ClassID";
                        comboBox1.DataSource = dt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Class load maduvalli dosha ide: " + ex.Message);
                }
            }
        }

        private void LoadTerms()
        {
            comboBox2.Items.Clear();
            comboBox2.Items.Add("Term 1");
            comboBox2.Items.Add("Term 2");
            comboBox2.Items.Add("Term 3");

            if (comboBox2.Items.Count > 0)
            {
                comboBox2.SelectedIndex = 0;
            }
        }


        private void Load_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue == null)
            {
                MessageBox.Show("Dayaavittu ondu class annu aayke madi!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString = "Server=localhost;Port=3307;Database=school_ams;Uid=root;Pwd=;";

            string query = @"
                SELECT 
                    StudentNo AS 'Admission No', 
                    FirstName AS 'First Name', 
                    LastName AS 'Last Name' 
                FROM students 
                WHERE CurrentClassID = @ClassID";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ClassID", comboBox1.SelectedValue);

                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);


                        dataGridView1.DataSource = dt;
                        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Students data load maduvalli dosha ide: " + ex.Message);
                }
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
        }

        private void leftPanel_Paint(object sender, PaintEventArgs e)
        {
        }

        private void Subject_Marks_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}