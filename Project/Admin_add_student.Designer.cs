namespace Project
{
    partial class Admin_add_student
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
            gender = new Panel();
            Save = new Button();
            class_name = new ComboBox();
            guardian_contact = new TextBox();
            guardian_name = new TextBox();
            address = new TextBox();
            DOB = new TextBox();
            label10 = new Label();
            label9 = new Label();
            label8 = new Label();
            textBox2 = new TextBox();
            First_name = new TextBox();
            Student_No = new TextBox();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            label7 = new Label();
            panel2 = new Panel();
            comboBox1 = new ComboBox();
            gender.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // gender
            // 
            gender.BackColor = Color.Gray;
            gender.Controls.Add(comboBox1);
            gender.Controls.Add(Save);
            gender.Controls.Add(class_name);
            gender.Controls.Add(guardian_contact);
            gender.Controls.Add(guardian_name);
            gender.Controls.Add(address);
            gender.Controls.Add(DOB);
            gender.Controls.Add(label10);
            gender.Controls.Add(label9);
            gender.Controls.Add(label8);
            gender.Controls.Add(textBox2);
            gender.Controls.Add(First_name);
            gender.Controls.Add(Student_No);
            gender.Controls.Add(label6);
            gender.Controls.Add(label5);
            gender.Controls.Add(label4);
            gender.Controls.Add(label3);
            gender.Controls.Add(label2);
            gender.Controls.Add(label1);
            gender.Location = new Point(52, 108);
            gender.Name = "gender";
            gender.Size = new Size(559, 581);
            gender.TabIndex = 0;
            // 
            // Save
            // 
            Save.BackColor = Color.FromArgb(0, 64, 0);
            Save.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Save.Location = new Point(196, 511);
            Save.Name = "Save";
            Save.Size = new Size(125, 46);
            Save.TabIndex = 19;
            Save.Text = "Save";
            Save.UseVisualStyleBackColor = false;
            Save.Click += Save_Click;
            // 
            // class_name
            // 
            class_name.DropDownStyle = ComboBoxStyle.DropDownList;
            class_name.FormattingEnabled = true;
            class_name.Location = new Point(254, 460);
            class_name.Name = "class_name";
            class_name.Size = new Size(261, 28);
            class_name.TabIndex = 18;
            // 
            // guardian_contact
            // 
            guardian_contact.Location = new Point(254, 415);
            guardian_contact.Name = "guardian_contact";
            guardian_contact.Size = new Size(261, 27);
            guardian_contact.TabIndex = 17;
            // 
            // guardian_name
            // 
            guardian_name.Location = new Point(254, 356);
            guardian_name.Name = "guardian_name";
            guardian_name.Size = new Size(261, 27);
            guardian_name.TabIndex = 16;
            // 
            // address
            // 
            address.Location = new Point(254, 299);
            address.Name = "address";
            address.Size = new Size(261, 27);
            address.TabIndex = 15;
            // 
            // DOB
            // 
            DOB.Location = new Point(254, 194);
            DOB.Name = "DOB";
            DOB.Size = new Size(261, 27);
            DOB.TabIndex = 13;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label10.Location = new Point(67, 460);
            label10.Name = "label10";
            label10.Size = new Size(121, 28);
            label10.TabIndex = 12;
            label10.Text = "Class Name";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label9.Location = new Point(67, 415);
            label9.Name = "label9";
            label9.Size = new Size(177, 28);
            label9.TabIndex = 11;
            label9.Text = "Guardian Contact";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label8.Location = new Point(67, 356);
            label8.Name = "label8";
            label8.Size = new Size(160, 28);
            label8.TabIndex = 10;
            label8.Text = "Guardian Name";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(254, 144);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(261, 27);
            textBox2.TabIndex = 9;
            // 
            // First_name
            // 
            First_name.Location = new Point(254, 95);
            First_name.Name = "First_name";
            First_name.Size = new Size(261, 27);
            First_name.TabIndex = 8;
            // 
            // Student_No
            // 
            Student_No.Location = new Point(254, 50);
            Student_No.Name = "Student_No";
            Student_No.Size = new Size(261, 27);
            Student_No.TabIndex = 7;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label6.Location = new Point(71, 298);
            label6.Name = "label6";
            label6.Size = new Size(87, 28);
            label6.TabIndex = 5;
            label6.Text = "Address";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label5.Location = new Point(71, 245);
            label5.Name = "label5";
            label5.Size = new Size(80, 28);
            label5.TabIndex = 4;
            label5.Text = "Gender";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label4.Location = new Point(71, 194);
            label4.Name = "label4";
            label4.Size = new Size(137, 28);
            label4.TabIndex = 3;
            label4.Text = "Date of Birth";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label3.Location = new Point(71, 144);
            label3.Name = "label3";
            label3.Size = new Size(112, 28);
            label3.TabIndex = 2;
            label3.Text = "Last Name";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label2.Location = new Point(71, 95);
            label2.Name = "label2";
            label2.Size = new Size(115, 28);
            label2.TabIndex = 1;
            label2.Text = "First Name";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(71, 49);
            label1.Name = "label1";
            label1.Size = new Size(120, 28);
            label1.TabIndex = 0;
            label1.Text = "Student No";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 24F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label7.Location = new Point(185, 22);
            label7.Name = "label7";
            label7.Size = new Size(332, 54);
            label7.TabIndex = 6;
            label7.Text = "Student Register";
            // 
            // panel2
            // 
            panel2.BackColor = Color.DarkBlue;
            panel2.Controls.Add(gender);
            panel2.Controls.Add(label7);
            panel2.Location = new Point(36, 24);
            panel2.Name = "panel2";
            panel2.Size = new Size(644, 721);
            panel2.TabIndex = 19;
            panel2.Paint += panel2_Paint;
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Male", "Female" });
            comboBox1.Location = new Point(254, 245);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(261, 28);
            comboBox1.TabIndex = 20;
            // 
            // Admin_add_student
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(725, 774);
            Controls.Add(panel2);
            Name = "Admin_add_student";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Admin_add_student";
            Load += Admin_add_student_Load;
            gender.ResumeLayout(false);
            gender.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel gender;
        private Label label1;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private TextBox textBox2;
        private TextBox First_name;
        private TextBox Student_No;
        private TextBox DOB;
        private Label label10;
        private Label label9;
        private Label label8;
        private TextBox guardian_contact;
        private TextBox guardian_name;
        private TextBox address;
        private ComboBox class_name;
        private Panel panel2;
        private Button Save;
        private ComboBox comboBox1;
    }
}