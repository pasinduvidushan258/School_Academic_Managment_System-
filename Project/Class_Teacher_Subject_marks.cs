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
    public partial class Class_Teacher_Subject_marks : Form
    {
        string connectionString = "Server=localhost;Port=3307;Database=school_ams;Uid=root;Pwd=;";
        public Class_Teacher_Subject_marks()
        {
            InitializeComponent();
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            LoadClasses();
            LoadTerms();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Class_Teacher_Profile classTeacherProfile = new Class_Teacher_Profile();
            classTeacherProfile.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Class_Teacher_Subject_marks classTeacherSubjectMarks = new Class_Teacher_Subject_marks();
            classTeacherSubjectMarks.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Class_Teacher_Analytics classTeacherAnalytics = new Class_Teacher_Analytics();
            classTeacherAnalytics.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Class_Teacher_Top_Students classTeacherTopStudents = new Class_Teacher_Top_Students();
            classTeacherTopStudents.Show();
            this.Hide();
        }

        private void Class_Analytics_Click(object sender, EventArgs e)
        {
            Class_Teacher_Class_Analytics classTeacherClassAnalytics = new Class_Teacher_Class_Analytics();
            classTeacherClassAnalytics.Show();
            this.Hide();
        }

        private void Discipline_Click(object sender, EventArgs e)
        {
            Class_Teacher_Discipline classTeacherDiscipline = new Class_Teacher_Discipline();
            classTeacherDiscipline.Show();
            this.Hide();
        }

        private void Extra_Curricular_Click(object sender, EventArgs e)
        {
            Class_Teacher_Extra_curricular classTeacherExtraCurricular = new Class_Teacher_Extra_curricular();
            classTeacherExtraCurricular.Show();
            this.Hide();
        }

        private void Class_Top_Students_Click(object sender, EventArgs e)
        {
            Class_Teacher_Class_Top_Students Class_Teacher_Class_Top_Students = new Class_Teacher_Class_Top_Students();
            Class_Teacher_Class_Top_Students.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ClassTeacherDashboard classTeacherDashboard = new ClassTeacherDashboard();
            classTeacherDashboard.Show();
            this.Hide();
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

        private void LoadClasses()
        {

            string query = @"
                SELECT DISTINCT c.ClassID, CONCAT('Grade ', c.Grade, ' - ', c.ClassName) AS ClassName
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
                    MessageBox.Show("Error: " + ex.Message);
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
                MessageBox.Show("Error1!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int selectedClassID = Convert.ToInt32(comboBox1.SelectedValue);
            int loggedInTeacherID = Class_Teacher_Profile.Session.LoggedInUserID; 

            // Extract numeric term value (e.g., extracts 1 out of "Term 1")
            int selectedTerm = 1;
            string termText = comboBox2.SelectedItem.ToString();
            if (termText.Contains("2")) selectedTerm = 2;
            else if (termText.Contains("3")) selectedTerm = 3;

            // FIXED QUERY: To use multiple instances safely in MySQL, we supply unique parameter names (@Term1, @Term2) OR assign them accurately.
            // Also adding MAX() or grouping ensures that if there are multiple subjects, the grid won't crash or duplicate student names.
            string query = @"
        SELECT 
            s.StudentNo AS 'Admission No', 
            s.FirstName AS 'First Name', 
            s.LastName AS 'Last Name',
            @TermDisplay AS 'Term',
            MAX(em.Marks) AS 'Marks'
        FROM students s
        LEFT JOIN exam_marks em ON s.StudentNo = em.StudentNo 
            AND em.ClassID = s.CurrentClassID 
            AND em.Term = @TermQuery
        WHERE s.CurrentClassID = @ClassID
        GROUP BY s.StudentNo, s.FirstName, s.LastName";

            string studentCountQuery = "SELECT COUNT(*) FROM students WHERE CurrentClassID = @ClassID";

            string marksMetricsQuery = @"
        SELECT AVG(Marks) AS ClassAvg, MAX(Marks) AS HighestMark 
        FROM exam_marks 
        WHERE ClassID = @ClassID 
          AND Term = @TermMetrics 
          AND TeacherID = @TeacherMetricsID";

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
                        cmd.Parameters.AddWithValue("@TermDisplay", "Term " + selectedTerm); // Clean display injection

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

                    // 3. Calculate average and highest marks details with distinct metric parameters
                    using (MySqlCommand cmdMetrics = new MySqlCommand(marksMetricsQuery, conn))
                    {
                        cmdMetrics.Parameters.AddWithValue("@ClassID", selectedClassID);
                        cmdMetrics.Parameters.AddWithValue("@TermMetrics", selectedTerm);
                        cmdMetrics.Parameters.AddWithValue("@TeacherMetricsID", loggedInTeacherID);

                        using (MySqlDataReader reader = cmdMetrics.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                if (reader["ClassAvg"] != DBNull.Value)
                                {
                                    double avg = Convert.ToDouble(reader["ClassAvg"]);
                                    Class_avg.Text = $"{Math.Round(avg)}%";
                                }
                                else
                                {
                                    Class_avg.Text = "0%";
                                }

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
                    // Provides a clean breakdown of the internal driver issue if your schema has an alternative constraint mismatch
                    MessageBox.Show("Students data load maduvalli dosha ide:\n" + ex.Message, "Database Execution Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Add_marks_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue == null)
            {
                MessageBox.Show("Dayaavittu ondu class annu aayke madi!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int classID = Convert.ToInt32(comboBox1.SelectedValue);

            int term = 1;
            string termText = comboBox2.SelectedItem.ToString();
            if (termText.Contains("2")) term = 2;
            else if (termText.Contains("3")) term = 3;

            // Open the entry form and pass variables down
            add_mark_from marksForm = new add_mark_from(classID, term);
            marksForm.ShowDialog(); // Opens as a popup window

            // Refresh the dashboard stats automatically when the popup closes!
            Load_Click(sender, e);
        }
    }
}
