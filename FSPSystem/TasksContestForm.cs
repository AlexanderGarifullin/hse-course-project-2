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
    public partial class TasksContestForm : Form
    {
        DataTable dataTable = new DataTable();
        public TasksContestForm(int contestID, string name)
        {
            InitializeComponent();
            label1.Text = name;
            using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
            {
                cn.Open();
                var sql = @"SELECT 
                          Trim(Tasks.CodeforcesTaskID) AS 'CF ID',
                          FirstDifficulties.Coefficient AS 'Коэффициент',
                          SecondDifficulties.TaskWeight AS 'RP',
                          STRING_AGG(TRIM(Themes.ThemeName), ', ') WITHIN GROUP (ORDER BY Themes.ThemeName) AS Themes
                        FROM 
                          TaskContests
                          LEFT JOIN Tasks ON Tasks.TaskID = TaskContests.TaskID
                          LEFT JOIN FirstDifficulties ON FirstDifficulties.FirstDifficultyID = Tasks.FirstDifficultyID
                          LEFT JOIN SecondDifficulties ON SecondDifficulties.SecondDifficultyID = Tasks.SecondDifficultyID
                          LEFT JOIN TaskThemes ON TaskThemes.TaskID = Tasks.TaskID
                          LEFT JOIN Themes ON Themes.ThemeID = TaskThemes.ThemeID
                        WHERE 
                          TaskContests.ContestID = @contestid
                        GROUP BY 
                          Tasks.CodeforcesTaskID, FirstDifficulties.Coefficient, SecondDifficulties.TaskWeight
                          order by Tasks.CodeforcesTaskID asc, FirstDifficulties.Coefficient asc, SecondDifficulties.TaskWeight asc
                                ";      

                var cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@contestid", contestID);
                SqlDataAdapter ds = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                ds.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }
    }
}
