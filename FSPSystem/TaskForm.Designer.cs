namespace FSPSystem
{
    partial class TaskForm
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
            this.textBoxCFID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxCoefficient = new System.Windows.Forms.ComboBox();
            this.comboBoxTaskWeight = new System.Windows.Forms.ComboBox();
            this.dataGridViewThemese = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonAddTheme = new System.Windows.Forms.Button();
            this.buttonDeleteTheme = new System.Windows.Forms.Button();
            this.comboBoxTheme = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewThemese)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(168, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Codeforces ID";
            // 
            // textBoxCFID
            // 
            this.textBoxCFID.Location = new System.Drawing.Point(54, 28);
            this.textBoxCFID.Name = "textBoxCFID";
            this.textBoxCFID.Size = new System.Drawing.Size(303, 22);
            this.textBoxCFID.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(168, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Коэффициент";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(193, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Вес";
            // 
            // comboBoxCoefficient
            // 
            this.comboBoxCoefficient.FormattingEnabled = true;
            this.comboBoxCoefficient.Location = new System.Drawing.Point(54, 72);
            this.comboBoxCoefficient.Name = "comboBoxCoefficient";
            this.comboBoxCoefficient.Size = new System.Drawing.Size(303, 24);
            this.comboBoxCoefficient.TabIndex = 4;
            this.comboBoxCoefficient.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBoxCoefficient_KeyPress);
            // 
            // comboBoxTaskWeight
            // 
            this.comboBoxTaskWeight.FormattingEnabled = true;
            this.comboBoxTaskWeight.Location = new System.Drawing.Point(54, 115);
            this.comboBoxTaskWeight.Name = "comboBoxTaskWeight";
            this.comboBoxTaskWeight.Size = new System.Drawing.Size(303, 24);
            this.comboBoxTaskWeight.TabIndex = 5;
            this.comboBoxTaskWeight.SelectedIndexChanged += new System.EventHandler(this.comboBoxTaskWeight_SelectedIndexChanged);
            this.comboBoxTaskWeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBoxTaskWeight_KeyPress);
            // 
            // dataGridViewThemese
            // 
            this.dataGridViewThemese.AllowUserToAddRows = false;
            this.dataGridViewThemese.AllowUserToDeleteRows = false;
            this.dataGridViewThemese.AllowUserToResizeColumns = false;
            this.dataGridViewThemese.AllowUserToResizeRows = false;
            this.dataGridViewThemese.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewThemese.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewThemese.Location = new System.Drawing.Point(49, 161);
            this.dataGridViewThemese.Name = "dataGridViewThemese";
            this.dataGridViewThemese.ReadOnly = true;
            this.dataGridViewThemese.RowHeadersVisible = false;
            this.dataGridViewThemese.RowHeadersWidth = 51;
            this.dataGridViewThemese.RowTemplate.Height = 24;
            this.dataGridViewThemese.Size = new System.Drawing.Size(308, 150);
            this.dataGridViewThemese.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(182, 142);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "Темы";
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(71, 367);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(125, 25);
            this.buttonOK.TabIndex = 8;
            this.buttonOK.Text = "Oк";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(202, 367);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(125, 25);
            this.buttonCancel.TabIndex = 9;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonAddTheme
            // 
            this.buttonAddTheme.Location = new System.Drawing.Point(143, 317);
            this.buttonAddTheme.Name = "buttonAddTheme";
            this.buttonAddTheme.Size = new System.Drawing.Size(125, 25);
            this.buttonAddTheme.TabIndex = 11;
            this.buttonAddTheme.Text = "Добавить тему";
            this.buttonAddTheme.UseVisualStyleBackColor = true;
            this.buttonAddTheme.Click += new System.EventHandler(this.buttonAddTheme_Click);
            // 
            // buttonDeleteTheme
            // 
            this.buttonDeleteTheme.Location = new System.Drawing.Point(274, 317);
            this.buttonDeleteTheme.Name = "buttonDeleteTheme";
            this.buttonDeleteTheme.Size = new System.Drawing.Size(125, 25);
            this.buttonDeleteTheme.TabIndex = 12;
            this.buttonDeleteTheme.Text = "Удалить тему";
            this.buttonDeleteTheme.UseVisualStyleBackColor = true;
            this.buttonDeleteTheme.Click += new System.EventHandler(this.buttonDeleteTheme_Click);
            // 
            // comboBoxTheme
            // 
            this.comboBoxTheme.FormattingEnabled = true;
            this.comboBoxTheme.Location = new System.Drawing.Point(3, 317);
            this.comboBoxTheme.Name = "comboBoxTheme";
            this.comboBoxTheme.Size = new System.Drawing.Size(134, 24);
            this.comboBoxTheme.TabIndex = 13;
            this.comboBoxTheme.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBoxTheme_KeyPress);
            // 
            // TaskForm
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(405, 404);
            this.Controls.Add(this.comboBoxTheme);
            this.Controls.Add(this.buttonDeleteTheme);
            this.Controls.Add(this.buttonAddTheme);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dataGridViewThemese);
            this.Controls.Add(this.comboBoxTaskWeight);
            this.Controls.Add(this.comboBoxCoefficient);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxCFID);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "TaskForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Задача";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewThemese)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox textBoxCFID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox comboBoxCoefficient;
        public System.Windows.Forms.ComboBox comboBoxTaskWeight;
        private System.Windows.Forms.DataGridView dataGridViewThemese;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonAddTheme;
        private System.Windows.Forms.Button buttonDeleteTheme;
        private System.Windows.Forms.ComboBox comboBoxTheme;
    }
}