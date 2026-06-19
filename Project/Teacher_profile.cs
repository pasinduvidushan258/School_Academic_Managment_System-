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
    public partial class Teacher_profile : Form
    {
        public Teacher_profile()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            TeacherDashboard teacherDashboard = new TeacherDashboard();
            teacherDashboard.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Subject_Marks subjectMarks = new Subject_Marks();
            subjectMarks.Show();
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

        private void passwordShow_Click(object sender, EventArgs e)
        {
            Current_Password.UseSystemPasswordChar = false;
            passwordHide.Visible = true;
            passwordShow.Visible = false;
        }

        private void passwordHide_Click(object sender, EventArgs e)
        {
            Current_Password.UseSystemPasswordChar = true;
            passwordShow.Visible = true;
            passwordHide.Visible = false;
        }

        private void pictureBox35_Click(object sender, EventArgs e)
        {
            New_Password.UseSystemPasswordChar = false;
            pictureBox35.Visible = false;
            pictureBox37.Visible = true;
        }

        private void pictureBox37_Click(object sender, EventArgs e)
        {
            New_Password.UseSystemPasswordChar = true;
            pictureBox37.Visible = false;
            pictureBox35.Visible = true;
        }

        private void pictureBox36_Click(object sender, EventArgs e)
        {
            Confirm_New_Password.UseSystemPasswordChar = false;
            pictureBox36.Visible = false;
            pictureBox38.Visible = true;
        }

        private void pictureBox38_Click(object sender, EventArgs e)
        {
            Confirm_New_Password.UseSystemPasswordChar = true;
            pictureBox38.Visible = false;
            pictureBox36.Visible = true;
        }

        private void leftPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
