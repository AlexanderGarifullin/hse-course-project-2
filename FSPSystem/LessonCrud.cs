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
    internal class LessonCrud
    {
        static public Dictionary<string, int> Teachers = new Dictionary<string, int>();
        public static void UploadLesson(DataViewForm _DataViewForm)
        {
            using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
            {
                cn.Open();
                var sql = @"SELECT [LessonID], Trim([LessonTopic]) as 'Тема занятия' ,  LessonDate AS 'Дата' ,
STRING_AGG(CONCAT(Trim(LastName), ' ', Trim(FirstName), ' ', Trim(MiddleName)), ', ') AS 'ФИО' , COALESCE(Trim(CodeforcesNickname), '-') as 'CF ник', Trim(Position) as 'Должность'        
FROM [FSPDB].[dbo].[Lessons]
                left join Teachers on Teachers.TeacherID = Lessons.TeacherID
                group by LessonID, LessonTopic, LessonDate, CodeforcesNickname, Position 
                order by 'Тема занятия' asc, 'Дата' asc, ФИО ASC,  Должность ASC, [CF ник] ASC   ";

                var cmd = new SqlCommand(sql, cn);

                SqlDataAdapter ds = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                _DataViewForm.dataTable = dataTable;

                ds.Fill(dataTable);
                _DataViewForm.dataGridView.DataSource = dataTable;
                _DataViewForm.dataGridView.Columns[0].Visible = false;
            }

            using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
            {
                cn.Open();
                var sql = @"SELECT TeacherID, 
                           STRING_AGG(CONCAT(Trim(LastName), ' ', Trim(FirstName), ' ', Trim(MiddleName)), ', ') AS 'ФИО'
                    FROM Teachers 
                    GROUP BY TeacherID 
                    ORDER BY ФИО ASC";

                var cmd = new SqlCommand(sql, cn);

                SqlDataReader reader1 = cmd.ExecuteReader();

                Dictionary<string, int> teachers = new Dictionary<string, int>();

                while (reader1.Read())
                {
                    int idteacher = reader1.GetInt32(0);
                    string teacher = reader1.GetString(1);                    
                    teachers.Add(teacher, idteacher);
                }

                Teachers = teachers;
            }
        }
        public static void AddLesson(DataViewForm _DataViewForm)
        {
            LessonForm lessonForm = new LessonForm();
            if (lessonForm.ShowDialog() == DialogResult.OK)
            {
                using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
                {
                    cn.Open();
                    string sql;
                    SqlCommand cmd;
                    sql = @"INSERT INTO [dbo].[Lessons]
                           ([LessonDate]
                           ,[LessonTopic]
                           ,[TeacherID])
                     VALUES
                           (@date
                           ,@topic
                           ,@teachid)";

                        cmd = new SqlCommand(sql, cn);
                        cmd.Parameters.AddWithValue("@date", lessonForm.date);
                        cmd.Parameters.AddWithValue("@topic", lessonForm.Topic);
                    cmd.Parameters.AddWithValue("@teachid", Teachers[lessonForm.Teacher]);
                    try
                        {
                            cmd.ExecuteNonQuery();
                        }
                        catch (SqlException)
                        {
                        MessageBox.Show($"Ошибка! Занятие с темой {lessonForm.Topic} уже есть в БД!");
                            return;
                        }
                }

                UploadLesson(_DataViewForm);
            }
        }

        public static void ChangeLesson(DataViewForm _DataViewForm, int idchange, string teacher, string topic, DateTime date)
        {
             LessonForm lessonForm = new LessonForm(idchange, teacher, topic, date);
            if (lessonForm.ShowDialog() == DialogResult.OK)
            {
                using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
                {

                    cn.Open();
                    string sql;
                    SqlCommand cmd;
                    sql = @"UPDATE [dbo].[Lessons]
                       SET [LessonDate] = @datenew
                          ,[LessonTopic] = @newtopic
                          ,[TeacherID] = @teacherid
                     WHERE LessonID = @lessonid";

                    cmd = new SqlCommand(sql, cn);
                    cmd.Parameters.AddWithValue("@datenew", lessonForm.date);
                    cmd.Parameters.AddWithValue("@lessonid", idchange);
                    cmd.Parameters.AddWithValue("@newtopic", lessonForm.Topic);
                    cmd.Parameters.AddWithValue("@teacherid", Teachers[lessonForm.Teacher]);
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException)
                    {
                        MessageBox.Show($"Ошибка! Занятие с темой {lessonForm.Topic} уже есть в БД!");
                        return;
                    }

                }

                UploadLesson(_DataViewForm);
            }
        }
        public static void DeleteLesson(DataViewForm _DataViewForm, int idtodel)
        {
            using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
            {
                cn.Open();
                string sql;
                SqlCommand cmd;
                sql = @"DELETE FROM [dbo].[Lessons]
      WHERE LessonID = @lessonid";

                cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@lessonid", idtodel);
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
