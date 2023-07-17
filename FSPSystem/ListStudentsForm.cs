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
    public partial class ListStudentsForm : Form
    {
        TeamsForm teamsForm;

        public ListStudentsForm(TeamsForm _teamsForm)
        {
            InitializeComponent();
            teamsForm = _teamsForm;
            using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
            {
                cn.Open();
                var sql = @"select  StudentID, Trim(LastName) as 'Фамилия', Trim(FirstName) as 'Имя', Trim(MiddleName) as 'Отчество', 
                    Trim(CodeforcesNickname) as 'CF ник', Trim(GroupName) as 'Группа'
			     from Students left  join StudyGroups on Students.GroupID = StudyGroups.GroupID
				 ORDER BY Фамилия ASC, Имя ASC, Отчество ASC, [CF ник] ASC, Группа ASC";
                var cmd = new SqlCommand(sql, cn);

                SqlDataAdapter ds = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                dataGridViewListStudents.DataSource = dataTable;

                ds.Fill(dataTable);
                dataGridViewListStudents.Columns[0].Visible = false;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            List<DataGridViewCell> selectedCells = new List<DataGridViewCell>();
            foreach (DataGridViewCell cell in dataGridViewListStudents.SelectedCells)
            {
                selectedCells.Add(cell);
            }
            teamsForm.selectedCells = selectedCells;
            DialogResult = DialogResult.OK;
        }
    }
}
