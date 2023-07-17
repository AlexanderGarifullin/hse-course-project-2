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
    internal class TeachersCrud
    {
        public static void UploadTeachers(DataViewForm _DataViewForm)
        {
            using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
            {
                cn.Open();
                var sql = @"SELECT TeacherID, 
                           Trim(LastName) as 'Фамилия',  
                           Trim(FirstName) as 'Имя', 
                           Trim(MiddleName) as 'Отчество',
                           CASE WHEN CodeforcesNickname = '' THEN '-' ELSE Trim(CodeforcesNickname) END as 'CF ник',
                           Trim(Position) as 'Должность'
                    FROM Teachers  
                    ORDER BY Фамилия ASC, Имя ASC, Отчество ASC,  Должность ASC, [CF ник] ASC
                    ";

                var cmd = new SqlCommand(sql, cn);

                SqlDataAdapter ds = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                _DataViewForm.dataTable = dataTable;

                ds.Fill(dataTable);
                _DataViewForm.dataGridView.DataSource = dataTable;
                _DataViewForm.dataGridView.Columns[0].Visible = false;
            }
        }

        public static void AddTeacher(DataViewForm _DataViewForm)
        {
            TeacherForm teacherForm = new TeacherForm();
            if (teacherForm.ShowDialog() == DialogResult.OK)
            {
                using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
                {
                    cn.Open();
                    string sql;
                    SqlCommand cmd;
                    sql = @"INSERT INTO [dbo].[Teachers]
                             ([LastName]
                               ,[FirstName]
                               ,[MiddleName]
                                ,[CodeforcesNickname]
                                ,[Position])
                         VALUES(
                               @lastname
                               ,@firstname
                               ,@middlename
                                ,@codeforcesNickname
                               ,@position)";
                    cmd = new SqlCommand(sql, cn);
                    cmd.Parameters.AddWithValue("@codeforcesNickname", teacherForm.CodeforcesNickname);
                    cmd.Parameters.AddWithValue("@lastname", teacherForm.LastName);
                    cmd.Parameters.AddWithValue("@firstname", teacherForm.FirstName);
                    cmd.Parameters.AddWithValue("@middlename", teacherForm.MiddleName);
                    cmd.Parameters.AddWithValue("@position", teacherForm.Position);
                
                    if (teacherForm.CodeforcesNickname == "") teacherForm.CodeforcesNickname = "-";
                    try
                    {
                        cmd.ExecuteNonQuery();
            
                    }
                    catch (SqlException)
                    {
                        MessageBox.Show($"Ошибка!");
                    }
                    UploadTeachers(_DataViewForm);
                }

            }
        }
        public static void DeleteTeacher(DataViewForm _DataViewForm, int teacherID)
        {
            using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
            {
                cn.Open();
                var sql = @"delete from Teachers where TeacherID = @teacherID;";
                var cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@teacherID", teacherID);
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

        public static void ChangeTeacher(DataViewForm _DataViewForm, string teacherID, string LastName, string FirstName, string MiddleName, string CodeforcesNickname, string Position)
        {
            TeacherForm teacherForm = new TeacherForm(LastName, FirstName, MiddleName, CodeforcesNickname, Position);
            if (teacherForm.ShowDialog() == DialogResult.OK)
            {
                using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
                {
                    cn.Open();
                    var sql = @"UPDATE [dbo].[Teachers]
                           SET [LastName] = @lastname
                              ,[FirstName] = @firstname
                              ,[MiddleName] = @middlename
                              ,[CodeforcesNickname] = @codeforcenick
                              ,[Position] = @pos
                         WHERE TeacherID = @teacherID";
                    var cmd = new SqlCommand(sql, cn);

                    cmd.Parameters.AddWithValue("@lastname", teacherForm.LastName);
                    cmd.Parameters.AddWithValue("@firstname", teacherForm.FirstName);
                    cmd.Parameters.AddWithValue("@middlename", teacherForm.MiddleName);
                    cmd.Parameters.AddWithValue("@codeforcenick", teacherForm.CodeforcesNickname);
                    cmd.Parameters.AddWithValue("@pos", teacherForm.Position);
                    cmd.Parameters.AddWithValue("@teacherID", teacherID);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        UploadTeachers(_DataViewForm);
                    }
                    catch (SqlException)
                    {
                        MessageBox.Show($"Ошибка!");
                    }
                }
            }
        }

    }
}
