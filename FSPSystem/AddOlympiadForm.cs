using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FSPSystem
{
    public partial class AddOlympiadForm : Form
    {
        public List<string> participants = new List<string>();
        public Dictionary<string, string> participants_cf_nick = new Dictionary<string, string>();
        public Dictionary<string, string> participants_cnt_solved = new Dictionary<string, string>();
        public string name;
        public string typeparticipant;
        public string bp;
        public string bppertask;
        public string year;
        DataTable dataTable = new DataTable();
        public AddOlympiadForm()
        {
            InitializeComponent();
            comboBoxParticipateType.Items.Add("Командная");
            comboBoxParticipateType.Items.Add("Индивидуальная");
            foreach (KeyValuePair<string, int> pair in OlympiadsCrud.Participants)
            {
               comboBoxParticipants.Items.Add(pair.Key);
            }
            dataTable.Columns.Add("Участник");
            dataTable.Columns.Add("Количество задач");
            dataGridView1.DataSource = dataTable;
        
            dataGridView1.Columns[0].ReadOnly = true;
        }

        public AddOlympiadForm(string OlympiadID, string name, string year, string bp, string bpperyear, string type) : this()
        {
            textBoxBP.Text = bp;
            textBoxBPperTask.Text = bpperyear;
            textBoxName.Text = name;
            textBoxYear.Text = year;
            comboBoxParticipateType.Text = type;

            using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
            {
                cn.Open();
                var sql = @"SELECT Participants.ParticipantID,
                       CONCAT(Trim(Students.CodeforcesNickname), ': ', CONCAT_WS(' ', Trim(Students.LastName), Trim(Students.FirstName), Trim(Students.MiddleName), 
                                 CASE WHEN TeamName IS NOT NULL THEN CONCAT('(', Trim(TeamName), ')') ELSE '' END)) AS 'Участник', ParticipantOlympiads.SolvedProblemsCount, Students.CodeforcesNickname
                FROM Participants 
                LEFT JOIN Teams ON Participants.TeamID = Teams.TeamID
                LEFT JOIN Students ON Participants.StudentID = Students.StudentID
				left join ParticipantOlympiads on ParticipantOlympiads.ParticipantID = Participants.ParticipantID
				where ParticipantOlympiads.OlympiadID = 3008
                GROUP BY Participants.ParticipantID, Students.CodeforcesNickname, TeamName, Students.LastName, Students.FirstName, Students.MiddleName, ParticipantOlympiads.SolvedProblemsCount, Students.CodeforcesNickname
                order by TeamName, CodeforcesNickname 	 ";
                var cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@idol", OlympiadID);
                SqlDataReader reader1 = cmd.ExecuteReader();



                while (reader1.Read())
                {
                    string participate = reader1.GetString(1);
                    string cfnick = reader1.GetString(3);
                    int cnt = reader1.GetInt32(2);
                    participants.Add(participate);
                    participants_cnt_solved[participate] = cnt.ToString();
                    DataRow newRow = dataTable.NewRow();
                    newRow["Участник"] = participate;
                    newRow["Количество задач"] = cnt;
                    participants_cf_nick.Add(participate, cfnick);
                    dataTable.Rows.Add(newRow);
                }
            }

        }


        private void comboBoxParticipateType_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBoxParticipants_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        public bool ValidateInput()
        {
             name = textBoxName.Text.Trim();
             year = textBoxYear.Text.Trim();
             bp = textBoxBP.Text.Trim();
             bppertask = textBoxBPperTask.Text.Trim();

            if (comboBoxParticipateType.SelectedItem == null)
            {
                MessageBox.Show("Ошибка! Вы не выбрали тип олимпиады!");
                return false;
            }
            if (name == "")
            {
                MessageBox.Show("Ошибка! Вы не ввели название олимпиады!");
                return false;
            }
            if (year == "")
            {
                MessageBox.Show("Ошибка! Вы не ввели год проведения олимпиады!");
                return false;
            }
            if (bp == "")
            {
                MessageBox.Show("Ошибка! Вы не ввели BP за олимпиаду!");
                return false;
            }
            if (bppertask == "")
            {
                MessageBox.Show("Ошибка! Вы не ввели BP за каждую задачу олимпиады!");
                return false;
            }

            int year1, bp1, bppertask1;

            if (!int.TryParse(year, out year1) || !(year1 >= 2021 && year1 <= 2023))
            {
                MessageBox.Show("Ошибка! Год может принимать значения лишь от 2021 до 2023!");
                return false;
            }

            if (!int.TryParse(bp, out bp1) || bp1 <= 0)
            {
                MessageBox.Show("Ошибка! BP может быть лишь положительным числом!");
                return false;
            }

            if (!int.TryParse(bppertask, out bppertask1) || bppertask1 <= 0)
            {
                MessageBox.Show("Ошибка! BP за задачу может быть лишь положительным числом!");
                return false;
            }

            
            return true;
        }


        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                typeparticipant = comboBoxParticipateType.Text;
                DialogResult = DialogResult.OK;
            }
        }

        private void buttonAddParticipate_Click(object sender, EventArgs e)
        {
            if (comboBoxParticipants.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали участника!");
                return;
            }
            else
            {
                string participant1 = comboBoxParticipants.SelectedItem.ToString();

                string cfnick = "";
                int idpart = OlympiadsCrud.Participants[participant1];

                using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
                {
                    cn.Open();
                    string sql;
                    SqlCommand cmd;
                    sql = @"SELECT
                          CodeforcesNickname as 'CF ник'
                        FROM Participants
                        LEFT JOIN Students ON Students.StudentID = Participants.StudentID
                        LEFT JOIN StudyGroups ON StudyGroups.GroupID = Students.GroupID
                        WHERE ParticipantID = @participantid
                        GROUP BY CodeforcesNickname, GroupName
                        ";

                    cmd = new SqlCommand(sql, cn);

                    cmd.Parameters.AddWithValue("@participantid", idpart);

                    try
                    {
                        SqlDataReader reader2 = cmd.ExecuteReader();

                        while (reader2.Read())
                        {
                            cfnick = reader2.GetString(0);
                        }
                    }
                    catch (SqlException)
                    {
                        MessageBox.Show($"Ошибка!");
                        return;
                    }
                }




                foreach (var part in participants_cf_nick)
                {
                    if (String.Compare(cfnick, part.Value) == 0)
                    {
                        MessageBox.Show($"В таблице уже есть информацие о \"{participant1}\"!");
                        return;
                    }

                }


                participants_cf_nick.Add(participant1, cfnick);

                DataRow newRow = dataTable.NewRow();
                newRow["Участник"] = participant1;
                newRow["Количество задач"] = "0";
                participants_cnt_solved[participant1]  = "0";
                dataTable.Rows.Add(newRow);
                participants.Add(participant1);
            }
        }

        private void buttonDeleteParticipant_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                {
                    int selectedRowIndex = cell.RowIndex;
                    participants_cf_nick.Remove(dataGridView1.Rows[cell.RowIndex].Cells["Участник"].Value.ToString());
                    participants.Remove(dataGridView1.Rows[cell.RowIndex].Cells["Участник"].Value.ToString());
                    participants_cnt_solved.Remove(dataGridView1.Rows[cell.RowIndex].Cells["Участник"].Value.ToString());
                    dataGridView1.Rows.RemoveAt(selectedRowIndex);
                }
            }
            else
            {
                MessageBox.Show("Вы не выбрали студента!");
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.RowIndex >= 0)
            {
                int result;
                if (!int.TryParse(dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString(), out result) || result < 0)
                {
                    MessageBox.Show("Введите неотрицательное целое число!");
                    dataGridView1[e.ColumnIndex, e.RowIndex].Value = "0";
                }
                else
                {
                    participants_cnt_solved[dataGridView1.Rows[e.RowIndex].Cells["Участник"].Value.ToString()] = result.ToString();
                  //  participants_cnt_solved[dataGridView1[0, e.RowIndex].Value.ToString()] = "0";
                }
            }
        }

        private void comboBoxParticipateType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
