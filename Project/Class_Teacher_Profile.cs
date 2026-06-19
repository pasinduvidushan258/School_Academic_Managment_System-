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
    public partial class Class_Teacher_Profile : Form
    {
        public Class_Teacher_Profile()
        {
            InitializeComponent();
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
            Class_Teacher_Class_Top_Students classTeacherClassTopStudents = new Class_Teacher_Class_Top_Students();
            classTeacherClassTopStudents.Show();
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
    }
}
