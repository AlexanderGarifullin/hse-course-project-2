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
    internal class TeamsCrud
    {
        public static void UploadTeams(DataViewForm _DataViewForm)
        {
            using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
            {
                cn.Open();
                var sql = @"SELECT t.TeamID, Trim(t.TeamName) as 'Команда', COALESCE(STRING_AGG(CONCAT(Trim(s.LastName), ' ', Trim(s.FirstName), ' ', Trim(s.MiddleName)), ', '), '') AS 'Состав'
                        FROM Teams t
                        LEFT JOIN Participants p ON t.TeamID = p.TeamID
                        LEFT JOIN Students s ON p.StudentID = s.StudentID
                        GROUP BY t.TeamID, t.TeamName
                        ORDER BY t.TeamName
                         ";
                var cmd = new SqlCommand(sql, cn);

                SqlDataAdapter ds = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                _DataViewForm.dataTable = dataTable;

                ds.Fill(dataTable);
                _DataViewForm.dataGridView.DataSource = dataTable;
                _DataViewForm.dataGridView.Columns[0].Visible = false;

                //sql = @"select GroupID, Trim(GroupName) as GroupName from StudyGroups";
                //cmd = new SqlCommand(sql, cn);

                //SqlDataReader reader = cmd.ExecuteReader();

                //Dictionary<string, int> groupsName = new Dictionary<string, int>();

                //while (reader.Read())
                //{
                //    int groupId = reader.GetInt32(0);
                //    string groupName = reader.GetString(1);
                //    groupsName.Add(groupName, groupId);
                //}
                //_DataViewForm.groupsName = groupsName;
            }
        }


        public static void DeleteTeam(DataViewForm _DataViewForm, int teamIDToDelete)
        {
            using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
            {
             //   MessageBox.Show(teamIDToDelete.ToString());
                cn.Open();
                var sql = @"delete from Teams where Teams.TeamID = @teamID;";
                var cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@teamID", teamIDToDelete);
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


        public static void AddTeam(DataViewForm _DataViewForm)
        {
            TeamsForm teamsForm = new TeamsForm(_DataViewForm);
            if (teamsForm.ShowDialog() == DialogResult.OK)
            {

            }
        }
       public static void ChangeTeam(DataViewForm dataViewForm, string teamID, string teamName)
        {

            TeamsForm teamsForm = new TeamsForm(dataViewForm, teamID, teamName);
            if (teamsForm.ShowDialog() == DialogResult.OK)
            {

            }
        }
    }
}
