using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FSPSystem
{
    internal class ContestsCrud
    {
        static public Dictionary<string, int> Contests = new Dictionary<string, int>();
        static public Dictionary<string, int> Participants = new Dictionary<string, int>();
        static public Dictionary<string, int> Tasks = new Dictionary<string, int>();

        public static void UploadContests(DataViewForm _DataViewForm)
        {
            Contests = new Dictionary<string, int>();
            Participants = new Dictionary<string, int>();
            Tasks = new Dictionary<string, int>();
            using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
            {
                cn.Open();
                var sql = @"select [ContestID], Trim([ContestName])  from Contests";
                var cmd = new SqlCommand(sql, cn);

                SqlDataReader reader1 = cmd.ExecuteReader();

                Dictionary<string, int> ols = new Dictionary<string, int>();

                while (reader1.Read())
                {
                    int idol = reader1.GetInt32(0);
                    string olname = reader1.GetString(1);
                    ols.Add(olname, idol);
                }
                Contests = ols;
            }
            using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
            {
                cn.Open();
                var sql = @"SELECT Participants.ParticipantID,
                       CONCAT(Trim(Students.CodeforcesNickname), ': ', CONCAT_WS(' ', Trim(Students.LastName), Trim(Students.FirstName), Trim(Students.MiddleName), 
                                 CASE WHEN TeamName IS NOT NULL THEN CONCAT('(', Trim(TeamName), ')') ELSE '' END)) AS 'Участник'
                FROM Participants 
                LEFT JOIN Teams ON Participants.TeamID = Teams.TeamID
                LEFT JOIN Students ON Participants.StudentID = Students.StudentID
                GROUP BY Participants.ParticipantID, Students.CodeforcesNickname, TeamName, Students.LastName, Students.FirstName, Students.MiddleName
                order by TeamName, CodeforcesNickname    ";
                var cmd = new SqlCommand(sql, cn);

                SqlDataReader reader1 = cmd.ExecuteReader();

                Dictionary<string, int> participants = new Dictionary<string, int>();

                while (reader1.Read())
                {
                    int idparticipant = reader1.GetInt32(0);
                    string participate = reader1.GetString(1);
                    if (participants.ContainsKey(participate))
                    {
   
                    }
                    else
                    {
                         participants.Add(participate, idparticipant);
                    }
                }
                Participants = participants;
            }
            using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
            {
                cn.Open();
                var sql = @"SELECT [TaskID]
                              ,Trim([CodeforcesTaskID])
                          FROM [FSPDB].[dbo].[Tasks]";
                var cmd = new SqlCommand(sql, cn);

                SqlDataReader reader1 = cmd.ExecuteReader();

                Dictionary<string, int> tasks = new Dictionary<string, int>();

                while (reader1.Read())
                {
                    int idol = reader1.GetInt32(0);
                    string taskid = reader1.GetString(1);
                    tasks.Add(taskid, idol);
                }
                Tasks = tasks;
            }
            using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
            {
                cn.Open();

                var sql = @"SELECT [ContestID]
                          ,Trim(ContestName) as 'Контест'
                          ,Duration as 'Длительность'
                          ,Trim(ParticipationType) as 'Тип участия'
                      FROM [FSPDB].[dbo].[Contests]
                    order by ContestName asc  , Duration asc, ParticipationType asc";

                var cmd = new SqlCommand(sql, cn);

                SqlDataAdapter ds = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                _DataViewForm.dataTable = dataTable;

                ds.Fill(dataTable);
                _DataViewForm.dataGridView.DataSource = dataTable;
                _DataViewForm.dataGridView.Columns[0].Visible = false;
            }
        }

        public static void UploadResults(DataViewForm _DataViewForm, int OlympiadID, string name)
        {
            ContestsResults contestsResults = new ContestsResults(OlympiadID, name);
            if (contestsResults.ShowDialog() == DialogResult.OK)
            {

            }
        }

        public static void UploadTasks(DataViewForm _DataViewForm, int contestID, string name)
        {
            TasksContestForm tasksContestForm = new TasksContestForm(contestID, name);
            if (tasksContestForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

            }
        }


        public static void DeleteContest(DataViewForm _DataViewForm, int idContest)
        {
            using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
            {
                cn.Open();
                var sql = @"delete from Contests where ContestID = @olid;";
                var cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@olid", idContest);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException)
                {
                    MessageBox.Show($"Ошибка!");
                }
            }
        }
        public static void AddContest(DataViewForm _DataViewForm)
        {
            AddContestForm addContestForm = new AddContestForm();
            if (addContestForm.ShowDialog() == DialogResult.OK)
            {
                int contestid = -1;
                using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
                {
                    cn.Open();
                    var sql = @"INSERT INTO [dbo].[Contests]
                           ([ContestName]
                           ,[Duration]
                           ,[ParticipationType])
                OUTPUT INSERTED.ContestID
                     VALUES
                           (@name
                           ,@duration
                           ,@type)";
                    var cmd = new SqlCommand(sql, cn);
                    cmd.Parameters.AddWithValue("@name", addContestForm.name);
                    cmd.Parameters.AddWithValue("@duration", addContestForm.duration);
                    cmd.Parameters.AddWithValue("@type", addContestForm.type);
                    try
                    {
                        contestid = (int)cmd.ExecuteScalar();
                    }
                    catch (SqlException)
                    {
                        MessageBox.Show($"Ошибка! Контест {addContestForm.name} уже есть в БД!");
                    }
                }

                Dictionary<string, int> task_contest_id = new Dictionary<string, int>();

                foreach (var cdiftask in addContestForm.tasks)
                {
                    using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
                    {
                        cn.Open();
                        var sql = @"INSERT INTO [dbo].[TaskContests]
                                           ([TaskID]
                                           ,[ContestID])
                                 OUTPUT INSERTED.TaskContestID
                                     VALUES
                                           (@taskid
                                           ,@contestid)
                                ";
                        var cmd = new SqlCommand(sql, cn);
                        cmd.Parameters.AddWithValue("@taskid", Tasks[cdiftask]);
                        cmd.Parameters.AddWithValue("@contestid", contestid);
                        try
                        {
                            int id_task_contest = (int)cmd.ExecuteScalar();
                            task_contest_id[cdiftask] = id_task_contest;
                        }
                        catch (SqlException)
                        {
                            MessageBox.Show($"Ошибка! Задача {cdiftask} уже связана с контестом  в БД!");
                        }
                    }
                }

                // пройти по всем строкам в DataGridView
                int indexrow = 0;
                foreach (DataGridViewRow row in addContestForm.dataGridViewParticipants.Rows)
                {
                    // получить значение из ячейки второго столбца (номер индекса = 1)
                    string cfNickname = row.Cells[1].Value.ToString();

                    // пройти по всем столбцам типа DataGridViewComboBoxColumn начиная с четвертого
                    for (int i = 3; i < addContestForm.dataGridViewParticipants.Columns.Count; i++)
                    {
                        DataGridViewComboBoxColumn comboBoxColumn = addContestForm.dataGridViewParticipants.Columns[i] as DataGridViewComboBoxColumn;
                        if (comboBoxColumn != null)
                        {
                            // получить значение из ячейки со столбцом типа DataGridViewComboBoxColumn для текущей строки
                            string comboBoxValue = row.Cells[i].FormattedValue.ToString();


                            string taskname = addContestForm.dataGridViewParticipants.Columns[i].Name;

                            using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
                            {
                                cn.Open();
                                var sql = @"INSERT INTO [dbo].[Solutions]
                                       ([TaskContestID]
                                       ,[ParticipantID]
                                       ,[SolutionStatus])
                                 VALUES
                                       (  @taslcontestid
                                       ,  @participantid
                                       , @solutionstatus)
                                    ";
                                var cmd = new SqlCommand(sql, cn);
                                cmd.Parameters.AddWithValue("@taslcontestid", task_contest_id[taskname]);
                                cmd.Parameters.AddWithValue("@participantid", Participants[addContestForm.cf_nick_participants[cfNickname]]);
                                cmd.Parameters.AddWithValue("@solutionstatus", comboBoxValue);
                                try
                                {
                                    cmd.ExecuteNonQuery();
                                }
                                catch (SqlException)
                                {
                                    MessageBox.Show($"Ошибка!");
                                }
                            }
                        }
                        indexrow++;
                    }
                }


                UploadContests(_DataViewForm);
            }
        }

        public static void ChangeContest(DataViewForm _DataViewForm, string contestid1, string name, string duration, string type)
        {
            AddContestForm addContestForm = new AddContestForm(contestid1, name, duration, type);
            if (addContestForm.ShowDialog() == DialogResult.OK)
            {   // Обновление полей контеста.
                using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
                {
                    cn.Open();
                    string sql;
                    SqlCommand cmd;
                    sql = @"UPDATE [dbo].[Contests]
                           SET [ContestName] = @name
                              ,[Duration] = @duration
                              ,[ParticipationType] = @type
                         WHERE ContestID = @contestid";

                    cmd = new SqlCommand(sql, cn);
                    cmd.Parameters.AddWithValue("@name", addContestForm.name);
                    cmd.Parameters.AddWithValue("@duration", addContestForm.duration);
                    cmd.Parameters.AddWithValue("@type", addContestForm.type);
                    cmd.Parameters.AddWithValue("@contestid", contestid1);

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException)
                    {
                        MessageBox.Show($"Ошибка! Контест с названием {addContestForm.name} уже есть в БД!");
                        return;
                    }
                }
                // Удаление связей со старыми задачами.
                using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
                {
                    cn.Open();
                    string sql;
                    SqlCommand cmd;
                    sql = @"DELETE FROM [dbo].[TaskContests]
                        WHERE ContestID = @contestid";

                    cmd = new SqlCommand(sql, cn);
                    cmd.Parameters.AddWithValue("@contestid", contestid1);

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException)
                    {
                        MessageBox.Show($"Ошибка!");
                        return;
                    }
                }

                // Добавление связей с новыми задачами.
                Dictionary<string, int> task_contest_id = new Dictionary<string, int>();
                foreach (var cdiftask in addContestForm.tasks)
                {
                    using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
                    {
                        cn.Open();
                        var sql = @"INSERT INTO [dbo].[TaskContests]
                                           ([TaskID]
                                           ,[ContestID])
                                 OUTPUT INSERTED.TaskContestID
                                     VALUES
                                           (@taskid
                                           ,@contestid)
                                ";
                        var cmd = new SqlCommand(sql, cn);
                        cmd.Parameters.AddWithValue("@taskid", Tasks[cdiftask]);
                        cmd.Parameters.AddWithValue("@contestid", contestid1);
                        try
                        {
                            int id_task_contest = (int)cmd.ExecuteScalar();
                            task_contest_id[cdiftask] = id_task_contest;
                        }
                        catch (SqlException)
                        {
                            MessageBox.Show($"Ошибка! Задача {cdiftask} уже связана с контестом  в БД!");
                        }
                    }
                }

                // Добавление решений.
                int indexrow = 0;
                MessageBox.Show(addContestForm.dataGridViewParticipants.Columns[1].Name);
                MessageBox.Show(addContestForm.dataGridViewParticipants.Columns[2].Name);
                MessageBox.Show(addContestForm.dataGridViewParticipants.Columns[3].Name);
                foreach (DataGridViewRow row in addContestForm.dataGridViewParticipants.Rows)
                {
                    // получить значение из ячейки второго столбца (номер индекса = 1)
                    MessageBox.Show(row.Cells[0].Value.ToString());
                    string cfNickname = row.Cells[1].Value.ToString();

                    // пройти по всем столбцам типа DataGridViewComboBoxColumn начиная с четвертого
                    for (int i = 3; i < addContestForm.dataGridViewParticipants.Columns.Count; i++)
                    {
                        MessageBox.Show(addContestForm.dataGridViewParticipants.Columns[i].Name);

                        DataGridViewComboBoxColumn comboBoxColumn = addContestForm.dataGridViewParticipants.Columns[i] as DataGridViewComboBoxColumn;

                        if (comboBoxColumn != null)
                        {
                            MessageBox.Show(comboBoxColumn.Name);
                            // получить значение из ячейки со столбцом типа DataGridViewComboBoxColumn для текущей строки
                            string comboBoxValue = "Не решена";
                            if (row.Cells[i].FormattedValue != null)
                            {
                                comboBoxValue = row.Cells[i].FormattedValue.ToString();
                            }

                            MessageBox.Show(comboBoxValue);

                            string taskname = addContestForm.dataGridViewParticipants.Columns[i].Name;

                            using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
                            {
                                cn.Open();
                                var sql = @"INSERT INTO [dbo].[Solutions]
                                       ([TaskContestID]
                                       ,[ParticipantID]
                                       ,[SolutionStatus])
                                 VALUES
                                       (  @taslcontestid
                                       ,  @participantid
                                       , @solutionstatus)
                                    ";
                                var cmd = new SqlCommand(sql, cn);
                                cmd.Parameters.AddWithValue("@taslcontestid", task_contest_id[taskname]);
                                cmd.Parameters.AddWithValue("@participantid", Participants[addContestForm.cf_nick_participants[cfNickname]]);
                                cmd.Parameters.AddWithValue("@solutionstatus", comboBoxValue);
                                try
                                {
                                    cmd.ExecuteNonQuery();
                                }
                                catch (SqlException)
                                {
                                    MessageBox.Show($"Ошибка!");
                                }
                            }
                        }
                        indexrow++;
                    }
                }

                UploadContests(_DataViewForm);
            }

        }
    }
}
