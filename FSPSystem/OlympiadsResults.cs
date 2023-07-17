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
    public partial class OlympiadsResults : Form
    {
        public OlympiadsResults(int OlympiadID, string name)
        {
            InitializeComponent();
            labelNameOlympiad.Text = name;
            dataGridView1.RowHeadersVisible = false;
   

            using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
            {
                cn.Open();
                var sql = @"SELECT 
                        Trim(LastName) as 'Фамилия', Trim(FirstName) as 'Имя', Trim(MiddleName) as 'Отчество', 
            Trim(GroupName) as 'Группа', Trim(CodeforcesNickname) as 'CF ник',  SolvedProblemsCount as 'Количество решенных задач',
                        COALESCE(SUM(BaseWeight + WeightPerProblem * SolvedProblemsCount), 0) AS BP
                    FROM (
                        SELECT 
                            Students.LastName, Students.FirstName, Students.MiddleName, StudyGroups.GroupName, 
                            Students.CodeforcesNickname, ParticipantOlympiads.SolvedProblemsCount, Olympiads.BaseWeight, 
                            Olympiads.WeightPerProblem
                        FROM ParticipantOlympiads  
                        LEFT JOIN Participants ON ParticipantOlympiads.ParticipantID = Participants.ParticipantID 
                        LEFT JOIN Students ON Students.StudentID = Participants.StudentID
                        LEFT JOIN StudyGroups ON StudyGroups.GroupID = Students.GroupID
                        LEFT JOIN Olympiads ON Olympiads.OlympiadID = ParticipantOlympiads.OlympiadID
                        WHERE Olympiads.OlympiadID = @olympid
                    ) AS subquery 
                    GROUP BY LastName, FirstName, MiddleName, GroupName, CodeforcesNickname, SolvedProblemsCount
                    ORDER BY BP DESC, SolvedProblemsCount DESC, LastName ASC, FirstName ASC, MiddleName ASC, GroupName ASC, CodeforcesNickname ASC
                     ";
               
                var cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@olympid", OlympiadID);
                SqlDataAdapter ds = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();

                dataGridView1.DataSource = dataTable;

                ds.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }
    }
}
