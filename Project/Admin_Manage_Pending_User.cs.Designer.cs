namespace Project
{
    partial class Admin_Manage_Pending_User
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            comboBox1 = new ComboBox();
            label2 = new Label();
            panel20 = new Panel();
            Save = new Button();
            dataGridView1 = new DataGridView();
            textBox1 = new TextBox();
            label3 = new Label();
            label1 = new Label();
            panel1.SuspendLayout();
            panel20.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.DarkBlue;
            panel1.Controls.Add(comboBox1);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(panel20);
            panel1.Controls.Add(dataGridView1);
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(36, 38);
            panel1.Name = "panel1";
            panel1.Size = new Size(677, 580);
            panel1.TabIndex = 0;
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(214, 437);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(246, 28);
            comboBox1.TabIndex = 8;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold);
            label2.Location = new Point(173, 388);
            label2.Name = "label2";
            label2.Size = new Size(324, 31);
            label2.TabIndex = 7;
            label2.Text = "Which role should switch to?";
            // 
            // panel20
            // 
            panel20.BackColor = Color.FromArgb(0, 64, 0);
            panel20.Controls.Add(Save);
            panel20.Location = new Point(232, 488);
            panel20.Name = "panel20";
            panel20.Size = new Size(216, 55);
            panel20.TabIndex = 6;
            // 
            // Save
            // 
            Save.BackColor = Color.FromArgb(0, 64, 0);
            Save.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            Save.Location = new Point(11, 8);
            Save.Name = "Save";
            Save.Size = new Size(197, 40);
            Save.TabIndex = 0;
            Save.Text = "Save";
            Save.UseVisualStyleBackColor = false;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(31, 174);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(623, 161);
            dataGridView1.TabIndex = 4;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(329, 126);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(207, 27);
            textBox1.TabIndex = 3;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(100, 122);
            label3.Name = "label3";
            label3.Size = new Size(193, 31);
            label3.TabIndex = 2;
            label3.Text = "Enter User Name";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 24F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.Location = new Point(110, 34);
            label1.Name = "label1";
            label1.Size = new Size(436, 54);
            label1.TabIndex = 0;
            label1.Text = "Manage Pending User";
            // 
            // Admin_Manage_Pending_User
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(753, 669);
            Controls.Add(panel1);
            Name = "Admin_Manage_Pending_User";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Admin_Manage_Pending_User";
            Load += Admin_Manage_Pending_User_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel20.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private TextBox textBox1;
        private Label label3;
        private DataGridView dataGridView1;
        private Label label2;
        private Panel panel20;
        private Button Save;
        private ComboBox comboBox1;
    }
}