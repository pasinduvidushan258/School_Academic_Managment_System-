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
    public partial class Admin_Discipline : Form
    {
        public Admin_Discipline()
        {
            InitializeComponent();
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

        private void button1_Click(object sender, EventArgs e)
        {
            From1 from1 = new From1();
            from1.Show();
            this.Hide();
        }

        private void Class_Analytics_Click(object sender, EventArgs e)
        {
            Admin_marks admin_Marks = new Admin_marks();
            admin_Marks.Show();
            this.Hide();
        }

        private void Discipline_Click(object sender, EventArgs e)
        {
            Admin_Discipline admin_Discipline = new Admin_Discipline();
            admin_Discipline.Show();
            this.Hide();
        }

        private void Extra_Curricular_Click(object sender, EventArgs e)
        {
            Admin_Extra_curricular admin_Extra_ = new Admin_Extra_curricular();
            admin_Extra_.Show();
            this.Hide();
        }
    }
}
