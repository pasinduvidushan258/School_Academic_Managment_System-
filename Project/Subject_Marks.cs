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


            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;


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
                    MessageBox.Show("Database not connected: " + ex.Message);
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
                MessageBox.Show("Select combobox value!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int selectedClassID = Convert.ToInt32(comboBox1.SelectedValue);
            int loggedInTeacherID = Teacher_profile.Session.LoggedInUserID;

            // Extract numeric term value (e.g., extracts 1 out of "Term 1")
            int selectedTerm = 1;
            string termText = comboBox2.SelectedItem.ToString();
            if (termText.Contains("2")) selectedTerm = 2;
            else if (termText.Contains("3")) selectedTerm = 3;

            // QUERY 1: Fetches the student list and pairs them with their matching marks for the selected term
            string query = @"
        SELECT 
            s.StudentNo AS 'Admission No', 
            s.FirstName AS 'First Name', 
            s.LastName AS 'Last Name',
            @TermDisplay AS 'Term',
            em.Marks AS 'Marks'
        FROM students s
        LEFT JOIN exam_marks em ON s.StudentNo = em.StudentNo 
            AND em.ClassID = s.CurrentClassID 
            AND em.Term = @TermQuery
        WHERE s.CurrentClassID = @ClassID";

            string studentCountQuery = "SELECT COUNT(*) FROM students WHERE CurrentClassID = @ClassID";

            // FIXED QUERY 3: Removed 'TeacherID = @TeacherID' to look at all student grades assigned to this class container
            string marksMetricsQuery = @"
        SELECT AVG(Marks) AS ClassAvg, MAX(Marks) AS HighestMark 
        FROM exam_marks 
        WHERE ClassID = @ClassID 
          AND Term = @TermMetrics";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // 1. Populate DataGridView including Term and Marks safely
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ClassID", selectedClassID);
                        cmd.Parameters.AddWithValue("@TermQuery", selectedTerm);
                        cmd.Parameters.AddWithValue("@TermDisplay", "Term " + selectedTerm);

                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        dataGridView1.DataSource = dt;
                        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    }

                    // 2. Count total enrolled students metrics
                    int totalStudentsCount = 0;
                    using (MySqlCommand cmdCount = new MySqlCommand(studentCountQuery, conn))
                    {
                        cmdCount.Parameters.AddWithValue("@ClassID", selectedClassID);
                        totalStudentsCount = Convert.ToInt32(cmdCount.ExecuteScalar());
                    }
                    Students.Text = totalStudentsCount.ToString();

                    // 3. Calculate average and highest marks details class-wide
                    using (MySqlCommand cmdMetrics = new MySqlCommand(marksMetricsQuery, conn))
                    {
                        cmdMetrics.Parameters.AddWithValue("@ClassID", selectedClassID);
                        cmdMetrics.Parameters.AddWithValue("@TermMetrics", selectedTerm);

                        using (MySqlDataReader reader = cmdMetrics.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Calculate Class Average cleanly
                                if (reader["ClassAvg"] != DBNull.Value)
                                {
                                    double avg = Convert.ToDouble(reader["ClassAvg"]);
                                    Class_avg.Text = $"{Math.Round(avg)}%";
                                }
                                else
                                {
                                    Class_avg.Text = "0%";
                                }

                                // Calculate Highest Mark cleanly
                                if (reader["HighestMark"] != DBNull.Value)
                                {
                                    decimal maxMark = Convert.ToDecimal(reader["HighestMark"]);
                                    Highest.Text = Math.Round(maxMark).ToString();
                                }
                                else
                                {
                                    Highest.Text = "N/A";
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Students data not load :\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void Add_marks_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue == null)
            {
                MessageBox.Show("No class assing!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int classID = Convert.ToInt32(comboBox1.SelectedValue);
            int term = 1;
            string termText = comboBox2.SelectedItem.ToString();
            if (termText.Contains("2")) term = 2;
            else if (termText.Contains("3")) term = 3;

            // Open the entry form and pass variables down
            subject_teacher_mark_add marksForm = new subject_teacher_mark_add(classID, term);
            marksForm.ShowDialog(); // Opens as a popup window

            // Refresh the dashboard stats automatically when the popup closes!
            Load_Click(sender, e);
        }
    }
}