using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FSPSystem
{
    public partial class AddContestForm : Form
    {
        public List<string> tasks = new List<string>();
        public Dictionary<string, string> participants = new Dictionary<string, string>();
        public Dictionary<string, string> cf_nick_participants = new Dictionary<string, string>();
         Dictionary<string, List<KeyValuePair<string, string>>> name_task_id = new Dictionary<string, List<KeyValuePair<string, string>>>();
        public string name;
        public string duration;
        public string type;
        DataTable dataTableTasks = new DataTable();
        public  DataTable dataTableResults = new DataTable();
        public AddContestForm()
        {
            InitializeComponent();
            foreach (KeyValuePair<string, int> pair in ContestsCrud.Participants)
            {
                comboBoxParticipants.Items.Add(pair.Key);
            }
            foreach (KeyValuePair<string, int> pair in ContestsCrud.Tasks)
            {
                comboBoxTasks.Items.Add(pair.Key);
            }
            comboBoxParticipateType.Items.Add("Командный");
            comboBoxParticipateType.Items.Add("Индивидуальный");
      
            dataTableTasks.Columns.Add("CD ID");

            dataGridViewTasks.DataSource = dataTableTasks;

            dataTableResults.Columns.Add("ФИО");
            dataTableResults.Columns.Add("CF ник");
            dataTableResults.Columns.Add("Группа");
            dataTableResults.Columns[0].ReadOnly = true;
            dataTableResults.Columns[1].ReadOnly = true;
            dataTableResults.Columns[2].ReadOnly = true;

            dataGridViewParticipants.DataSource = dataTableResults;

        }

        public AddContestForm(string contestid, string name, string duration, string type) : this()
        {
            textBoxDuration.Text = duration;
            textBoxName.Text = name;
            comboBoxParticipateType.Text = type;
            comboBoxParticipateType.SelectedItem = type;
            participants = new Dictionary<string, string>();
            cf_nick_participants = new Dictionary<string, string>();
            using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
            {
                cn.Open();
                var sql = @"select Trim(CodeforcesTaskID) as 'cf'  from TaskContests
                left join Tasks on Tasks.TaskID = TaskContests.TaskID
                where ContestID = @cfid";
                var cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@cfid", contestid);

                SqlDataReader reader1 = cmd.ExecuteReader();

                while (reader1.Read())
                {
                    string task1 = reader1.GetString(0);
                    tasks.Add(task1);

                    DataGridViewComboBoxColumn comboBoxColumn = new DataGridViewComboBoxColumn();

                    comboBoxColumn.HeaderText = task1;
                    comboBoxColumn.Name = task1;

                    comboBoxColumn.Items.AddRange("Решена", "Не решена", "Дорешена");
                    comboBoxColumn.DefaultCellStyle.NullValue = "Не решена";
        
                    
                    dataGridViewParticipants.Columns.Add(comboBoxColumn);

                    DataRow newRow = dataTableTasks.NewRow();

                    newRow["CD ID"] = task1;
                    dataTableTasks.Rows.Add(newRow);
                }
            }

            using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
            {
                cn.Open();
                var sql = @" SELECT  CONCAT(Trim(Students.CodeforcesNickname), ': ', CONCAT_WS(' ', Trim(Students.LastName), Trim(Students.FirstName), Trim(Students.MiddleName), 
                                 CASE WHEN TeamName IS NOT NULL THEN CONCAT('(', Trim(TeamName), ')') ELSE '' END)) AS 'Участник',
								 Trim(CodeforcesNickname), CONCAT(Trim(Students.LastName), ' ', Trim(Students.FirstName), ' ', Trim(Students.MiddleName)) AS 'ФИО',
								 GroupName
                        FROM Participants
                        LEFT JOIN Solutions ON Solutions.ParticipantID = Participants.ParticipantID
                        LEFT JOIN TaskContests ON TaskContests.TaskContestID = Solutions.TaskContestID
                        LEFT JOIN Students ON Participants.StudentID = Students.StudentID
                        LEFT JOIN StudyGroups ON Students.GroupID = StudyGroups.GroupID
                        LEFT JOIN Teams ON Teams.TeamID = Participants.TeamID
                        WHERE TaskContests.ContestID = @contestid
                        GROUP BY Students.LastName, Students.FirstName, Students.MiddleName, TeamName, Students.CodeforcesNickname, Participants.ParticipantID, GroupName
                                 ";
                var cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@contestid", contestid);
                SqlDataReader reader1 = cmd.ExecuteReader();


                while (reader1.Read())
                {

                    string participate = reader1.GetString(0);
                    string cfnick = reader1.GetString(1);  
                    string fio = reader1.GetString(2);
                    string group = reader1.GetString(3);

                    participants.Add(participate, cfnick);
                    cf_nick_participants.Add(cfnick, participate);

                    DataRow row = dataTableResults.NewRow();
                    row["ФИО"] = fio;
                    row["CF ник"] = cfnick;
                    row["Группа"] = group;

                    dataTableResults.Rows.Add(row);

                }

            }


            LoadResults(contestid);
        }
        private void LoadResults(string contestid)
        {
            using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
            {
                cn.Open();
                string sql;
                SqlCommand cmd;

                sql = @"SELECT CONCAT(Trim(Students.LastName), ' ', Trim(Students.FirstName), ' ', Trim(Students.MiddleName)) AS 'ФИО', 
                                   Trim(Students.CodeforcesNickname) AS 'CF ник', 
                                   Trim(StudyGroups.GroupName) AS 'Группа',
                                   TasksCFID.CodeforcesTaskID AS 'CodeforcesTaskID', 
                                   COALESCE(Trim(Solutions.SolutionStatus), 'Не решена') AS 'SolutionStatus'
                            FROM Participants
                            LEFT JOIN Solutions ON Solutions.ParticipantID = Participants.ParticipantID
                            LEFT JOIN TaskContests ON TaskContests.TaskContestID = Solutions.TaskContestID
                            LEFT JOIN Students ON Participants.StudentID = Students.StudentID
                            LEFT JOIN StudyGroups ON Students.GroupID = StudyGroups.GroupID
                            LEFT JOIN (
                                SELECT Tasks.TaskID, Trim(Tasks.CodeforcesTaskID) as 'CodeforcesTaskID' 
                                FROM TaskContests 
                                LEFT JOIN Tasks ON Tasks.TaskID = TaskContests.TaskID
                                WHERE TaskContests.ContestID = @contestid
                            ) TasksCFID ON TaskContests.TaskID = TasksCFID.TaskID
                            WHERE TaskContests.ContestID = @contestid
                            ORDER BY Students.LastName, Students.FirstName, Students.MiddleName, TasksCFID.CodeforcesTaskID
                             ";

                cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@contestid", contestid);

                try
                {
                    SqlDataReader reader2 = cmd.ExecuteReader();
                    string lastname = "";
                    string oldcfnick = "";
                    string oldgroupname = "";

                    while (reader2.Read())
                    {
                        string curname = reader2.GetString(0);
                        string cfnick = reader2.GetString(1);
                        string groupname = reader2.GetString(2);
                        string cftask = reader2.GetString(3);
                        string status = reader2.GetString(4);

                        int index = -1;
                        for (int i = 0; i < dataGridViewParticipants.Rows.Count ; i++)
                        {
                            if (dataGridViewParticipants.Rows[i].Cells["ФИО"].Value.ToString() == curname)
                            {
                                DataGridViewComboBoxCell cc = new DataGridViewComboBoxCell();
                                cc.Items.AddRange("Решена", "Не решена", "Дорешена");
                                cc.Value = status;

                              dataGridViewParticipants.Rows[i].Cells[cftask].Value = status;

                            }
                        }

                        //if (!name_task_id.ContainsKey(curname))
                        //{
                        //    name_task_id[curname] = new List<KeyValuePair<string, string>>();
                        //}
                        //var pairs = name_task_id[curname];

                        //pairs.Add(new KeyValuePair<string, string>(cftask, status));
                    }
                }
                catch (SqlException)
                {
                    MessageBox.Show($"Ошибка!");
                    return;
                }

    
            }
        }

        private void comboBoxParticipantType_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBoxParticipants_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled   = true;
        }

        public bool ValidateInput()
        {
            name = textBoxName.Text.Trim();
            duration = textBoxDuration.Text.Trim();
            if (name == "")
            {
                MessageBox.Show("Вы не ввели название контеста!");
                return false;
            }
            int dur = 0;
            if (int.TryParse(duration, out dur))
            {
                if (!(dur >= 60 && dur <= 300))
                {
                    MessageBox.Show("Ошибка! Длительность может быть лишь от 60 минут до 300 минут включительно!");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Ошибка! Вы ввели не число в поле 'Длительность'!");
                return false;
            }
            if (comboBoxParticipateType.SelectedItem == null)
            {
                MessageBox.Show("Ошибка! Вы не выбрали тип контеста!");
                return false;
            }
            type = comboBoxParticipateType.Text;
            return true;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void buttonAddTask_Click(object sender, EventArgs e)
        {
            if (comboBoxTasks.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали задачу!");
                return;
            }
            string taskid = comboBoxTasks.SelectedItem.ToString();

            foreach (var task in tasks)
            {
                if (String.Compare(task, taskid) == 0)
                {
                    MessageBox.Show($"В контесте уже есть здача \"{taskid}\"!");
                    return;
                }
            }

            DataRow newRow = dataTableTasks.NewRow();

            DataGridViewComboBoxColumn comboBoxColumn = new DataGridViewComboBoxColumn();

            comboBoxColumn.HeaderText = taskid;
            comboBoxColumn.Name = taskid;
            comboBoxColumn.Items.AddRange("Решена", "Не решена", "Дорешена");
            comboBoxColumn.DefaultCellStyle.NullValue = "Не решена";
            // comboBoxColumn.DataSource = new List<string>() { "Решена", "Не решена", "Дорешена" };
            dataGridViewParticipants.Columns.Add(comboBoxColumn);

            //dataTableResults.Columns.Add(taskid);


            newRow["CD ID"] = taskid;
            dataTableTasks.Rows.Add(newRow);
            tasks.Add(taskid);
        }

        private void buttonDeleteTask_Click(object sender, EventArgs e)
        {

            if (dataGridViewTasks.SelectedCells.Count > 0)
            {
                foreach (DataGridViewCell cell in dataGridViewTasks.SelectedCells)
                {
                    int selectedRowIndex = cell.RowIndex;
                    tasks.Remove(dataGridViewTasks.Rows[cell.RowIndex].Cells["CD ID"].Value.ToString());

                    // dataTableResults.Columns.Remove(dataGridViewTasks.Rows[cell.RowIndex].Cells["CD ID"].Value.ToString());

                    dataGridViewParticipants.Columns.Remove(dataGridViewTasks.Rows[cell.RowIndex].Cells["CD ID"].Value.ToString());

                    dataGridViewTasks.Rows.RemoveAt(selectedRowIndex);
                }
            }
            else
            {
                MessageBox.Show("Вы не выбрали задачу!");
            }
        }

        private void buttonAddParticipant_Click(object sender, EventArgs e)
        {
            if (comboBoxParticipants.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали участника!");
                return;
            }
            string part = comboBoxParticipants.SelectedItem.ToString();
            string cgnickpart = "";
            int idpart = ContestsCrud.Participants[part];
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
                        cgnickpart = reader2.GetString(0);
                    }
                }
                catch (SqlException)
                {
                    MessageBox.Show($"Ошибка!");
                    return;
                }
            }




            foreach (var participant in participants)
            {
                if (String.Compare(cgnickpart, participant.Value) == 0)
                {
                    MessageBox.Show($"В контесте уже есть участник \"{part}\"!");
                    return;
                }
            }

            participants.Add(part, cgnickpart);

            
            string name = "";
            string group = "";
            string cfnick = "";

            using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
            {
                cn.Open();
                string sql;
                SqlCommand cmd;
                sql = @"SELECT
                          STUFF((
                            SELECT CONCAT(', ', CONCAT_WS(' ', Trim(LastName), Trim(FirstName), Trim(MiddleName)))
                            FROM Participants
                            LEFT JOIN Students ON Students.StudentID = Participants.StudentID
                            WHERE ParticipantID = @participantid
                            FOR XML PATH('')
                          ), 1, 2, '') AS 'ФИО',
                          Trim(CodeforcesNickname) as 'CF ник',
                          Trim(GroupName) as 'Группа'
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
                         name = reader2.GetString(0);
                        cfnick = reader2.GetString(1);
                        group = reader2.GetString(2);
                    }
                }
                catch (SqlException)
                {
                    MessageBox.Show($"Ошибка!");
                    return;
                }
            }

            DataRow row = dataTableResults.NewRow();
            row["ФИО"] = name;
            row["CF ник"] = cfnick;
            row["Группа"] = group;
            cf_nick_participants.Add(cfnick, part);


            dataTableResults.Rows.Add(row);
        }

        private void buttonDeleteParticipant_Click(object sender, EventArgs e)
        {
            if (dataGridViewParticipants.SelectedCells.Count > 0)
            {
                foreach (DataGridViewCell cell in dataGridViewParticipants.SelectedCells)
                {
                    int selectedRowIndex = cell.RowIndex;
                    participants.Remove(cf_nick_participants[dataGridViewParticipants.Rows[cell.RowIndex].Cells["CF ник"].Value.ToString()]);
                    cf_nick_participants.Remove(dataGridViewParticipants.Rows[cell.RowIndex].Cells["CF ник"].Value.ToString());

                    // dataTableResults.Columns.Remove(dataGridViewTasks.Rows[cell.RowIndex].Cells["CD ID"].Value.ToString());
                    dataGridViewParticipants.Rows.RemoveAt(selectedRowIndex);
                }
            }
            else
            {
                MessageBox.Show("Вы не выбрали участника!");
            }
        }
    }
}
