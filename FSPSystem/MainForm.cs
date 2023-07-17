using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FSPSystem
{
    public partial class MainForm : Form
    {
        public const string connectionString =
@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FSPDB;Integrated Security=True";


        public MainForm()
        {
            InitializeComponent();
        }

        private void btnContests_Click(object sender, EventArgs e)
        {
            DataViewForm dataViewForm = new DataViewForm(this, Tables.Contests);
            if (dataViewForm.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void btnStudents_Click(object sender, EventArgs e)
        {
            DataViewForm dataViewForm = new DataViewForm(this, Tables.Students);
            if (dataViewForm.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void btnTeams_Click(object sender, EventArgs e)
        {
            DataViewForm dataViewForm = new DataViewForm(this, Tables.Teams);
            if (dataViewForm.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void btnTeachers_Click(object sender, EventArgs e)
        {
            DataViewForm dataViewForm = new DataViewForm(this, Tables.Teachers);
            if (dataViewForm.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void btnTasks_Click(object sender, EventArgs e)
        {
            DataViewForm dataViewForm = new DataViewForm(this, Tables.Tasks);
            if (dataViewForm.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void btnLessons_Click(object sender, EventArgs e)
        {
            DataViewForm dataViewForm = new DataViewForm(this, Tables.Lessons);
            if (dataViewForm.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void btnOlympiads_Click(object sender, EventArgs e)
        {
            DataViewForm dataViewForm = new DataViewForm(this, Tables.Olympiads);
            if (dataViewForm.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            ReportsForm reportsForm = new ReportsForm();
            if (reportsForm.ShowDialog() == DialogResult.OK)
            {

            }
        }
    }
}
