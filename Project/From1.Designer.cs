namespace Project
{
    partial class From1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(From1));
            PanelLeft = new Panel();
            pictureBox3 = new PictureBox();
            label4 = new Label();
            pictureBox2 = new PictureBox();
            pictureBox1 = new PictureBox();
            PanelRight = new Panel();
            button2 = new Button();
            button1 = new Button();
            linkLabel2 = new LinkLabel();
            linkLabel1 = new LinkLabel();
            label3 = new Label();
            txtPassword = new TextBox();
            label2 = new Label();
            txtUserName = new TextBox();
            label1 = new Label();
            LOGIN = new Label();
            colorDialog1 = new ColorDialog();
            PanelLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            PanelRight.SuspendLayout();
            SuspendLayout();
            // 
            // PanelLeft
            // 
            PanelLeft.BackColor = Color.DarkBlue;
            PanelLeft.Controls.Add(pictureBox3);
            PanelLeft.Controls.Add(label4);
            PanelLeft.Controls.Add(pictureBox2);
            PanelLeft.Controls.Add(pictureBox1);
            PanelLeft.Location = new Point(0, 0);
            PanelLeft.Name = "PanelLeft";
            PanelLeft.Size = new Size(400, 500);
            PanelLeft.TabIndex = 0;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(28, 104);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(293, 160);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 8;
            pictureBox3.TabStop = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Bell MT", 18F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Snow;
            label4.Location = new Point(12, 21);
            label4.Name = "label4";
            label4.Size = new Size(335, 68);
            label4.TabIndex = 7;
            label4.Text = "School Academic \r\n      Management System";
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(0, 261);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(149, 239);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 6;
            pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(-266, 105);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(125, 62);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            // 
            // PanelRight
            // 
            PanelRight.BackColor = SystemColors.InactiveCaption;
            PanelRight.Controls.Add(button2);
            PanelRight.Controls.Add(button1);
            PanelRight.Controls.Add(linkLabel2);
            PanelRight.Controls.Add(linkLabel1);
            PanelRight.Controls.Add(label3);
            PanelRight.Controls.Add(txtPassword);
            PanelRight.Controls.Add(label2);
            PanelRight.Controls.Add(txtUserName);
            PanelRight.Controls.Add(label1);
            PanelRight.Controls.Add(LOGIN);
            PanelRight.Location = new Point(349, 0);
            PanelRight.Name = "PanelRight";
            PanelRight.Size = new Size(554, 500);
            PanelRight.TabIndex = 1;
            PanelRight.Paint += PanelRight_Paint;
            // 
            // button2
            // 
            button2.BackColor = SystemColors.InactiveCaption;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button2.ForeColor = SystemColors.ActiveCaptionText;
            button2.Location = new Point(501, 3);
            button2.Name = "button2";
            button2.Size = new Size(50, 29);
            button2.TabIndex = 10;
            button2.Text = "X";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.Location = new Point(209, 372);
            button1.Name = "button1";
            button1.Size = new Size(125, 37);
            button1.TabIndex = 9;
            button1.Text = "LOGIN";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // linkLabel2
            // 
            linkLabel2.AutoSize = true;
            linkLabel2.Location = new Point(358, 326);
            linkLabel2.Name = "linkLabel2";
            linkLabel2.Size = new Size(125, 20);
            linkLabel2.TabIndex = 8;
            linkLabel2.TabStop = true;
            linkLabel2.Text = "Forgot Password?";
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(235, 440);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(114, 20);
            linkLabel1.TabIndex = 7;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Create Account!";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(66, 440);
            label3.Name = "label3";
            label3.Size = new Size(163, 20);
            label3.TabIndex = 6;
            label3.Text = "Don't have an account?";
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(66, 296);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(417, 27);
            txtPassword.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(66, 251);
            label2.Name = "label2";
            label2.Size = new Size(101, 28);
            label2.TabIndex = 3;
            label2.Text = "Password";
            // 
            // txtUserName
            // 
            txtUserName.Location = new Point(66, 202);
            txtUserName.Name = "txtUserName";
            txtUserName.Size = new Size(417, 27);
            txtUserName.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(66, 150);
            label1.Name = "label1";
            label1.Size = new Size(116, 28);
            label1.TabIndex = 1;
            label1.Text = "User Name";
            // 
            // LOGIN
            // 
            LOGIN.AutoSize = true;
            LOGIN.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            LOGIN.ForeColor = Color.DarkBlue;
            LOGIN.Location = new Point(116, 61);
            LOGIN.Name = "LOGIN";
            LOGIN.Size = new Size(297, 54);
            LOGIN.TabIndex = 0;
            LOGIN.Text = "Welcome Back";
            // 
            // From1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 500);
            Controls.Add(PanelRight);
            Controls.Add(PanelLeft);
            FormBorderStyle = FormBorderStyle.None;
            Name = "From1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += Form1_Load;
            PanelLeft.ResumeLayout(false);
            PanelLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            PanelRight.ResumeLayout(false);
            PanelRight.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel PanelLeft;
        private Panel PanelRight;
        private Label LOGIN;
        private TextBox txtPassword;
        private Label label2;
        private TextBox txtUserName;
        private Label label1;
        private ColorDialog colorDialog1;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private LinkLabel linkLabel1;
        private Label label3;
        private LinkLabel linkLabel2;
        private Label label4;
        private Button button1;
        private PictureBox pictureBox3;
        private Button button2;
    }
}
