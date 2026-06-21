namespace Project
{
    partial class subject_teacher_mark_add
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
            label1 = new Label();
            panel1 = new Panel();
            dataGridView_marksAdd = new DataGridView();
            btnCancel = new Button();
            btnSaveMarks = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView_marksAdd).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 24F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.Location = new Point(371, 30);
            label1.Name = "label1";
            label1.Size = new Size(226, 54);
            label1.TabIndex = 6;
            label1.Text = "Add marks";
            // 
            // panel1
            // 
            panel1.BackColor = Color.Navy;
            panel1.Controls.Add(dataGridView_marksAdd);
            panel1.Controls.Add(btnCancel);
            panel1.Controls.Add(btnSaveMarks);
            panel1.Location = new Point(85, 102);
            panel1.Name = "panel1";
            panel1.Size = new Size(792, 627);
            panel1.TabIndex = 5;
            // 
            // dataGridView_marksAdd
            // 
            dataGridView_marksAdd.BackgroundColor = Color.Gray;
            dataGridView_marksAdd.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView_marksAdd.Location = new Point(38, 23);
            dataGridView_marksAdd.Name = "dataGridView_marksAdd";
            dataGridView_marksAdd.RowHeadersWidth = 51;
            dataGridView_marksAdd.Size = new Size(726, 434);
            dataGridView_marksAdd.TabIndex = 0;
            // 
            // btnCancel
            // 
            btnCancel.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            btnCancel.Location = new Point(481, 505);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(123, 47);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSaveMarks
            // 
            btnSaveMarks.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSaveMarks.Location = new Point(148, 505);
            btnSaveMarks.Name = "btnSaveMarks";
            btnSaveMarks.Size = new Size(132, 47);
            btnSaveMarks.TabIndex = 1;
            btnSaveMarks.Text = "Save";
            btnSaveMarks.UseVisualStyleBackColor = true;
            btnSaveMarks.Click += btnSaveMarks_Click;
            // 
            // subject_teacher_mark_add
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gray;
            ClientSize = new Size(962, 759);
            Controls.Add(label1);
            Controls.Add(panel1);
            Name = "subject_teacher_mark_add";
            Text = "subject_teacher_mark_add";
            Load += subject_teacher_mark_add_Load;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView_marksAdd).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Panel panel1;
        private DataGridView dataGridView_marksAdd;
        private Button btnCancel;
        private Button btnSaveMarks;
    }
}