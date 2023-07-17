namespace FSPSystem
{
    partial class ReportsForm
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
            this.buttonRating = new System.Windows.Forms.Button();
            this.buttonContestResult = new System.Windows.Forms.Button();
            this.buttonOlympiadResult = new System.Windows.Forms.Button();
            this.buttonTasksByDifficulty = new System.Windows.Forms.Button();
            this.buttonTasksByTheme = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonRating
            // 
            this.buttonRating.Location = new System.Drawing.Point(23, 12);
            this.buttonRating.Name = "buttonRating";
            this.buttonRating.Size = new System.Drawing.Size(189, 40);
            this.buttonRating.TabIndex = 0;
            this.buttonRating.Text = "Рейтинг факультатива";
            this.buttonRating.UseVisualStyleBackColor = true;
            this.buttonRating.Click += new System.EventHandler(this.buttonRating_Click);
            // 
            // buttonContestResult
            // 
            this.buttonContestResult.Location = new System.Drawing.Point(23, 58);
            this.buttonContestResult.Name = "buttonContestResult";
            this.buttonContestResult.Size = new System.Drawing.Size(189, 40);
            this.buttonContestResult.TabIndex = 1;
            this.buttonContestResult.Text = "Положение контеста";
            this.buttonContestResult.UseVisualStyleBackColor = true;
            this.buttonContestResult.Click += new System.EventHandler(this.buttonContestResult_Click);
            // 
            // buttonOlympiadResult
            // 
            this.buttonOlympiadResult.Location = new System.Drawing.Point(23, 104);
            this.buttonOlympiadResult.Name = "buttonOlympiadResult";
            this.buttonOlympiadResult.Size = new System.Drawing.Size(189, 40);
            this.buttonOlympiadResult.TabIndex = 2;
            this.buttonOlympiadResult.Text = "Положение олимпиады";
            this.buttonOlympiadResult.UseVisualStyleBackColor = true;
            this.buttonOlympiadResult.Click += new System.EventHandler(this.buttonOlympiadResult_Click);
            // 
            // buttonTasksByDifficulty
            // 
            this.buttonTasksByDifficulty.Location = new System.Drawing.Point(23, 150);
            this.buttonTasksByDifficulty.Name = "buttonTasksByDifficulty";
            this.buttonTasksByDifficulty.Size = new System.Drawing.Size(189, 53);
            this.buttonTasksByDifficulty.TabIndex = 3;
            this.buttonTasksByDifficulty.Text = "Задачи определенной сложности";
            this.buttonTasksByDifficulty.UseVisualStyleBackColor = true;
            this.buttonTasksByDifficulty.Click += new System.EventHandler(this.buttonTasksByDifficulty_Click);
            // 
            // buttonTasksByTheme
            // 
            this.buttonTasksByTheme.Location = new System.Drawing.Point(23, 209);
            this.buttonTasksByTheme.Name = "buttonTasksByTheme";
            this.buttonTasksByTheme.Size = new System.Drawing.Size(189, 50);
            this.buttonTasksByTheme.TabIndex = 4;
            this.buttonTasksByTheme.Text = "Задачи определенной темы";
            this.buttonTasksByTheme.UseVisualStyleBackColor = true;
            this.buttonTasksByTheme.Click += new System.EventHandler(this.buttonTasksByTheme_Click);
            // 
            // ReportsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(226, 271);
            this.Controls.Add(this.buttonTasksByTheme);
            this.Controls.Add(this.buttonTasksByDifficulty);
            this.Controls.Add(this.buttonOlympiadResult);
            this.Controls.Add(this.buttonContestResult);
            this.Controls.Add(this.buttonRating);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ReportsForm";
            this.Text = "Отчеты";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonRating;
        private System.Windows.Forms.Button buttonContestResult;
        private System.Windows.Forms.Button buttonOlympiadResult;
        private System.Windows.Forms.Button buttonTasksByDifficulty;
        private System.Windows.Forms.Button buttonTasksByTheme;
    }
}