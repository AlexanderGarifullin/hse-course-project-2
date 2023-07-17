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
    internal class OlympiadsCrud
    {
        static public Dictionary<string, int> Olympiads = new Dictionary<string, int>();
        static public Dictionary<string, int> Participants = new Dictionary<string, int>();

        public static void UploadOlympiads(DataViewForm _DataViewForm)
        {
            Participants = new Dictionary<string, int>();
            Olympiads = new Dictionary<string, int>();
            using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
            {
                cn.Open();

                var sql = @"select OlympiadID, Trim(OlympiadName) as 'Олимпиада', Olympiads.Year as 'Год проведения', BaseWeight as 'BP', WeightPerProblem as 'BP за задачу',
                Trim(ParticipationType) as 'Тип участия' from Olympiads
                order by OlympiadName asc  , Olympiads.Year asc, BaseWeight asc, WeightPerProblem asc, ParticipationType asc";
     
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
                var sql = @"select OlympiadID, OlympiadName  from Olympiads";
                var cmd = new SqlCommand(sql, cn);

                SqlDataReader reader1 = cmd.ExecuteReader();

                Dictionary<string, int> ols = new Dictionary<string, int>();

                while (reader1.Read())
                {
                    int idol = reader1.GetInt32(0);
                    string olname = reader1.GetString(1);
                    ols.Add(olname, idol);
                }
                Olympiads = ols;
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
        }


        public static void AddOlympiad(DataViewForm _DataViewForm)
        {
            AddOlympiadForm addOlympiadForm = new AddOlympiadForm();
            if (addOlympiadForm.ShowDialog() == DialogResult.OK)
            {
                int idOlymp = -1;
                using(SqlConnection cn = new SqlConnection(MainForm.connectionString))
                {
                    cn.Open();
                    string sql;
                    SqlCommand cmd;
                    sql = @"INSERT INTO [dbo].[Olympiads]
                           ([OlympiadName]
                           ,[Year]
                           ,[BaseWeight]
                           ,[WeightPerProblem]
                           ,[ParticipationType])
                     OUTPUT INSERTED.OlympiadID
                     VALUES
                           (@name
                           ,@year
                           ,@bp
                           ,@bpperproblem
                           ,@type)";

                     cmd = new SqlCommand(sql, cn);

                    cmd.Parameters.AddWithValue("@name", addOlympiadForm.name);
                    cmd.Parameters.AddWithValue("@year", addOlympiadForm.year);
                    cmd.Parameters.AddWithValue("@bp", addOlympiadForm.bp);
                    cmd.Parameters.AddWithValue("@bpperproblem", addOlympiadForm.bppertask);
                    cmd.Parameters.AddWithValue("@type", addOlympiadForm.typeparticipant);
                    try
                    {
                        idOlymp = (int)cmd.ExecuteScalar();
                    }
                    catch (SqlException)
                    {
                        MessageBox.Show($"Ошибка! Олимпиада с названием {addOlympiadForm.name} уже есть в БД!");
                        return;
                    }
                }

                using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
                {
                    cn.Open();
                    string sql;
                    SqlCommand cmd;
                    sql = @"INSERT INTO [dbo].[ParticipantOlympiads]
                           ([ParticipantID]
                           ,[OlympiadID]
                           ,[SolvedProblemsCount])
                     VALUES
                           (@partid
                           ,@olympid
                           ,@cnt)";
                    foreach (var part in addOlympiadForm.participants)
                    {

                        cmd = new SqlCommand(sql, cn);

                        cmd.Parameters.AddWithValue("@partid", Participants[part]);
                        cmd.Parameters.AddWithValue("@olympid", idOlymp);
                        cmd.Parameters.AddWithValue("@cnt", addOlympiadForm.participants_cnt_solved[part]);
                        cmd.ExecuteNonQuery();
                        try
                        {
                           
                        }
                        catch (SqlException)
                        {
                            MessageBox.Show($"Ошибка!");
                            return;
                        }
                    }
                }

                UploadOlympiads(_DataViewForm);
            }
            
        }

        public static void DeleteOlympiad(DataViewForm _DataViewForm, int idOlymp)
        {
            using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
            {
                cn.Open();
                var sql = @"delete from Olympiads where OlympiadID = @olid;";
                var cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@olid", idOlymp);
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


        public static void ChangeOlympiad(DataViewForm _DataViewForm, string OlympiadID, string name, string year, string bp, string bpperyear, string type)
        {
            AddOlympiadForm addOlympiadForm = new AddOlympiadForm(OlympiadID, name, year, bp, bpperyear, type);
            if (addOlympiadForm.ShowDialog() == DialogResult.OK)
            {
                using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
                {
                    cn.Open();
                    string sql;
                    SqlCommand cmd;
                    sql = @"UPDATE [dbo].[Olympiads]
                       SET [OlympiadName] = @name
                          ,[Year] = @year
                          ,[BaseWeight] = @bp
                          ,[WeightPerProblem] = @bpperproblem
                          ,[ParticipationType] = @type
                     WHERE OlympiadID = @olid ";

                    cmd = new SqlCommand(sql, cn);
                    cmd.Parameters.AddWithValue("@olid", OlympiadID);
                    cmd.Parameters.AddWithValue("@name", addOlympiadForm.name);
                    cmd.Parameters.AddWithValue("@year", addOlympiadForm.year);
                    cmd.Parameters.AddWithValue("@bp", addOlympiadForm.bp);
                    cmd.Parameters.AddWithValue("@bpperproblem", addOlympiadForm.bppertask);
                    cmd.Parameters.AddWithValue("@type", addOlympiadForm.typeparticipant);

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException)
                    {
                        MessageBox.Show($"Ошибка! Олимпиада с названием {addOlympiadForm.name} уже есть в БД!");
                        return;
                    }
                }


                using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
                {
                    cn.Open();
                    string sql;
                    SqlCommand cmd;
                    sql = @"DELETE FROM [dbo].[ParticipantOlympiads]
                     WHERE OlympiadID = @olid ";

                    cmd = new SqlCommand(sql, cn);
                    cmd.Parameters.AddWithValue("@olid", OlympiadID);

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
                    sql = @"INSERT INTO [dbo].[ParticipantOlympiads]
                           ([ParticipantID]
                           ,[OlympiadID]
                           ,[SolvedProblemsCount])
                     VALUES
                           (@partid
                           ,@olympid
                           ,@cnt)";
                    foreach (var part in addOlympiadForm.participants)
                    {

                        cmd = new SqlCommand(sql, cn);

                        cmd.Parameters.AddWithValue("@partid", Participants[part]);
                        cmd.Parameters.AddWithValue("@olympid", OlympiadID);
                        cmd.Parameters.AddWithValue("@cnt", addOlympiadForm.participants_cnt_solved[part]);
                        cmd.ExecuteNonQuery();
                        try
                        {

                        }
                        catch (SqlException)
                        {
                            MessageBox.Show($"Ошибка!");
                            return;
                        }
                    }
                }
                UploadOlympiads(_DataViewForm);
            }
        }

            public static void UploadResults(DataViewForm _DataViewForm , int OlympiadID, string name)
        {
            OlympiadsResults olympiadsResults = new OlympiadsResults(OlympiadID, name);
            if (olympiadsResults.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

            }
        }

    }
}
