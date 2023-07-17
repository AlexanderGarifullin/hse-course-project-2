namespace FSPSystem
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnReports = new System.Windows.Forms.Button();
            this.btnTeachers = new System.Windows.Forms.Button();
            this.btnLessons = new System.Windows.Forms.Button();
            this.btnStudents = new System.Windows.Forms.Button();
            this.btnTeams = new System.Windows.Forms.Button();
            this.btnContests = new System.Windows.Forms.Button();
            this.btnTasks = new System.Windows.Forms.Button();
            this.btnOlympiads = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnReports
            // 
            this.btnReports.Location = new System.Drawing.Point(143, 105);
            this.btnReports.Name = "btnReports";
            this.btnReports.Size = new System.Drawing.Size(125, 25);
            this.btnReports.TabIndex = 1;
            this.btnReports.Text = "Отчёты";
            this.btnReports.UseVisualStyleBackColor = true;
            this.btnReports.Click += new System.EventHandler(this.btnReports_Click);
            // 
            // btnTeachers
            // 
            this.btnTeachers.Location = new System.Drawing.Point(57, 12);
            this.btnTeachers.Name = "btnTeachers";
            this.btnTeachers.Size = new System.Drawing.Size(125, 25);
            this.btnTeachers.TabIndex = 2;
            this.btnTeachers.Text = "Преподаватели";
            this.btnTeachers.UseVisualStyleBackColor = true;
            this.btnTeachers.Click += new System.EventHandler(this.btnTeachers_Click);
            // 
            // btnLessons
            // 
            this.btnLessons.Location = new System.Drawing.Point(57, 43);
            this.btnLessons.Name = "btnLessons";
            this.btnLessons.Size = new System.Drawing.Size(125, 25);
            this.btnLessons.TabIndex = 3;
            this.btnLessons.Text = "Занятия";
            this.btnLessons.UseVisualStyleBackColor = true;
            this.btnLessons.Click += new System.EventHandler(this.btnLessons_Click);
            // 
            // btnStudents
            // 
            this.btnStudents.Location = new System.Drawing.Point(223, 12);
            this.btnStudents.Name = "btnStudents";
            this.btnStudents.Size = new System.Drawing.Size(125, 25);
            this.btnStudents.TabIndex = 4;
            this.btnStudents.Text = "Студенты";
            this.btnStudents.UseVisualStyleBackColor = true;
            this.btnStudents.Click += new System.EventHandler(this.btnStudents_Click);
            // 
            // btnTeams
            // 
            this.btnTeams.Location = new System.Drawing.Point(223, 43);
            this.btnTeams.Name = "btnTeams";
            this.btnTeams.Size = new System.Drawing.Size(125, 25);
            this.btnTeams.TabIndex = 5;
            this.btnTeams.Text = "Команды";
            this.btnTeams.UseVisualStyleBackColor = true;
            this.btnTeams.Click += new System.EventHandler(this.btnTeams_Click);
            // 
            // btnContests
            // 
            this.btnContests.Location = new System.Drawing.Point(12, 74);
            this.btnContests.Name = "btnContests";
            this.btnContests.Size = new System.Drawing.Size(125, 25);
            this.btnContests.TabIndex = 6;
            this.btnContests.Text = "Контесты";
            this.btnContests.UseVisualStyleBackColor = true;
            this.btnContests.Click += new System.EventHandler(this.btnContests_Click);
            // 
            // btnTasks
            // 
            this.btnTasks.Location = new System.Drawing.Point(143, 74);
            this.btnTasks.Name = "btnTasks";
            this.btnTasks.Size = new System.Drawing.Size(125, 25);
            this.btnTasks.TabIndex = 7;
            this.btnTasks.Text = "Задачи";
            this.btnTasks.UseVisualStyleBackColor = true;
            this.btnTasks.Click += new System.EventHandler(this.btnTasks_Click);
            // 
            // btnOlympiads
            // 
            this.btnOlympiads.Location = new System.Drawing.Point(274, 74);
            this.btnOlympiads.Name = "btnOlympiads";
            this.btnOlympiads.Size = new System.Drawing.Size(125, 25);
            this.btnOlympiads.TabIndex = 8;
            this.btnOlympiads.Text = "Олимпиады";
            this.btnOlympiads.UseVisualStyleBackColor = true;
            this.btnOlympiads.Click += new System.EventHandler(this.btnOlympiads_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 146);
            this.Controls.Add(this.btnOlympiads);
            this.Controls.Add(this.btnTasks);
            this.Controls.Add(this.btnContests);
            this.Controls.Add(this.btnTeams);
            this.Controls.Add(this.btnStudents);
            this.Controls.Add(this.btnLessons);
            this.Controls.Add(this.btnTeachers);
            this.Controls.Add(this.btnReports);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "FSP";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnReports;
        private System.Windows.Forms.Button btnTeachers;
        private System.Windows.Forms.Button btnLessons;
        private System.Windows.Forms.Button btnStudents;
        private System.Windows.Forms.Button btnTeams;
        private System.Windows.Forms.Button btnContests;
        private System.Windows.Forms.Button btnTasks;
        private System.Windows.Forms.Button btnOlympiads;
    }
}

