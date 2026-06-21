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
    public partial class Admin_Manage_Pending_User : Form
    {
        // Database Connection String
        string connectionString = "Server=localhost;Port=3307;Database=school_ams;Uid=root;Pwd=;";

        // To remember the ID of the User selected from the DataGrid
        private string selectedUserID = "";

        public Admin_Manage_Pending_User()
        {
            InitializeComponent();

            
            this.Load += Admin_Manage_Pending_User_Load;

           
            this.textBox1.TextChanged += textBox1_TextChanged;
            this.dataGridView1.CellClick += dataGridView1_CellClick;
            this.Save.Click += button1_Click; 
        }

        private void Admin_Manage_Pending_User_Load(object sender, EventArgs e)
        {
            
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Admin");
            comboBox1.Items.Add("Teacher");
            comboBox1.Items.Add("ClassTeacher");
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList; 

            
            LoadPendingUsers("");
        }

        
        private void LoadPendingUsers(string usernameKeyword)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    
                    string query = @"
                        SELECT UserID, FirstName, LastName, Email, Username 
                        FROM users 
                        WHERE UserRole = 'Pending' AND Username LIKE @search";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@search", "%" + usernameKeyword + "%");
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        dataGridView1.DataSource = dt;

                        
                        dataGridView1.ReadOnly = true;
                        dataGridView1.AllowUserToAddRows = false;
                        dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error loading users: " + ex.Message); }
        }

        // Filtering the table when typing in the TextBox
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            LoadPendingUsers(textBox1.Text.Trim());
        }

        
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
               
                selectedUserID = dataGridView1.Rows[e.RowIndex].Cells["UserID"].Value.ToString();
            }
        }

        
        
        private void button1_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrEmpty(selectedUserID))
            {
                MessageBox.Show("Please select a user from the table above.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a new role.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Update the Database
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string updateQuery = "UPDATE users SET UserRole = @role WHERE UserID = @uid";

                    using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@role", comboBox1.SelectedItem.ToString()); 
                        cmd.Parameters.AddWithValue("@uid", selectedUserID); 
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Position changed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Update Error: " + ex.Message); }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}