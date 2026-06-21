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
    public partial class Admin_Teacher : Form
    {
        // Database Connection String
        string connectionString = "Server=localhost;Port=3306;Database=school_ams;Uid=root;Pwd=;";

        // To remember the index of the rows being edited
        private int editIndexPending = -1;
        private int editIndexCurrent = -1;

        public Admin_Teacher()
        {
            InitializeComponent();


            Pending_Users_view.ReadOnly = true;
            Pending_Users_view.AllowUserToAddRows = false;

            Current_Users_View.ReadOnly = true;
            Current_Users_View.AllowUserToAddRows = false;


            this.Load += new EventHandler(Admin_Teacher_Load);
        }

        private void button6_Click(object sender, EventArgs e)
        {

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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void leftPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Admin_Teacher_Load(object sender, EventArgs e)
        {
            LoadRoleComboBox();
            LoadPendingUsers("");
            LoadCurrentUsers("", "All Roles");
        }


        private void LoadRoleComboBox()
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("All Roles");
            comboBox1.Items.Add("Admin");
            comboBox1.Items.Add("Teacher");
            comboBox1.Items.Add("ClassTeacher");
            comboBox1.SelectedIndex = 0;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void LoadPendingUsers(string searchKeyword)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT UserID, 
                               CONCAT(FirstName, ' ', IFNULL(LastName, '')) AS 'Name', 
                               Email, Username, UserRole, MobileNumber, Gender, DOB 
                        FROM users 
                        WHERE LOWER(UserRole) = 'pending' 
                        AND (FirstName LIKE @search OR LastName LIKE @search OR Username LIKE @search)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@search", "%" + searchKeyword + "%");
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        Pending_Users_view.Columns.Clear();
                        Pending_Users_view.DataSource = dt;

                        SetGridColumnsReadOnly(Pending_Users_view);
                        AddActionColumns(Pending_Users_view);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error loading pending users: " + ex.Message); }
        }

        private void LoadCurrentUsers(string searchKeyword, string roleFilter)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT UserID, 
                               CONCAT(FirstName, ' ', IFNULL(LastName, '')) AS 'Name', 
                               Email, Username, UserRole, MobileNumber, Gender, DOB 
                        FROM users 
                        WHERE LOWER(UserRole) IN ('admin', 'teacher', 'classteacher') 
                        AND (FirstName LIKE @search OR LastName LIKE @search OR Username LIKE @search)";

                    if (roleFilter != "All Roles" && !string.IsNullOrEmpty(roleFilter))
                    {
                        query += " AND UserRole = @role";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@search", "%" + searchKeyword + "%");
                        if (roleFilter != "All Roles") cmd.Parameters.AddWithValue("@role", roleFilter);

                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        Current_Users_View.Columns.Clear();
                        Current_Users_View.DataSource = dt;

                        SetGridColumnsReadOnly(Current_Users_View);
                        AddActionColumns(Current_Users_View);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error loading current users: " + ex.Message); }
        }


        // A common method for adding action buttons to a DataGridView
        private void AddActionColumns(DataGridView grid)
        {
            DataGridViewLinkColumn editCol = new DataGridViewLinkColumn();
            editCol.Name = "EditAction";
            editCol.HeaderText = "Actions";
            editCol.Text = "Edit";
            editCol.UseColumnTextForLinkValue = true;
            editCol.LinkColor = Color.DodgerBlue;
            grid.Columns.Add(editCol);

            DataGridViewLinkColumn delCol = new DataGridViewLinkColumn();
            delCol.Name = "DeleteAction";
            delCol.HeaderText = "";
            delCol.Text = "X";
            delCol.UseColumnTextForLinkValue = true;
            delCol.LinkColor = Color.Red;
            grid.Columns.Add(delCol);
        }

        // Locking unchangeable columns
        private void SetGridColumnsReadOnly(DataGridView grid)
        {
            if (grid.Columns["UserID"] != null) grid.Columns["UserID"].ReadOnly = true;
            if (grid.Columns["Name"] != null) grid.Columns["Name"].ReadOnly = true;
            if (grid.Columns["Username"] != null) grid.Columns["Username"].ReadOnly = true;
            if (grid.Columns["Email"] != null) grid.Columns["Email"].ReadOnly = true;
            if (grid.Columns["UserRole"] != null) grid.Columns["UserRole"].ReadOnly = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string keyword = textBox1.Text.Trim();
            string role = comboBox1.SelectedItem?.ToString() ?? "All Roles";

            LoadPendingUsers(keyword);
            LoadCurrentUsers(keyword, role);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string keyword = textBox1.Text.Trim();
            string role = comboBox1.SelectedItem?.ToString() ?? "All Roles";

            LoadCurrentUsers(keyword, role);
        }

        // For Current Users table
        private void Current_Users_View_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            HandleGridActions(Current_Users_View, e, ref editIndexCurrent);
        }

        // For Pending Users table
        private void Pending_Users_view_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            HandleGridActions(Pending_Users_view, e, ref editIndexPending);
        }

        // Common operation for both Grids (Edit and Delete)
        private void HandleGridActions(DataGridView grid, DataGridViewCellEventArgs e, ref int editIndex)
        {
            if (e.RowIndex < 0) return;

            string userId = grid.Rows[e.RowIndex].Cells["UserID"].Value.ToString();


            if (grid.Columns[e.ColumnIndex].Name == "DeleteAction")
            {
                if (MessageBox.Show("Are you sure you want to delete this user?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        using (MySqlConnection conn = new MySqlConnection(connectionString))
                        {
                            conn.Open();

                            new MySqlCommand("UPDATE classes SET ClassTeacherID = NULL WHERE ClassTeacherID = " + userId, conn).ExecuteNonQuery();
                            new MySqlCommand("DELETE FROM teacher_allocations WHERE TeacherID = " + userId, conn).ExecuteNonQuery();
                            new MySqlCommand("DELETE FROM exam_marks WHERE TeacherID = " + userId, conn).ExecuteNonQuery();


                            new MySqlCommand("DELETE FROM users WHERE UserID = " + userId, conn).ExecuteNonQuery();
                            MessageBox.Show("Successfully Deleted!");


                            string keyword = textBox1.Text.Trim();
                            string role = comboBox1.SelectedItem?.ToString() ?? "All Roles";
                            LoadPendingUsers(keyword);
                            LoadCurrentUsers(keyword, role);
                        }
                    }
                    catch (Exception ex) { MessageBox.Show("Error deleting: " + ex.Message); }
                }
            }

            else if (grid.Columns[e.ColumnIndex].Name == "EditAction")
            {
                DataGridViewLinkCell linkCell = (DataGridViewLinkCell)grid.Rows[e.RowIndex].Cells["EditAction"];

                if (linkCell.Value.ToString() == "Edit")
                {
                    grid.ReadOnly = false;

                    if (editIndex != -1 && editIndex != e.RowIndex)
                    {
                        grid.Rows[editIndex].ReadOnly = true;
                        ((DataGridViewLinkCell)grid.Rows[editIndex].Cells["EditAction"]).Value = "Edit";
                    }

                    grid.Rows[e.RowIndex].ReadOnly = false;
                    SetGridColumnsReadOnly(grid);

                    linkCell.Value = "Save";
                    editIndex = e.RowIndex;
                }
                else if (linkCell.Value.ToString() == "Save")
                {
                    try
                    {
                        string mobile = grid.Rows[e.RowIndex].Cells["MobileNumber"].Value?.ToString();
                        string gender = grid.Rows[e.RowIndex].Cells["Gender"].Value?.ToString();
                        string dob = grid.Rows[e.RowIndex].Cells["DOB"].Value?.ToString();

                        using (MySqlConnection conn = new MySqlConnection(connectionString))
                        {
                            conn.Open();
                            string updateQuery = "UPDATE users SET MobileNumber = @mob, Gender = @gen, DOB = @dob WHERE UserID = @uid";

                            using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@mob", mobile);
                                cmd.Parameters.AddWithValue("@gen", gender);

                                if (DateTime.TryParse(dob, out DateTime parsedDob))
                                    cmd.Parameters.AddWithValue("@dob", parsedDob.ToString("yyyy-MM-dd"));
                                else
                                    cmd.Parameters.AddWithValue("@dob", DBNull.Value);

                                cmd.Parameters.AddWithValue("@uid", userId);
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Data was updated!");
                            }
                        }

                        grid.ReadOnly = true;
                        linkCell.Value = "Edit";
                        editIndex = -1;
                    }
                    catch (Exception ex) { MessageBox.Show("Update Error: " + ex.Message); }
                }
            }
        }

        private void Manage_Pending_User_Click(object sender, EventArgs e)
        {
            Admin_Manage_Pending_User manageUserForm = new Admin_Manage_Pending_User();


            if (manageUserForm.ShowDialog() == DialogResult.OK)
            {
                string keyword = textBox1.Text.Trim();
                string role = comboBox1.SelectedItem?.ToString() ?? "All Roles";

                LoadPendingUsers(keyword);
                LoadCurrentUsers(keyword, role);
            }
        }

        private void pictureBox27_Click(object sender, EventArgs e)
        {

        }

        private void button21_Click(object sender, EventArgs e)
        {
            AdminDashboard adminDashboard = new AdminDashboard();
            adminDashboard.Show();
            this.Hide();
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }
    }

}
