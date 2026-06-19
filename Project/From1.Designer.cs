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
            PanelRight = new Panel();
            passwordShow = new PictureBox();
            passwordHide = new PictureBox();
            label5 = new Label();
            button2 = new Button();
            button1 = new Button();
            linkLabel2 = new LinkLabel();
            Register = new LinkLabel();
            label3 = new Label();
            txtPassword = new TextBox();
            label2 = new Label();
            txtUserName = new TextBox();
            label1 = new Label();
            LOGIN = new Label();
            pictureBox3 = new PictureBox();
            label4 = new Label();
            pictureBox2 = new PictureBox();
            pictureBox1 = new PictureBox();
            colorDialog1 = new ColorDialog();
            PanelLeft.SuspendLayout();
            PanelRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)passwordShow).BeginInit();
            ((System.ComponentModel.ISupportInitialize)passwordHide).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // PanelLeft
            // 
            PanelLeft.Anchor = AnchorStyles.None;
            PanelLeft.BackColor = Color.DarkBlue;
            PanelLeft.Controls.Add(PanelRight);
            PanelLeft.Controls.Add(pictureBox3);
            PanelLeft.Controls.Add(label4);
            PanelLeft.Controls.Add(pictureBox2);
            PanelLeft.Controls.Add(pictureBox1);
            PanelLeft.Location = new Point(207, 89);
            PanelLeft.Name = "PanelLeft";
            PanelLeft.Size = new Size(900, 500);
            PanelLeft.TabIndex = 0;
            PanelLeft.Paint += PanelLeft_Paint;
            // 
            // PanelRight
            // 
            PanelRight.Anchor = AnchorStyles.None;
            PanelRight.BackColor = SystemColors.InactiveCaption;
            PanelRight.Controls.Add(passwordShow);
            PanelRight.Controls.Add(passwordHide);
            PanelRight.Controls.Add(label5);
            PanelRight.Controls.Add(button2);
            PanelRight.Controls.Add(button1);
            PanelRight.Controls.Add(linkLabel2);
            PanelRight.Controls.Add(Register);
            PanelRight.Controls.Add(label3);
            PanelRight.Controls.Add(txtPassword);
            PanelRight.Controls.Add(label2);
            PanelRight.Controls.Add(txtUserName);
            PanelRight.Controls.Add(label1);
            PanelRight.Controls.Add(LOGIN);
            PanelRight.Location = new Point(362, 12);
            PanelRight.Name = "PanelRight";
            PanelRight.Size = new Size(525, 476);
            PanelRight.TabIndex = 1;
            PanelRight.Paint += PanelRight_Paint;
            // 
            // passwordShow
            // 
            passwordShow.Image = (Image)resources.GetObject("passwordShow.Image");
            passwordShow.Location = new Point(450, 282);
            passwordShow.Name = "passwordShow";
            passwordShow.Size = new Size(25, 18);
            passwordShow.SizeMode = PictureBoxSizeMode.StretchImage;
            passwordShow.TabIndex = 13;
            passwordShow.TabStop = false;
            passwordShow.Click += passwordShow_Click;
            // 
            // passwordHide
            // 
            passwordHide.Image = (Image)resources.GetObject("passwordHide.Image");
            passwordHide.Location = new Point(450, 282);
            passwordHide.Name = "passwordHide";
            passwordHide.Size = new Size(25, 18);
            passwordHide.SizeMode = PictureBoxSizeMode.StretchImage;
            passwordHide.TabIndex = 12;
            passwordHide.TabStop = false;
            passwordHide.Visible = false;
            passwordHide.Click += passwordHide_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Garamond", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(66, 89);
            label5.Name = "label5";
            label5.Size = new Size(204, 22);
            label5.TabIndex = 11;
            label5.Text = "Sign in to your account";
            // 
            // button2
            // 
            button2.BackColor = SystemColors.InactiveCaption;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button2.ForeColor = SystemColors.ActiveCaptionText;
            button2.Location = new Point(472, 3);
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
            button1.Location = new Point(213, 356);
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
            linkLabel2.Location = new Point(358, 313);
            linkLabel2.Name = "linkLabel2";
            linkLabel2.Size = new Size(125, 20);
            linkLabel2.TabIndex = 8;
            linkLabel2.TabStop = true;
            linkLabel2.Text = "Forgot Password?";
            // 
            // Register
            // 
            Register.AutoSize = true;
            Register.Location = new Point(234, 415);
            Register.Name = "Register";
            Register.Size = new Size(114, 20);
            Register.TabIndex = 7;
            Register.TabStop = true;
            Register.Text = "Create Account!";
            Register.LinkClicked += register_LinkClicked;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(65, 415);
            label3.Name = "label3";
            label3.Size = new Size(163, 20);
            label3.TabIndex = 6;
            label3.Text = "Don't have an account?";
            // 
            // txtPassword
            // 
            txtPassword.BorderStyle = BorderStyle.None;
            txtPassword.Location = new Point(66, 281);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(417, 20);
            txtPassword.TabIndex = 4;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Black", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label2.Location = new Point(65, 233);
            label2.Name = "label2";
            label2.Size = new Size(127, 28);
            label2.TabIndex = 3;
            label2.Text = "PASSWORD";
            // 
            // txtUserName
            // 
            txtUserName.BorderStyle = BorderStyle.None;
            txtUserName.Location = new Point(66, 186);
            txtUserName.Name = "txtUserName";
            txtUserName.Size = new Size(417, 20);
            txtUserName.TabIndex = 2;
            txtUserName.TextChanged += txtUserName_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Black", 12F, FontStyle.Bold | FontStyle.Italic);
            label1.Location = new Point(66, 144);
            label1.Name = "label1";
            label1.Size = new Size(126, 28);
            label1.TabIndex = 1;
            label1.Text = "USERNAME";
            // 
            // LOGIN
            // 
            LOGIN.AutoSize = true;
            LOGIN.Font = new Font("Segoe UI", 24F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            LOGIN.ForeColor = Color.DarkBlue;
            LOGIN.Location = new Point(52, 35);
            LOGIN.Name = "LOGIN";
            LOGIN.Size = new Size(296, 54);
            LOGIN.TabIndex = 0;
            LOGIN.Text = "Welcome Back";
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
            label4.Font = new Font("Bell MT", 16.8000011F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Snow;
            label4.Location = new Point(12, 21);
            label4.Name = "label4";
            label4.Size = new Size(329, 68);
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
            // From1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkBlue;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1337, 705);
            Controls.Add(PanelLeft);
            FormBorderStyle = FormBorderStyle.None;
            Name = "From1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            WindowState = FormWindowState.Maximized;
            Load += Form1_Load;
            PanelLeft.ResumeLayout(false);
            PanelLeft.PerformLayout();
            PanelRight.ResumeLayout(false);
            PanelRight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)passwordShow).EndInit();
            ((System.ComponentModel.ISupportInitialize)passwordHide).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
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
        private LinkLabel Register;
        private Label label3;
        private LinkLabel linkLabel2;
        private Label label4;
        private Button button1;
        private PictureBox pictureBox3;
        private Button button2;
        private Label label5;
        private PictureBox passwordShow;
        private PictureBox passwordHide;
    }
}
