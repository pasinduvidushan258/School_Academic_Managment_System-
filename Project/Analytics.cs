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
    public partial class Analytics : Form
    {
        public Analytics()
        {
            InitializeComponent();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            TeacherDashboard teacherDashboard = new TeacherDashboard();
            teacherDashboard.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Teacher_profile teacherProfile = new Teacher_profile();
            teacherProfile.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Subject_Marks subjectMarks = new Subject_Marks();
            subjectMarks.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Top_Students students = new Top_Students();
            students.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            From1 loginForm = new From1();
            loginForm.Show();
            this.Close();
        }
    }
}
