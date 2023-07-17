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
    public partial class ContestsResults : Form
    {
        DataTable dataTable = new DataTable();

        public ContestsResults(int contestID, string name)
        {
            InitializeComponent();
            label1.Text = name;
           dataGridView1.RowHeadersVisible = false;

            dataTable.Columns.Add("ФИО");
            dataTable.Columns.Add("CF ник");
            dataTable.Columns.Add("Группа");

   
            using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
            {
                cn.Open();
                string sql;
                SqlCommand cmd;
                sql = @"select Tasks.TaskID, Trim(CodeforcesTaskID) as 'CFID' from TaskContests 
                    left join Tasks on Tasks.TaskID = TaskContests.TaskID
                    where TaskContests.ContestID = @contestID";

                cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@contestID", contestID);

                try
                {
                    SqlDataReader reader2 = cmd.ExecuteReader();

                    while (reader2.Read())
                    {
                        int id = reader2.GetInt32(0);
                        string cdif = reader2.GetString(1);
  
                        dataTable.Columns.Add(cdif);
                    }
                }
                catch (SqlException)
                {
                    MessageBox.Show($"Ошибка!");
                    return;
                }
            }

            dataGridView1.DataSource = dataTable;
            dataGridView1.Columns[0].Frozen = true;
            dataGridView1.Columns[1].Frozen = true;
            dataGridView1.Columns[2].Frozen = true;
            LoadStudents(contestID, name);
        }
        private void LoadStudents(int contestID, string name)
        {
            using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
            {
                cn.Open();
                string sql;
                SqlCommand cmd;
                //sql = @"SELECT CONCAT(Trim(Students.LastName), ' ', Trim(Students.FirstName), ' ', Trim(Students.MiddleName)) AS 'ФИО', 
                //           Trim(Students.CodeforcesNickname) AS 'CF ник', 
                //           Trim(StudyGroups.GroupName) AS 'Группа',
                //           Trim(Tasks.CodeforcesTaskID) AS 'CodeforcesTaskID', 
                //           COALESCE(Trim(Solutions.SolutionStatus), 'Не решена') AS 'SolutionStatus'
                //    FROM Tasks
                //    CROSS JOIN Participants
                //    LEFT JOIN TaskContests ON TaskContests.TaskID = Tasks.TaskID AND TaskContests.ContestID = @contestid
                //    LEFT JOIN Solutions ON Solutions.TaskContestID = TaskContests.TaskContestID AND Solutions.ParticipantID = Participants.ParticipantID
                //    LEFT JOIN Students ON Participants.StudentID = Students.StudentID
                //    LEFT JOIN Teams ON Participants.TeamID = Teams.TeamID
                //    LEFT JOIN StudyGroups ON Students.GroupID = StudyGroups.GroupID
                //    ORDER BY Students.LastName, Students.FirstName, Students.MiddleName, Tasks.CodeforcesTaskID
                //    ";


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

                cmd.Parameters.AddWithValue("@contestid", contestID);

                try
                {
                    SqlDataReader reader2 = cmd.ExecuteReader();
                    string lastname = "";
                    string oldcfnick  = "";
                    string oldgroupname = "";
                    DataRow newRow = dataTable.NewRow();
                    int cnt = 0;
                    while (reader2.Read())
                    {
                        cnt++;
                       string curname = reader2.GetString(0);
                       
                        if (curname == null || curname == "") return;
                    
                        string cfnick = reader2.GetString(1);
                        string groupname = reader2.GetString(2);
                        string cftask = reader2.GetString(3);
                        string status = reader2.GetString(4);
                      
                        if (curname != lastname && lastname != "")
                        {
                            newRow["ФИО"] = lastname;
                            newRow["Группа"] = oldgroupname;
                            newRow["CF ник"] = oldcfnick;

                            foreach (DataColumn colname in dataTable.Columns)
                            {
                                if (newRow[colname.ColumnName] is null)
                                {
                                    newRow[colname.ColumnName] = "Не решена";
                                }
                            }

                            dataTable.Rows.Add(newRow);
                            newRow = dataTable.NewRow();
                            newRow[cftask] = status;
                        }
                        else
                        {
                            newRow[cftask] = status;
                        }
                        lastname = curname;
                        oldcfnick = cfnick;
                        oldgroupname = groupname;
                    }
                    if (cnt == 0) return;
                    newRow["ФИО"] = lastname;
                    newRow["Группа"] = oldgroupname;
                    newRow["CF ник"] = oldcfnick;
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        if (newRow[i] == DBNull.Value)
                        {
                            newRow[i] = "Не решена";
                        }
                    }
              
                    dataTable.Rows.Add(newRow);
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        for (int j = 0; j < dataGridView1.Columns.Count; j++)
                        {
                            if (dataGridView1.Rows[i].Cells[j].Value == null || dataGridView1.Rows[i].Cells[j].Value.ToString() == "")
                            {
                                dataGridView1.Rows[i].Cells[j].Value = "Не решена";
                            }
                        }
                    }
                }
                catch (SqlException)
                {
                    MessageBox.Show($"Ошибка!");
                    return;
                }
            }
        }

        private void dataGridView1_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {

        }
    }
}
