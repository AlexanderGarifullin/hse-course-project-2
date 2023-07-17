using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FSPSystem
{
    internal class TaskCrud

    {
       static public  Dictionary<double, int> coefficiens = new Dictionary<double, int>();
       static public Dictionary<int, int> taskweight = new Dictionary<int, int>();
        static public Dictionary<string, int> themes = new Dictionary<string, int>();

        public static void UploadTasks(DataViewForm _DataViewForm)
        {
            using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
            {
                cn.Open();
                var sql = @"select Tasks.TaskID,   Tasks.CodeforcesTaskID as 'Codeforces ID', FirstDifficulties.Coefficient as 'Коэффициент', SecondDifficulties.TaskWeight as 'RP',
                          STRING_AGG(TRIM(Themes.ThemeName), ', ') WITHIN GROUP (ORDER BY Themes.ThemeName) AS 'Темы'
                         from Tasks 
                        left join FirstDifficulties on FirstDifficulties.FirstDifficultyID = Tasks.FirstDifficultyID
                        left join SecondDifficulties on SecondDifficulties.SecondDifficultyID = Tasks.SecondDifficultyID
                        left join TaskThemes on TaskThemes.TaskID = Tasks.TaskID 
                        left join Themes on Themes.ThemeID = TaskThemes.ThemeID 
                         GROUP BY 
                          Tasks.TaskID, Tasks.CodeforcesTaskID, FirstDifficulties.Coefficient, SecondDifficulties.TaskWeight
                          order by Tasks.CodeforcesTaskID asc, FirstDifficulties.Coefficient asc, SecondDifficulties.TaskWeight asc, 'Темы' asc
                            ";

                var cmd = new SqlCommand(sql, cn);

                SqlDataAdapter ds = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                _DataViewForm.dataTable = dataTable;

                ds.Fill(dataTable);
                _DataViewForm.dataGridView.DataSource = dataTable;
                _DataViewForm.dataGridView.Columns[0].Visible = false;
            }
            using (SqlConnection cn = new SqlConnection(MainForm.connectionString)) {
                cn.Open();
                var sql = @"select * from FirstDifficulties";
               var  cmd = new SqlCommand(sql, cn);

                SqlDataReader reader1 = cmd.ExecuteReader();

                Dictionary<double, int> coef = new Dictionary<double, int>();

                while (reader1.Read())
                {
                    int idcoef = reader1.GetInt32(0);
                    double thiscoef = reader1.GetDouble(1);
                    coef.Add(thiscoef, idcoef);
                }

                coefficiens = coef;
            }
            using (SqlConnection cn = new SqlConnection(MainForm.connectionString)) {
                cn.Open();
                var sql2 = @"select * from SecondDifficulties";
                var cmd2 = new SqlCommand(sql2, cn);

                SqlDataReader reader2 = cmd2.ExecuteReader();

                Dictionary<int, int> rp = new Dictionary<int, int>();

                while (reader2.Read())
                {
                    int idrpf = reader2.GetInt32(0);
                    int thisrp = reader2.GetInt32(1);
                    rp.Add(thisrp, idrpf);
                }

                taskweight = rp;
            }
            using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
            {
                cn.Open();
                var sql2 = @"select ThemeID, Trim(ThemeName) as 'Name' from Themes";
                var cmd2 = new SqlCommand(sql2, cn);

                SqlDataReader reader2 = cmd2.ExecuteReader();

                Dictionary<string, int> them = new Dictionary<string, int>();

                while (reader2.Read())
                {
                    int idrpf = reader2.GetInt32(0);
                    string theme = reader2.GetString(1);
                    them.Add(theme, idrpf);
                }

                themes = them;
            }
        }


        public static void AddTask(DataViewForm _DataViewForm)
        {
            TaskForm taskForm = new TaskForm();
            if (taskForm.ShowDialog() == DialogResult.OK)
            {
                int idtask = 0;
                using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
                {
                    cn.Open();
                    string sql;
                    SqlCommand cmd;
                    sql = @"INSERT INTO [dbo].[Tasks]
                           ([CodeforcesTaskID]
                           ,[FirstDifficultyID]
                           ,[SecondDifficultyID])
                    OUTPUT INSERTED.TaskID
                     VALUES
                           (@cfid
                           ,@firstdif
                           ,@secdif)";

                    cmd = new SqlCommand(sql, cn);

                    cmd.Parameters.AddWithValue("@cfid", taskForm.taskid);
                    cmd.Parameters.AddWithValue("@firstdif", coefficiens[taskForm.coef]);
                    cmd.Parameters.AddWithValue("@secdif", taskweight[taskForm.rp]);

                    //cmd.Parameters.AddWithValue("@lastname", teacherForm.LastName);
                    //cmd.Parameters.AddWithValue("@firstname", teacherForm.FirstName);
                    //cmd.Parameters.AddWithValue("@middlename", teacherForm.MiddleName);
                    //cmd.Parameters.AddWithValue("@position", teacherForm.Position);
                 
                    try
                    {   idtask = (int)cmd.ExecuteScalar();
                       
                        UploadTasks(_DataViewForm);
                    }
                    catch (SqlException)
                    {
                        MessageBox.Show($"Ошибка, в бд уже есть задача с ID {taskForm.taskid}!");
                        return;
                    }
                }
                using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
                {
                    cn.Open();
                    string sql;
                    SqlCommand cmd;
                    sql = @"INSERT INTO [dbo].[TaskThemes]
                           ([TaskID]
                           ,[ThemeID])
                     VALUES
                           ( @taskid
                           ,@themeid)";
                    foreach (var theme in taskForm.allthemes)
                    {

                        cmd = new SqlCommand(sql, cn);

                        cmd.Parameters.AddWithValue("@taskid", idtask);
                        cmd.Parameters.AddWithValue("@themeid", themes[theme]);

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
                }

                UploadTasks(_DataViewForm);


            }

            }


        public static void ChangeTask(DataViewForm _DataViewForm, int idchange)
        {
            TaskForm taskForm = new TaskForm(idchange);
            if (taskForm.ShowDialog() == DialogResult.OK)
            {
                using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
                {
                    cn.Open();
                    string sql;
                    SqlCommand cmd;
                    sql = @"
                            UPDATE [dbo].[Tasks]
                               SET [CodeforcesTaskID] =  @cftaskid
                                  ,[FirstDifficultyID] =  @firstdif
                                  ,[SecondDifficultyID] = @secdif
                             WHERE TaskID = @taskid";

                        cmd = new SqlCommand(sql, cn);

                        cmd.Parameters.AddWithValue("@taskid", idchange);
                        cmd.Parameters.AddWithValue("@cftaskid", taskForm.taskid);
                        cmd.Parameters.AddWithValue("@firstdif", coefficiens[taskForm.coef]);
                        cmd.Parameters.AddWithValue("@secdif", taskweight[taskForm.rp]);
                        try
                        {
                            cmd.ExecuteNonQuery();
                        }
                        catch (SqlException)
                        {
                            MessageBox.Show($"Задача с CodeforcesID ({taskForm.taskid}) уже есть!");
                            return;
                        }
                }

                using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
                {
                    cn.Open();
                    string sql;
                    SqlCommand cmd;
                    sql = @"
                           DELETE FROM [dbo].[TaskThemes]
                             WHERE TaskID = @taskid";

                        cmd = new SqlCommand(sql, cn);

                        cmd.Parameters.AddWithValue("@taskid", idchange);
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

                using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
                {
                    cn.Open();
                    string sql;
                    SqlCommand cmd;
                    sql = @"INSERT INTO [dbo].[TaskThemes]
                           ([TaskID]
                           ,[ThemeID])
                     VALUES
                           ( @taskid
                           ,@themeid)";
                    foreach (var theme in taskForm.allthemes)
                    {

                        cmd = new SqlCommand(sql, cn);

                        cmd.Parameters.AddWithValue("@taskid", idchange);
                        cmd.Parameters.AddWithValue("@themeid", themes[theme]);

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
                }



                UploadTasks(_DataViewForm);
            }
        }

        public static void DeleteTask(DataViewForm _DataViewForm, int idtodel)
        {
            using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
            {
                cn.Open();
                string sql;
                SqlCommand cmd;
                sql = @"DELETE FROM [dbo].[Tasks]
      WHERE TaskID = @taskID";

                cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@taskID", idtodel);

                //cmd.Parameters.AddWithValue("@lastname", teacherForm.LastName);
                //cmd.Parameters.AddWithValue("@firstname", teacherForm.FirstName);
                //cmd.Parameters.AddWithValue("@middlename", teacherForm.MiddleName);
                //cmd.Parameters.AddWithValue("@position", teacherForm.Position);

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
        }
    }
}
