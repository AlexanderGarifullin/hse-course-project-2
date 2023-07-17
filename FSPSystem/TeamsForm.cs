using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FSPSystem
{
    public partial class TeamsForm : Form
    {

        public string teamName;
        public bool rosterExist = false;
        public DataTable dataTable = new DataTable();
        public List<DataGridViewCell> selectedCells = new List<DataGridViewCell>();
        public List<int> addedStudentID = new List<int>();
        public List<int> deletedStudentID = new List<int>();
        public Dictionary<string, int> groupsName = new Dictionary<string, int>();
        public DataViewForm _DataViewForm;
        public bool change = false;
        public string teamId;
        public TeamsForm()
        {
            InitializeComponent();
        }
        public TeamsForm(DataViewForm _DataViewForm)
        {
            InitializeComponent();
            this._DataViewForm = _DataViewForm;
            dataTable.Columns.Add("StudentID");
            dataTable.Columns.Add("Фамилия");
            dataTable.Columns.Add("Имя");
            dataTable.Columns.Add("Отчество");
            dataTable.Columns.Add("CF ник");
            dataTable.Columns.Add("Группа");
            dataGridViewRoster.DataSource = dataTable;
            dataGridViewRoster.Columns[0].Visible = false;
            using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
            {
                cn.Open();
                var sql = @"select GroupID, Trim(GroupName) as GroupName from StudyGroups";
                var cmd = new SqlCommand(sql, cn);

                SqlDataReader reader = cmd.ExecuteReader();

                Dictionary<string, int> groupsName = new Dictionary<string, int>();

                while (reader.Read())
                {
                    int groupId = reader.GetInt32(0);
                    string groupName = reader.GetString(1);
                    groupsName.Add(groupName, groupId);
                }
                this.groupsName = groupsName;
            }
        }


        public TeamsForm(DataViewForm _DataViewForm, string teamID, string teamName)
        {
            InitializeComponent();
            rosterExist = true;
            textBoxTeamName.Text = teamName;
            this._DataViewForm = _DataViewForm;
            change = true;
            this.teamId = teamID;
            using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
            {
                cn.Open();
                var sql = @"select Participants.StudentID, Trim(LastName) as 'Фамилия' , Trim(FirstName) as 'Имя' , Trim(MiddleName) as 'Отчество',
                    Trim(CodeforcesNickname) as 'CF ник' , Trim(GroupName) as 'Группа' from Participants 
                    left join Students on Students.StudentID = Participants.StudentID
                    left join StudyGroups on StudyGroups.GroupID = Students.GroupID
                    where TeamID = @teamID ORDER BY Фамилия ASC, Имя ASC, Отчество ASC, [CF ник] ASC, Группа ASC"; 
                
                var cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@teamID", teamID);

                SqlDataAdapter ds = new SqlDataAdapter(cmd);

                ds.Fill(dataTable);

                dataGridViewRoster.DataSource = dataTable;
                dataGridViewRoster.Columns[0].Visible = false;      
            }

            using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
            {
                cn.Open();
                var sql = @"select GroupID, Trim(GroupName) as GroupName from StudyGroups";
                var cmd = new SqlCommand(sql, cn);

                SqlDataReader reader = cmd.ExecuteReader();

                Dictionary<string, int> groupsName = new Dictionary<string, int>();

                while (reader.Read())
                {
                    int groupId = reader.GetInt32(0);
                    string groupName = reader.GetString(1);
                    groupsName.Add(groupName, groupId);
                }
                this.groupsName = groupsName;
            }
        }

        private static int FindIndexToInsert(DataGridView dataGridView, string LastName, string FirstName, string MiddleName, string CodeforcesNickname, string GroupName)
        {
            int index = 0;

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (String.Compare(row.Cells[1].Value.ToString(), LastName) > 0 || String.Compare(row.Cells[1].Value.ToString(), LastName) == 0 &&
                    (String.Compare(row.Cells[2].Value.ToString(), FirstName) > 0 || String.Compare(row.Cells[2].Value.ToString(), FirstName) == 0 &&
                    (String.Compare(row.Cells[3].Value.ToString(), MiddleName) > 0 || String.Compare(row.Cells[3].Value.ToString(), MiddleName) == 0 &&
                    (String.Compare(row.Cells[4].Value.ToString(), CodeforcesNickname) > 0 || (String.Compare(row.Cells[4].Value.ToString(), CodeforcesNickname) == 0 &&
                     (String.Compare(row.Cells[5].Value.ToString(), GroupName) > 0))))))
                    break;
                index++;
            }
            return index;
        }

        private static int FindIndexToInsertTeamName(DataGridView dataGridView, string teamName)
        {
            int index = 0;

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (String.Compare(row.Cells[1].Value.ToString(), teamName) > 0)
                    break;
                index++;
            }
            return index;
        }
        public bool ValidateInput()
        {
            teamName = textBoxTeamName.Text.Trim();
            if (teamName == "")
            {
                MessageBox.Show($"Название команды не может быть пустым!");
                return false;
            }
            return true;
        }
     


        private void btnOK_Click(object sender, EventArgs e)
        {    
            if (ValidateInput())
            {
                if (change)
                {
                    using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
                    {
                        cn.Open();
                        var sql = @"UPDATE [dbo].[Teams]
                               SET [TeamName] = @teamname
                             WHERE Teams.TeamID = @teamId ";
                        var cmd = new SqlCommand(sql, cn);
                        cmd.Parameters.AddWithValue("@teamId", teamId);
                        cmd.Parameters.AddWithValue("@teamname", textBoxTeamName.Text);
                        try
                        {
                            cmd.ExecuteNonQuery();

                            foreach (var id in addedStudentID)
                            {
                                sql = @"INSERT INTO [dbo].[Participants]
                                   ([StudentID]
                                   ,[TeamID])
                             VALUES
                                   (@studentID
                                   ,@teamid)";
                                cmd = new SqlCommand(sql, cn);
                                cmd.Parameters.AddWithValue("@studentID", id);
                                cmd.Parameters.AddWithValue("@teamid", teamId);
                                try
                                {
                                    cmd.ExecuteNonQuery();
                                }
                                catch (SqlException)
                                {
                                    //   MessageBox.Show($"Ошибка!");
                                }
                            }

                            foreach (var id in deletedStudentID)
                            {
                                sql = @"delete from Participants  where StudentID = @studentid and TeamID = @teamid ";
                                cmd = new SqlCommand(sql, cn);
                                cmd.Parameters.AddWithValue("@studentID", id);
                                cmd.Parameters.AddWithValue("@teamid", teamId);
             
                                try
                                {
                                    cmd.ExecuteNonQuery();
                                }
                                catch (SqlException)
                                {
                                     MessageBox.Show($"Ошибка!");
                                }
                            }

                            TeamsCrud.UploadTeams(_DataViewForm);


                        }
                        catch (SqlException)
                        {
                            MessageBox.Show($"Ошибка! Команда {teamName} уже есть в БД");
                        }
                    }
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
                    {
                        cn.Open();
                        var sql = @"INSERT INTO [dbo].[Teams]
                               ([TeamName])
                        OUTPUT INSERTED.TeamID
                         VALUES
                               (@teamname)";
                        var cmd = new SqlCommand(sql, cn);
                        cmd.Parameters.AddWithValue("@teamname", teamName);
                        try
                        {
                            int teamID = (int)cmd.ExecuteScalar();
                            foreach (var id in addedStudentID)
                            {
                                sql = @"INSERT INTO [dbo].[Participants]
                                   ([StudentID]
                                   ,[TeamID])
                             VALUES
                                   (@studentID
                                   ,@teamid)";
                                cmd = new SqlCommand(sql, cn);
                                cmd.Parameters.AddWithValue("@studentID", id);
                                cmd.Parameters.AddWithValue("@teamid", teamID);
                                try
                                {
                                    cmd.ExecuteNonQuery();
                                }
                                catch (SqlException)
                                {
                                    //   MessageBox.Show($"Ошибка!");
                                }
                            }

                            foreach (var id in deletedStudentID)
                            {
                                sql = @"delete from Participants  where StudentID = @studentid and TeamID = @teamid ";
                                cmd = new SqlCommand(sql, cn);
                                cmd.Parameters.AddWithValue("@studentID", id);
                                cmd.Parameters.AddWithValue("@teamid", teamID);
                                try
                                {
                                    cmd.ExecuteNonQuery();
                                }
                                catch (SqlException)
                                {
                                    // MessageBox.Show($"Ошибка!");
                                }
                            }
                            TeamsCrud.UploadTeams(_DataViewForm);


                        }
                        catch (SqlException)
                        {
                            MessageBox.Show($"Ошибка! Команда {teamName} уже есть в БД");
                        }
                    }
                    DialogResult = DialogResult.OK;
                }
            }
        }

        private void btnAddExistSrudent_Click(object sender, EventArgs e)
        {
            if (!rosterExist)
            {
                rosterExist = true;
                ListStudentsForm listStudentsForm = new ListStudentsForm(this);
                if (listStudentsForm.ShowDialog() == DialogResult.OK)
                {
                    foreach (var cell in selectedCells)
                    {
                        DataGridViewRow row = cell.OwningRow;
                        string studeint = row.Cells["StudentID"].Value.ToString();
                        string lastName = row.Cells["Фамилия"].Value.ToString();
                        string firstName = row.Cells["Имя"].Value.ToString();
                        string middleName = row.Cells["Отчество"].Value.ToString();
                        string codeforcesNickname = row.Cells["CF ник"].Value.ToString();
                        string groupName = row.Cells["Группа"].Value.ToString();
                        int indexToAdd = FindIndexToInsert(dataGridViewRoster, lastName, firstName, middleName, codeforcesNickname, groupName);

                        DataRow newRow = dataTable.NewRow();
                        newRow["StudentID"] = studeint;
                        newRow["Фамилия"] = lastName;
                        newRow["Имя"] = firstName;
                        newRow["Отчество"] = middleName;
                        newRow["CF ник"] = codeforcesNickname;
                        newRow["Группа"] = groupName;
                        dataTable.Rows.InsertAt(newRow, indexToAdd);

                        addedStudentID.Add(Convert.ToInt32(studeint));
                    }
                }
            }
            else
            {
                ListStudentsForm listStudentsForm = new ListStudentsForm(this);
                if (listStudentsForm.ShowDialog() == DialogResult.OK)
                {
                    foreach (var cell in selectedCells)
                    {
                        DataGridViewRow row = cell.OwningRow;
                        string studeint = row.Cells["StudentID"].Value.ToString();
                        string lastName = row.Cells["Фамилия"].Value.ToString();
                        string firstName = row.Cells["Имя"].Value.ToString();
                        string middleName = row.Cells["Отчество"].Value.ToString();
                        string codeforcesNickname = row.Cells["CF ник"].Value.ToString();
                        string groupName = row.Cells["Группа"].Value.ToString();

                        for (int curRow = 0; curRow < dataGridViewRoster.Rows.Count; curRow++)
                        {
                            if (studeint == dataGridViewRoster.Rows[curRow].Cells[0].Value.ToString())
                            {
                                MessageBox.Show($"Студент {lastName} {firstName} {middleName} уже есть в команде!");
                                return;
                            }
                        }

                        int indexToAdd = FindIndexToInsert(dataGridViewRoster, lastName, firstName, middleName, codeforcesNickname, groupName);

                        DataRow newRow = dataTable.NewRow();
                        newRow["StudentID"] = studeint;
                        newRow["Фамилия"] = lastName;
                        newRow["Имя"] = firstName;
                        newRow["Отчество"] = middleName;
                        newRow["CF ник"] = codeforcesNickname;
                        newRow["Группа"] = groupName;
                        dataTable.Rows.InsertAt(newRow, indexToAdd);
                        addedStudentID.Add(Convert.ToInt32(studeint));
                    }
                }
            }
        }

        private void btnDeleteStudent_Click(object sender, EventArgs e)
        {
            if (dataGridViewRoster.CurrentCell == null)
            {
                MessageBox.Show("Вы не выбрали ячейку для удаления!");
                return;
            }
            int selectedRowIndex = dataGridViewRoster.SelectedCells[0].RowIndex;
            int idToDelete = Convert.ToInt32(dataGridViewRoster.Rows[selectedRowIndex].Cells["StudentID"].Value);
            deletedStudentID.Add(idToDelete);
            dataGridViewRoster.Rows.RemoveAt(selectedRowIndex);
        }

        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            var studentFrom = new StudentForm();
            if (studentFrom.ShowDialog() == DialogResult.OK)
            {
                using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
                {
                    cn.Open();
                    string query = "SELECT COUNT(*) FROM Students WHERE CodeforcesNickname = @codeforcenicnkame";
                    var cmd = new SqlCommand(query, cn);

                    cmd.Parameters.AddWithValue("@codeforcenicnkame", studentFrom.CodeforcesNickname);
                    try
                    {
                        int count = (int)cmd.ExecuteScalar();
                        if (count > 0)
                        {
                            MessageBox.Show($"Ошибка! Студент с ником {studentFrom.CodeforcesNickname} уже есть в БД");
                        }
                        else
                        {
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
                            cmd = new SqlCommand(sql, cn);

                            cmd.Parameters.AddWithValue("@lastName", studentFrom.LastName);
                            cmd.Parameters.AddWithValue("@firstName", studentFrom.FirstName);
                            cmd.Parameters.AddWithValue("@middleName", studentFrom.MiddleName);
                            cmd.Parameters.AddWithValue("@codeforcesNickname", studentFrom.CodeforcesNickname);
                            cmd.Parameters.AddWithValue("@groupID", groupsName[studentFrom.GroupName]);

                            try
                            {
                                int insertedId = (int)cmd.ExecuteScalar();
                                int indexToAdd = FindIndexToInsert(dataGridViewRoster, studentFrom.LastName, studentFrom.FirstName, studentFrom.MiddleName, studentFrom.CodeforcesNickname, studentFrom.GroupName);
                                DataRow newRow = dataTable.NewRow();
                                newRow["StudentID"] = insertedId;
                                newRow["Фамилия"] = studentFrom.LastName;
                                newRow["Имя"] = studentFrom.FirstName;
                                newRow["Отчество"] = studentFrom.MiddleName;
                                newRow["CF ник"] = studentFrom.CodeforcesNickname;
                                newRow["Группа"] = studentFrom.GroupName;
                                dataTable.Rows.InsertAt(newRow, indexToAdd);
                                addedStudentID.Add(insertedId);
                                rosterExist = true;
                            }
                            catch (SqlException)
                            {
                                MessageBox.Show($"Ошибка! Студент с ником {studentFrom.CodeforcesNickname} уже есть в БД!");
                            }
                        }
                    }
                    catch (SqlException)
                    {
                        MessageBox.Show($"Ошибка!");
                    }
                }
            }
        }

        private void btnChangeStudent_Click(object sender, EventArgs e)
        {
            if (!rosterExist)
            {
                MessageBox.Show($"Вы не загрузили состав команды!");
            }
            else
            {
                if (dataGridViewRoster.CurrentCell == null) {
                    MessageBox.Show("Вы не выбрали ячейку для изменения!");
                    return;
                }
                int selectedRowIndex = dataGridViewRoster.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridViewRoster.SelectedCells[0].OwningRow;
                string studentID = dataGridViewRoster.Rows[selectedRowIndex].Cells["StudentID"].Value.ToString();
                string lastName = dataGridViewRoster.Rows[selectedRowIndex].Cells["Фамилия"].Value.ToString();
                string firstName = dataGridViewRoster.Rows[selectedRowIndex].Cells["Имя"].Value.ToString();
                string middleName = dataGridViewRoster.Rows[selectedRowIndex].Cells["Отчество"].Value.ToString();
                string codeforcesNickname= dataGridViewRoster.Rows[selectedRowIndex].Cells["CF ник"].Value.ToString();
                string groupName = dataGridViewRoster.Rows[selectedRowIndex].Cells["Группа"].Value.ToString();

                var studentForm = new StudentForm(groupsName,lastName, firstName, middleName, codeforcesNickname, groupName);
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
                        cmd.Parameters.AddWithValue("@groupID", groupsName[studentForm.GroupName]);
                        cmd.Parameters.AddWithValue("@studentID", studentID);
                        try
                        {
                            cmd.ExecuteNonQuery();
                            dataTable.Rows.RemoveAt(selectedRowIndex);
                            int indexToAdd = FindIndexToInsert(dataGridViewRoster, studentForm.LastName, studentForm.FirstName, studentForm.MiddleName, studentForm.CodeforcesNickname, studentForm.GroupName);
                            DataRow newRow = dataTable.NewRow();
                            newRow["StudentID"] = studentID;
                            newRow["Фамилия"] = studentForm.LastName;
                            newRow["Имя"] = studentForm.FirstName;
                            newRow["Отчество"] = studentForm.MiddleName;
                            newRow["CF ник"] = studentForm.CodeforcesNickname;
                            newRow["Группа"] = studentForm.GroupName;
                            dataTable.Rows.InsertAt(newRow, indexToAdd);
                        }
                        catch (SqlException)
                        {
                            MessageBox.Show($"Ошибка! Студент с ником {studentForm.CodeforcesNickname} уже есть в БД!");
                        }
                    }
                }
            }
        }

        private void TeamsForm_Load(object sender, EventArgs e)
        {

        }
    }
}
