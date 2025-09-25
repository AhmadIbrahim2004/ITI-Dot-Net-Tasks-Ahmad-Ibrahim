namespace Entity_Framework_Day_3_CodeFirst
{
    partial class Form1
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
            dgvMovies = new DataGridView();
            gbMovieDetails = new GroupBox();
            chkIsActive = new CheckBox();
            numDuration = new NumericUpDown();
            label2 = new Label();
            txtTitle = new TextBox();
            label1 = new Label();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnClear = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvMovies).BeginInit();
            gbMovieDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numDuration).BeginInit();
            SuspendLayout();
            // 
            // dgvMovies
            // 
            dgvMovies.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMovies.Dock = DockStyle.Top;
            dgvMovies.Location = new Point(0, 0);
            dgvMovies.Name = "dgvMovies";
            dgvMovies.ReadOnly = true;
            dgvMovies.RowHeadersWidth = 51;
            dgvMovies.Size = new Size(800, 117);
            dgvMovies.TabIndex = 0;
            dgvMovies.SelectionChanged += dgvMovies_SelectionChanged;
            // 
            // gbMovieDetails
            // 
            gbMovieDetails.Controls.Add(chkIsActive);
            gbMovieDetails.Controls.Add(numDuration);
            gbMovieDetails.Controls.Add(label2);
            gbMovieDetails.Controls.Add(txtTitle);
            gbMovieDetails.Controls.Add(label1);
            gbMovieDetails.Location = new Point(6, 135);
            gbMovieDetails.Name = "gbMovieDetails";
            gbMovieDetails.Size = new Size(781, 130);
            gbMovieDetails.TabIndex = 1;
            gbMovieDetails.TabStop = false;
            gbMovieDetails.Text = "Movie Details";
            // 
            // chkIsActive
            // 
            chkIsActive.AutoSize = true;
            chkIsActive.Location = new Point(423, 71);
            chkIsActive.Name = "chkIsActive";
            chkIsActive.Size = new Size(86, 24);
            chkIsActive.TabIndex = 4;
            chkIsActive.Text = "Is Active";
            chkIsActive.UseVisualStyleBackColor = true;
            // 
            // numDuration
            // 
            numDuration.Location = new Point(158, 85);
            numDuration.Maximum = new decimal(new int[] { 400, 0, 0, 0 });
            numDuration.Name = "numDuration";
            numDuration.Size = new Size(165, 27);
            numDuration.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(21, 85);
            label2.Name = "label2";
            label2.Size = new Size(109, 20);
            label2.TabIndex = 2;
            label2.Text = "Duration (min):";
            // 
            // txtTitle
            // 
            txtTitle.Location = new Point(149, 37);
            txtTitle.Name = "txtTitle";
            txtTitle.Size = new Size(120, 27);
            txtTitle.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(23, 33);
            label1.Name = "label1";
            label1.Size = new Size(41, 20);
            label1.TabIndex = 0;
            label1.Text = "Title:";
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(35, 297);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(80, 26);
            btnAdd.TabIndex = 2;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(34, 348);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(93, 30);
            btnUpdate.TabIndex = 3;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(203, 302);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(114, 34);
            btnDelete.TabIndex = 4;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(202, 358);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(126, 37);
            btnClear.TabIndex = 5;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnClear);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnAdd);
            Controls.Add(gbMovieDetails);
            Controls.Add(dgvMovies);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgvMovies).EndInit();
            gbMovieDetails.ResumeLayout(false);
            gbMovieDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numDuration).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvMovies;
        private GroupBox gbMovieDetails;
        private CheckBox chkIsActive;
        private NumericUpDown numDuration;
        private Label label2;
        private TextBox txtTitle;
        private Label label1;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnClear;
    }
}
