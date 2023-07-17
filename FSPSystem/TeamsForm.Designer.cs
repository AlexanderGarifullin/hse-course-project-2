namespace FSPSystem
{
    partial class TeamsForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxTeamName = new System.Windows.Forms.TextBox();
            this.dataGridViewRoster = new System.Windows.Forms.DataGridView();
            this.btnAddStudent = new System.Windows.Forms.Button();
            this.btnDeleteStudent = new System.Windows.Forms.Button();
            this.btnChangeStudent = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAddExistSrudent = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRoster)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(357, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Команда";
            // 
            // textBoxTeamName
            // 
            this.textBoxTeamName.Location = new System.Drawing.Point(255, 28);
            this.textBoxTeamName.Name = "textBoxTeamName";
            this.textBoxTeamName.Size = new System.Drawing.Size(276, 22);
            this.textBoxTeamName.TabIndex = 1;
            // 
            // dataGridViewRoster
            // 
            this.dataGridViewRoster.AllowUserToAddRows = false;
            this.dataGridViewRoster.AllowUserToDeleteRows = false;
            this.dataGridViewRoster.AllowUserToResizeColumns = false;
            this.dataGridViewRoster.AllowUserToResizeRows = false;
            this.dataGridViewRoster.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewRoster.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewRoster.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRoster.Location = new System.Drawing.Point(12, 56);
            this.dataGridViewRoster.MultiSelect = false;
            this.dataGridViewRoster.Name = "dataGridViewRoster";
            this.dataGridViewRoster.ReadOnly = true;
            this.dataGridViewRoster.RowHeadersVisible = false;
            this.dataGridViewRoster.RowHeadersWidth = 51;
            this.dataGridViewRoster.RowTemplate.Height = 24;
            this.dataGridViewRoster.Size = new System.Drawing.Size(776, 293);
            this.dataGridViewRoster.TabIndex = 2;
            // 
            // btnAddStudent
            // 
            this.btnAddStudent.Location = new System.Drawing.Point(406, 355);
            this.btnAddStudent.Name = "btnAddStudent";
            this.btnAddStudent.Size = new System.Drawing.Size(125, 43);
            this.btnAddStudent.TabIndex = 3;
            this.btnAddStudent.Text = "Добавить студента";
            this.btnAddStudent.UseVisualStyleBackColor = true;
            this.btnAddStudent.Visible = false;
            this.btnAddStudent.Click += new System.EventHandler(this.btnAddStudent_Click);
            // 
            // btnDeleteStudent
            // 
            this.btnDeleteStudent.Location = new System.Drawing.Point(668, 355);
            this.btnDeleteStudent.Name = "btnDeleteStudent";
            this.btnDeleteStudent.Size = new System.Drawing.Size(125, 43);
            this.btnDeleteStudent.TabIndex = 4;
            this.btnDeleteStudent.Text = "Удалить студента";
            this.btnDeleteStudent.UseVisualStyleBackColor = true;
            this.btnDeleteStudent.Click += new System.EventHandler(this.btnDeleteStudent_Click);
            // 
            // btnChangeStudent
            // 
            this.btnChangeStudent.Location = new System.Drawing.Point(537, 355);
            this.btnChangeStudent.Name = "btnChangeStudent";
            this.btnChangeStudent.Size = new System.Drawing.Size(125, 43);
            this.btnChangeStudent.TabIndex = 5;
            this.btnChangeStudent.Text = "Изменить студента";
            this.btnChangeStudent.UseVisualStyleBackColor = true;
            this.btnChangeStudent.Visible = false;
            this.btnChangeStudent.Click += new System.EventHandler(this.btnChangeStudent_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(397, 414);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(0, 0);
            this.button2.TabIndex = 7;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(266, 404);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(125, 43);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "ОК";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(397, 404);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(125, 43);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "ОТМЕНА";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnAddExistSrudent
            // 
            this.btnAddExistSrudent.Location = new System.Drawing.Point(12, 355);
            this.btnAddExistSrudent.Name = "btnAddExistSrudent";
            this.btnAddExistSrudent.Size = new System.Drawing.Size(222, 43);
            this.btnAddExistSrudent.TabIndex = 10;
            this.btnAddExistSrudent.Text = "Добавить существующих студентов";
            this.btnAddExistSrudent.UseVisualStyleBackColor = true;
            this.btnAddExistSrudent.Click += new System.EventHandler(this.btnAddExistSrudent_Click);
            // 
            // TeamsForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(800, 459);
            this.Controls.Add(this.btnAddExistSrudent);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnChangeStudent);
            this.Controls.Add(this.btnDeleteStudent);
            this.Controls.Add(this.btnAddStudent);
            this.Controls.Add(this.dataGridViewRoster);
            this.Controls.Add(this.textBoxTeamName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "TeamsForm";
            this.Text = "Команда";
            this.Load += new System.EventHandler(this.TeamsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRoster)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox textBoxTeamName;
        private System.Windows.Forms.DataGridView dataGridViewRoster;
        private System.Windows.Forms.Button btnAddStudent;
        private System.Windows.Forms.Button btnDeleteStudent;
        private System.Windows.Forms.Button btnChangeStudent;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAddExistSrudent;
    }
}