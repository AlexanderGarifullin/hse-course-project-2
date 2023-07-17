namespace FSPSystem
{
    partial class AddOlympiadForm
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
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxYear = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxBP = new System.Windows.Forms.TextBox();
            this.textBoxBPperTask = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxParticipateType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonAddParticipate = new System.Windows.Forms.Button();
            this.buttonDeleteParticipant = new System.Windows.Forms.Button();
            this.comboBoxParticipants = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(325, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Название";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(204, 28);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(308, 22);
            this.textBoxName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(201, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Год проведения";
            // 
            // textBoxYear
            // 
            this.textBoxYear.Location = new System.Drawing.Point(224, 72);
            this.textBoxYear.Name = "textBoxYear";
            this.textBoxYear.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBoxYear.Size = new System.Drawing.Size(72, 22);
            this.textBoxYear.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(352, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "BP";
            // 
            // textBoxBP
            // 
            this.textBoxBP.Location = new System.Drawing.Point(331, 72);
            this.textBoxBP.Name = "textBoxBP";
            this.textBoxBP.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBoxBP.Size = new System.Drawing.Size(72, 22);
            this.textBoxBP.TabIndex = 5;
            // 
            // textBoxBPperTask
            // 
            this.textBoxBPperTask.Location = new System.Drawing.Point(440, 72);
            this.textBoxBPperTask.Name = "textBoxBPperTask";
            this.textBoxBPperTask.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBoxBPperTask.Size = new System.Drawing.Size(72, 22);
            this.textBoxBPperTask.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(437, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "BP за задачу";
            // 
            // comboBoxParticipateType
            // 
            this.comboBoxParticipateType.FormattingEnabled = true;
            this.comboBoxParticipateType.Location = new System.Drawing.Point(297, 116);
            this.comboBoxParticipateType.Name = "comboBoxParticipateType";
            this.comboBoxParticipateType.Size = new System.Drawing.Size(121, 24);
            this.comboBoxParticipateType.TabIndex = 8;
            this.comboBoxParticipateType.SelectedIndexChanged += new System.EventHandler(this.comboBoxParticipateType_SelectedIndexChanged);
            this.comboBoxParticipateType.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBoxParticipateType_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(336, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "Участие";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(30, 175);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(705, 263);
            this.dataGridView1.TabIndex = 10;
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(328, 156);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 16);
            this.label6.TabIndex = 11;
            this.label6.Text = "Участники";
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(398, 490);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(125, 25);
            this.buttonCancel.TabIndex = 12;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(239, 490);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(125, 25);
            this.buttonOK.TabIndex = 13;
            this.buttonOK.Text = "Ок";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonAddParticipate
            // 
            this.buttonAddParticipate.Location = new System.Drawing.Point(509, 444);
            this.buttonAddParticipate.Name = "buttonAddParticipate";
            this.buttonAddParticipate.Size = new System.Drawing.Size(39, 25);
            this.buttonAddParticipate.TabIndex = 14;
            this.buttonAddParticipate.Text = "+";
            this.buttonAddParticipate.UseVisualStyleBackColor = true;
            this.buttonAddParticipate.Click += new System.EventHandler(this.buttonAddParticipate_Click);
            // 
            // buttonDeleteParticipant
            // 
            this.buttonDeleteParticipant.Location = new System.Drawing.Point(569, 444);
            this.buttonDeleteParticipant.Name = "buttonDeleteParticipant";
            this.buttonDeleteParticipant.Size = new System.Drawing.Size(166, 25);
            this.buttonDeleteParticipant.TabIndex = 15;
            this.buttonDeleteParticipant.Text = "Удалить участгика";
            this.buttonDeleteParticipant.UseVisualStyleBackColor = true;
            this.buttonDeleteParticipant.Click += new System.EventHandler(this.buttonDeleteParticipant_Click);
            // 
            // comboBoxParticipants
            // 
            this.comboBoxParticipants.FormattingEnabled = true;
            this.comboBoxParticipants.Location = new System.Drawing.Point(30, 445);
            this.comboBoxParticipants.Name = "comboBoxParticipants";
            this.comboBoxParticipants.Size = new System.Drawing.Size(473, 24);
            this.comboBoxParticipants.TabIndex = 16;
            this.comboBoxParticipants.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBoxParticipants_KeyPress);
            // 
            // AddOlympiadForm
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(747, 527);
            this.Controls.Add(this.comboBoxParticipants);
            this.Controls.Add(this.buttonDeleteParticipant);
            this.Controls.Add(this.buttonAddParticipate);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBoxParticipateType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxBPperTask);
            this.Controls.Add(this.textBoxBP);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxYear);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "AddOlympiadForm";
            this.Text = "Олимпиада";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox textBoxYear;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox textBoxBP;
        public System.Windows.Forms.TextBox textBoxBPperTask;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.ComboBox comboBoxParticipateType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonAddParticipate;
        private System.Windows.Forms.Button buttonDeleteParticipant;
        private System.Windows.Forms.ComboBox comboBoxParticipants;
    }
}