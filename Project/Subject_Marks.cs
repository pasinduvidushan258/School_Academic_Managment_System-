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
    public partial class Subject_Marks : Form
    {
        public Subject_Marks()
        {
            InitializeComponent();
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
    }
}
