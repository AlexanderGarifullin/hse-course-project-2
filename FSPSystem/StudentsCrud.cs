using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FSPSystem
{
    internal class StudentsCrud
    {
        private static int FindIndexToInsert (DataGridView dataGridView,string LastName, string FirstName, string MiddleName, string CodeforcesNickname, string GroupName)
        {
            int index = 0;

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (String.Compare(row.Cells[2].Value.ToString(), LastName) > 0 || String.Compare(row.Cells[2].Value.ToString(), LastName) == 0 && 
                    (String.Compare(row.Cells[3].Value.ToString(), FirstName) > 0 || String.Compare(row.Cells[3].Value.ToString(), FirstName) == 0  &&
                    (String.Compare(row.Cells[4].Value.ToString(), MiddleName) > 0  || String.Compare(row.Cells[4].Value.ToString(), MiddleName) == 0 &&
                    (String.Compare(row.Cells[5].Value.ToString(), CodeforcesNickname) > 0  || (String.Compare(row.Cells[5].Value.ToString(), CodeforcesNickname) == 0 &&
                     (String.Compare(row.Cells[5].Value.ToString(), GroupName) > 0) )))   ) )
                    break;
                index++;
            }
            return index;
        }


        public static void AddStudent(DataViewForm _DataViewForm)
        {
            StudentForm studentForm = new StudentForm(_DataViewForm.groupsName);
            if (studentForm.ShowDialog() == DialogResult.OK)
            {
                using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
                {
                    cn.Open();
                    var sql = @"INSERT INTO [dbo].[Students]
                           ([LastName]
                           ,[FirstName]
                           ,[MiddleName]
                           ,[CodeforcesNickname]
                           ,[GroupID])
                    OUTPUT INSERTED.StudentID
                     VALUES
                           ( @lastName
                           ,@firstName
                           ,@middleName
                           ,@codeforcesNickname
                           ,@groupID);";
                    var cmd = new SqlCommand(sql, cn);

                    cmd.Parameters.AddWithValue("@lastName", studentForm.LastName);
                    cmd.Parameters.AddWithValue("@firstName", studentForm.FirstName);
                    cmd.Parameters.AddWithValue("@middleName", studentForm.MiddleName);
                    cmd.Parameters.AddWithValue("@codeforcesNickname", studentForm.CodeforcesNickname);
                    cmd.Parameters.AddWithValue("@groupID", _DataViewForm.groupsName[studentForm.GroupName]);

                    try
                    {
                        int insertedId = (int)cmd.ExecuteScalar();
                       int index =  FindIndexToInsert(_DataViewForm.dataGridView, studentForm.LastName, studentForm.FirstName, studentForm.MiddleName, studentForm.CodeforcesNickname , studentForm.GroupName);
                        DataRow newRow = _DataViewForm.dataTable.NewRow();
                        newRow["StudentID"] = insertedId;
                        newRow["GroupID"] = _DataViewForm.groupsName[studentForm.GroupName];
                        newRow["Фамилия"] = studentForm.LastName;
                        newRow["Имя"] = studentForm.FirstName;
                        newRow["Отчество"] = studentForm.MiddleName;
                        newRow["CF ник"] = studentForm.CodeforcesNickname;
                        newRow["Группа"] = studentForm.GroupName;
                        _DataViewForm.dataTable.Rows.InsertAt(newRow, index);
                    }
                    catch (SqlException)
                    {
                        MessageBox.Show($"Ошибка! Студент с ником {studentForm.CodeforcesNickname} уже есть в БД!");
                    }
                }
            }
        }

        public static void DeleteStudent(DataViewForm _DataViewForm, int studentIDToDelete)
        {
            using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
            {
                cn.Open();
                var sql = @"delete from Students where StudentID = @studentID;";
                var cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@studentID", studentIDToDelete);
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
        public static void ChangeStudent(DataViewForm _DataViewForm, int selectedRowIndex, string studentId, string groupID, string LastName, string FirstName, string MiddleName, string CodeforcesNickname, string GroupName)
        {
            StudentForm studentForm = new StudentForm(_DataViewForm.groupsName,  LastName,  FirstName,  MiddleName,  CodeforcesNickname,  GroupName);
            if (studentForm.ShowDialog() == DialogResult.OK)
            {
                using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
                {
                    cn.Open();
                    var sql = @"UPDATE [dbo].[Students]
                               SET [LastName] = @lastName
                                  ,[FirstName] = @firstName
                                  ,[MiddleName] = @middleName
                                  ,[CodeforcesNickname] = @codeforcesNickname
                                  ,[GroupID] = @groupID
                             WHERE StudentID = @studentID;";
                    var cmd = new SqlCommand(sql, cn);

                    cmd.Parameters.AddWithValue("@lastName", studentForm.LastName);
                    cmd.Parameters.AddWithValue("@firstName", studentForm.FirstName);
                    cmd.Parameters.AddWithValue("@middleName", studentForm.MiddleName);
                    cmd.Parameters.AddWithValue("@codeforcesNickname", studentForm.CodeforcesNickname);
                    cmd.Parameters.AddWithValue("@groupID", _DataViewForm.groupsName[studentForm.GroupName]);
                    cmd.Parameters.AddWithValue("@studentID", studentId);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        int insertedId = Convert.ToInt32(studentId);
                        _DataViewForm.dataGridView.Rows.RemoveAt(selectedRowIndex);
                        int index = FindIndexToInsert(_DataViewForm.dataGridView, studentForm.LastName, studentForm.FirstName, studentForm.MiddleName, studentForm.CodeforcesNickname, studentForm.GroupName);
                        DataRow newRow = _DataViewForm.dataTable.NewRow();
                        newRow["StudentID"] = insertedId;
                        newRow["GroupID"] = _DataViewForm.groupsName[studentForm.GroupName];
                        newRow["Фамилия"] = studentForm.LastName;
                        newRow["Имя"] = studentForm.FirstName;
                        newRow["Отчество"] = studentForm.MiddleName;
                        newRow["CF ник"] = studentForm.CodeforcesNickname;
                        newRow["Группа"] = studentForm.GroupName;
                        _DataViewForm.dataTable.Rows.InsertAt(newRow, index);
                    }
                    catch (SqlException)
                    {
                        MessageBox.Show($"Ошибка! Студент с ником {studentForm.CodeforcesNickname} уже есть в БД!");
                    }
                }
            }
        }
        public static void UploadStudent(DataViewForm _DataViewForm)
        {
            using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
            {
                cn.Open();
                var sql = @"select  StudentID, Students.GroupID, Trim(LastName) as 'Фамилия', Trim(FirstName) as 'Имя', Trim(MiddleName) as 'Отчество', 
                    Trim(CodeforcesNickname) as 'CF ник', Trim(GroupName) as 'Группа'
			     from Students left  join StudyGroups on Students.GroupID = StudyGroups.GroupID
				 ORDER BY Фамилия ASC, Имя ASC, Отчество ASC, [CF ник] ASC, Группа ASC";
                var cmd = new SqlCommand(sql, cn);

                SqlDataAdapter ds = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                _DataViewForm.dataTable = dataTable;
                
                ds.Fill(dataTable);
                _DataViewForm.dataGridView.DataSource = dataTable;
                _DataViewForm.dataGridView.Columns[0].Visible = false;
                _DataViewForm.dataGridView.Columns[1].Visible = false;

                sql = @"select GroupID, Trim(GroupName) as GroupName from StudyGroups";
                cmd = new SqlCommand(sql, cn);

                SqlDataReader reader = cmd.ExecuteReader();

                Dictionary<string, int> groupsName = new Dictionary<string, int>();

                while (reader.Read())
                {
                    int groupId = reader.GetInt32(0);
                    string groupName = reader.GetString(1);
                    groupsName.Add(groupName, groupId);
                }
                _DataViewForm.groupsName = groupsName;
            }
        }
    }
}
