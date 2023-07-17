using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FSPSystem
{
    public partial class TaskForm : Form
    {
        public string taskid;
        public double coef;
        public int rp;
        public  List<string> themes = new List<string>();
        public List<string> allthemes = new List<string>();
        public List<string> deletedThemes = new List<string>();
        DataTable dataTable = new DataTable();
        public bool newtask = true;

        public TaskForm()
        {
            InitializeComponent();
            foreach (KeyValuePair<double, int> pair in TaskCrud.coefficiens)
            {
                comboBoxCoefficient.Items.Add(pair.Key);
            }
            foreach (KeyValuePair<int, int> pair in TaskCrud.taskweight)
            {
                comboBoxTaskWeight.Items.Add(pair.Key);
            }

            foreach (KeyValuePair<string, int> pair in TaskCrud.themes)
            {
                comboBoxTheme.Items.Add(pair.Key);
            }
            dataTable.Columns.Add("Тема");
            dataGridViewThemese.DataSource = dataTable;
        }

        public TaskForm(int idTask) : this()
        {      
            using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
            {
                cn.Open();
                string sql;
                SqlCommand cmd;
                sql = @"select Trim(ThemeName) as tname from TaskThemes 
                    left join Tasks on TaskThemes.TaskID = Tasks.TaskID
                    left join Themes on TaskThemes.ThemeID = Themes.ThemeID
                    where Tasks.TaskID = @taskid order by ThemeName  ";

                cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@taskid", idTask);

                try
                {
                    SqlDataReader reader2 = cmd.ExecuteReader();

                    while (reader2.Read())
                    {
                        string theme = reader2.GetString(0);
                        allthemes.Add(theme);
                        DataRow newRow = dataTable.NewRow();
                        newRow["Тема"] = theme;
                        dataTable.Rows.Add(newRow);
                    }
                }
                catch (SqlException)
                {
                    MessageBox.Show($"Ошибка!");
                    return;
                }
            }
            using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
            {
                cn.Open();
                string sql;
                SqlCommand cmd;
                sql = @"select Trim(CodeforcesTaskID) as cf, Coefficient, TaskWeight from Tasks 
                    left join FirstDifficulties on FirstDifficulties.FirstDifficultyID = Tasks.FirstDifficultyID
                    left join SecondDifficulties on SecondDifficulties.SecondDifficultyID = Tasks.SecondDifficultyID
                    where TaskID = @taskid ";

                cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@taskid", idTask);

                try
                {
                    SqlDataReader reader2 = cmd.ExecuteReader();

                    while (reader2.Read())
                    {
                        string cdid = reader2.GetString(0);
                        double coef = reader2.GetDouble(1);
                        int rp = reader2.GetInt32(2);

                        textBoxCFID.Text = cdid;
                        comboBoxCoefficient.Text = coef.ToString();
                        comboBoxTaskWeight.Text = rp.ToString();
             
                    }
                }
                catch (SqlException)
                {
                    MessageBox.Show($"Ошибка!");
                    return;
                }
            }
        }


        private void comboBoxTaskWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBoxCoefficient_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBoxTheme_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        public bool ValidateInput()
        {
            if (comboBoxCoefficient.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали коэффициент!");
                return false;
            }
            if (comboBoxTaskWeight.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали вес задачи (RP)");
                return false;
            }
            string taskid = textBoxCFID.Text.Trim();
            string patterntack = @"^[1-9]\d*[A-Za-z]$";
            bool ismatchtask = Regex.IsMatch(taskid, patterntack);
            if (taskid == "")
            {
                MessageBox.Show("Вы не ввели данные в поле Codeforces ID задачи");
                return false;
            }
            if (!ismatchtask)
            {
                MessageBox.Show($"ID задачи не может быть {taskid}, ID - строка вида число + латинская буква");
                return false;
            }
            return true;
        }


        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                coef = Convert.ToDouble(comboBoxCoefficient.SelectedItem.ToString());
                rp = Convert.ToInt32(comboBoxTaskWeight.SelectedItem.ToString());
                taskid = textBoxCFID.Text.Trim();
                char lastChar = char.ToUpper(taskid[taskid.Length - 1]);
                taskid = taskid.Substring(0, taskid.Length - 1) + lastChar;
                DialogResult = DialogResult.OK;
            }
        }

        private void buttonAddTheme_Click(object sender, EventArgs e)
        {
            if (comboBoxTheme.SelectedIndex != -1)
            {
                string theme = comboBoxTheme.SelectedItem.ToString();

                foreach (var otheme in allthemes)
                {
                    if (String.Compare(otheme, theme) == 0)
                    {
                        MessageBox.Show($"У задачи уже есть тема \"{theme}\"!");
                        return;
                    }
                }

                DataRow newRow = dataTable.NewRow();
                newRow["Тема"] = theme;
                dataTable.Rows.Add(newRow);
                themes.Add(theme);
                allthemes.Add(theme);
            }
            else
            {
                MessageBox.Show("Вы не выбрали тему!");
            }
        }

        private void buttonDeleteTheme_Click(object sender, EventArgs e)
        {
            if (dataGridViewThemese.SelectedCells.Count > 0)
            {       
                foreach (DataGridViewCell cell in dataGridViewThemese.SelectedCells)
                {
                    int selectedRowIndex = cell.RowIndex;
                    allthemes.Remove(dataGridViewThemese.Rows[cell.RowIndex].Cells["Тема"].Value.ToString());
                   
                    deletedThemes.Add(dataGridViewThemese.Rows[cell.RowIndex].Cells["Тема"].Value.ToString());
                    dataGridViewThemese.Rows.RemoveAt(selectedRowIndex);
                }
            }
            else
            {
                MessageBox.Show("Вы не выбрали тему!");
            }
        }

        private void comboBoxTaskWeight_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
