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
    public partial class Admin_student : Form
    {
        // Database Connection String
        string connectionString = "Server=localhost;Port=3307;Database=school_ams;Uid=root;Pwd=;";

        // දැනට Edit කරමින් පවතින පේළියේ (Row) Index එක මතක තබාගැනීමට
        private int editingRowIndex = -1;

        public Admin_student()
        {
            InitializeComponent();

            // මුලින්ම මුළු DataGridView එකම අතින් වෙනස් කරන්න බැරි වෙන්න හදනවා (Read-Only)
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false; // යටින් එන හිස් පේළිය අයින් කරනවා
        }

        private void Admin_student_Load(object sender, EventArgs e)
        {
            LoadClassesToComboBox();
            LoadStudents(""); // මුලින්ම හැම ළමයාවම පෙන්නනවා
        }

        // ==========================================
        // ComboBox එකට පන්ති ටික ගේන Method එක
        // ==========================================
        private void LoadClassesToComboBox()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT ClassID, ClassName FROM classes ORDER BY Grade ASC, ClassName ASC";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        // Add "All Classes" to the beginning
                        DataRow row = dt.NewRow();
                        row["ClassID"] = 0;
                        row["ClassName"] = "All Classes";
                        dt.Rows.InsertAt(row, 0);

                        comboBox1.DataSource = dt;
                        comboBox1.DisplayMember = "ClassName";
                        comboBox1.ValueMember = "ClassID";
                        comboBox1.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading classes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==========================================
        // The method of bringing children into the grid
        // ==========================================
        private void LoadStudents(string searchKeyword = "", int classId = 0)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    
                    string query = @"
                        SELECT s.StudentNo,
                               CONCAT(s.FirstName, ' ', IFNULL(s.LastName, '')) AS 'Name',
                               s.DOB,
                               s.Gender,
                               s.Address,
                               s.GuardianName,
                               s.GuardianContact,
                               c.ClassName AS 'Class'
                        FROM students s
                        LEFT JOIN classes c ON s.CurrentClassID = c.ClassID
                        WHERE (s.StudentNo LIKE @search OR s.FirstName LIKE @search OR s.LastName LIKE @search)";

                    if (classId > 0)
                    {
                        query += " AND s.CurrentClassID = @classId";
                    }

                    query += " ORDER BY s.StudentNo ASC";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@search", "%" + searchKeyword + "%");
                        if (classId > 0)
                        {
                            cmd.Parameters.AddWithValue("@classId", classId);
                        }

                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        dataGridView1.Columns.Clear();
                        dataGridView1.DataSource = dt;

                        // StudentNo, Name and Class are locked separately so that they can never be edited.
                        dataGridView1.Columns["StudentNo"].ReadOnly = true;
                        dataGridView1.Columns["Name"].ReadOnly = true;
                        dataGridView1.Columns["Class"].ReadOnly = true;

                        //  The Edit button (As a Link)
                        DataGridViewLinkColumn editCol = new DataGridViewLinkColumn();
                        editCol.Name = "EditAction";
                        editCol.HeaderText = "Actions";
                        editCol.Text = "Edit";
                        editCol.UseColumnTextForLinkValue = true;
                        editCol.LinkColor = Color.DodgerBlue;
                        dataGridView1.Columns.Add(editCol);

                        // The Delete button(As a Link)
                        DataGridViewLinkColumn delCol = new DataGridViewLinkColumn();
                        delCol.Name = "DeleteAction";
                        delCol.HeaderText = "";
                        delCol.Text = "X";
                        delCol.UseColumnTextForLinkValue = true;
                        delCol.LinkColor = Color.Red;
                        dataGridView1.Columns.Add(delCol);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string keyword = textBox1.Text.Trim();
            int classId = 0;
            if (comboBox1.SelectedValue != null && int.TryParse(comboBox1.SelectedValue.ToString(), out int parsedId))
            {
                classId = parsedId;
            }
            LoadStudents(keyword, classId);
        }

        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue != null && int.TryParse(comboBox1.SelectedValue.ToString(), out int classId))
            {
                string keyword = textBox1.Text.Trim();
                LoadStudents(keyword, classId);
            }
        }

        
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string studentNo = dataGridView1.Rows[e.RowIndex].Cells["StudentNo"].Value.ToString();

           
            if (dataGridView1.Columns[e.ColumnIndex].Name == "DeleteAction")
            {
                if (MessageBox.Show("Are you sure you want to delete this student?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        using (MySqlConnection conn = new MySqlConnection(connectionString))
                        {
                            conn.Open();

                            
                            MySqlCommand cmd1 = new MySqlCommand("DELETE FROM exam_marks WHERE StudentNo = @StudentNo", conn);
                            cmd1.Parameters.AddWithValue("@StudentNo", studentNo);
                            cmd1.ExecuteNonQuery();

                            
                            MySqlCommand cmd2 = new MySqlCommand("DELETE FROM students WHERE StudentNo = @StudentNo", conn);
                            cmd2.Parameters.AddWithValue("@StudentNo", studentNo);
                            cmd2.ExecuteNonQuery();

                            MessageBox.Show("Deleted successfully!");

                            // Refreshing the grid
                            int classId = 0;
                            if (comboBox1.SelectedValue != null && int.TryParse(comboBox1.SelectedValue.ToString(), out int parsedId)) { classId = parsedId; }
                            LoadStudents(textBox1.Text.Trim(), classId);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting: " + ex.Message);
                    }
                }
            }

            
            else if (dataGridView1.Columns[e.ColumnIndex].Name == "EditAction")
            {
                DataGridViewLinkCell linkCell = (DataGridViewLinkCell)dataGridView1.Rows[e.RowIndex].Cells["EditAction"];

                if (linkCell.Value.ToString() == "Edit")
                {
                    
                    dataGridView1.ReadOnly = false;

                   
                    if (editingRowIndex != -1 && editingRowIndex != e.RowIndex)
                    {
                        dataGridView1.Rows[editingRowIndex].ReadOnly = true;
                        ((DataGridViewLinkCell)dataGridView1.Rows[editingRowIndex].Cells["EditAction"]).Value = "Edit";
                    }

                    //  Only this line is allowed to be edited.
                    dataGridView1.Rows[e.RowIndex].ReadOnly = false;

                 
                    dataGridView1.Rows[e.RowIndex].Cells["StudentNo"].ReadOnly = true;
                    dataGridView1.Rows[e.RowIndex].Cells["Name"].ReadOnly = true;
                    dataGridView1.Rows[e.RowIndex].Cells["Class"].ReadOnly = true;

                 
                    linkCell.Value = "Save";
                    editingRowIndex = e.RowIndex;
                }
                else if (linkCell.Value.ToString() == "Save")
                {
                    // After clicking "Save" button update the database
                    try
                    {
                        string dob = dataGridView1.Rows[e.RowIndex].Cells["DOB"].Value?.ToString();
                        string gender = dataGridView1.Rows[e.RowIndex].Cells["Gender"].Value?.ToString();
                        string address = dataGridView1.Rows[e.RowIndex].Cells["Address"].Value?.ToString();
                        string gName = dataGridView1.Rows[e.RowIndex].Cells["GuardianName"].Value?.ToString();
                        string gContact = dataGridView1.Rows[e.RowIndex].Cells["GuardianContact"].Value?.ToString();

                        using (MySqlConnection conn = new MySqlConnection(connectionString))
                        {
                            conn.Open();
                            string updateQuery = @"
                                UPDATE students 
                                SET DOB = @dob, Gender = @gender, Address = @address, 
                                    GuardianName = @gName, GuardianContact = @gContact
                                WHERE StudentNo = @StudentNo";

                            using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                            {
                                DateTime parsedDob;
                                if (DateTime.TryParse(dob, out parsedDob))
                                {
                                    cmd.Parameters.AddWithValue("@dob", parsedDob.ToString("yyyy-MM-dd"));
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@dob", DBNull.Value);
                                }

                                cmd.Parameters.AddWithValue("@gender", gender);
                                cmd.Parameters.AddWithValue("@address", address);
                                cmd.Parameters.AddWithValue("@gName", gName);
                                cmd.Parameters.AddWithValue("@gContact", gContact);
                                cmd.Parameters.AddWithValue("@StudentNo", studentNo);

                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Successfully updated!");
                            }
                        }

                        
                        dataGridView1.ReadOnly = true;
                        linkCell.Value = "Edit";
                        editingRowIndex = -1;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error updating: " + ex.Message);
                    }
                }
            }
        }

        
        private void button19_Click(object sender, EventArgs e)
        {
            Admin_add_student admin_Add_Student = new Admin_add_student();

            // If you add a new child and close the form, the grid will refresh with the new child.
            if (admin_Add_Student.ShowDialog() == DialogResult.OK)
            {
                int classId = 0;
                if (comboBox1.SelectedValue != null && int.TryParse(comboBox1.SelectedValue.ToString(), out int parsedId)) { classId = parsedId; }
                LoadStudents(textBox1.Text.Trim(), classId);
            }
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
            Admin_Extra_curricular admin_Extra_Curricular = new Admin_Extra_curricular();
            admin_Extra_Curricular.Show();
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
    }
}