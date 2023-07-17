namespace FSPSystem
{
    partial class LessonForm
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
            this.dateTimePickerLessonDate = new System.Windows.Forms.DateTimePicker();
            this.textBoxLessonTopic = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxTeachers = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(121, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Тема занятия";
            // 
            // dateTimePickerLessonDate
            // 
            this.dateTimePickerLessonDate.Location = new System.Drawing.Point(40, 88);
            this.dateTimePickerLessonDate.Name = "dateTimePickerLessonDate";
            this.dateTimePickerLessonDate.Size = new System.Drawing.Size(252, 22);
            this.dateTimePickerLessonDate.TabIndex = 1;
            this.dateTimePickerLessonDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dateTimePickerLessonDate_KeyPress);
            // 
            // textBoxLessonTopic
            // 
            this.textBoxLessonTopic.Location = new System.Drawing.Point(40, 38);
            this.textBoxLessonTopic.Name = "textBoxLessonTopic";
            this.textBoxLessonTopic.Size = new System.Drawing.Size(252, 22);
            this.textBoxLessonTopic.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(121, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Дата проведения";
            // 
            // comboBoxTeachers
            // 
            this.comboBoxTeachers.FormattingEnabled = true;
            this.comboBoxTeachers.Location = new System.Drawing.Point(40, 146);
            this.comboBoxTeachers.Name = "comboBoxTeachers";
            this.comboBoxTeachers.Size = new System.Drawing.Size(252, 24);
            this.comboBoxTeachers.TabIndex = 4;
            this.comboBoxTeachers.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBoxTeachers_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(121, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Преподаватель";
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(26, 186);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(125, 25);
            this.buttonOK.TabIndex = 6;
            this.buttonOK.Text = "Ок";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(167, 185);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(125, 25);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // LessonForm
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(336, 225);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxTeachers);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxLessonTopic);
            this.Controls.Add(this.dateTimePickerLessonDate);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "LessonForm";
            this.Text = "Занятие";
            this.Load += new System.EventHandler(this.LessonForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.DateTimePicker dateTimePickerLessonDate;
        public System.Windows.Forms.TextBox textBoxLessonTopic;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox comboBoxTeachers;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
    }
}