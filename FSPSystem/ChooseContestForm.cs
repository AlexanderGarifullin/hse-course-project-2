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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace FSPSystem
{
    public partial class ChooseContestForm : Form
    {
        public string contest;

        public Dictionary<string, int> contests_idcontest = new Dictionary<string, int>();
        public ChooseContestForm()
        {
            InitializeComponent();
            using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
            {
                cn.Open();
                var sql = @"select ContestID, Trim(ContestName) from Contests";
                var cmd = new SqlCommand(sql, cn);

                SqlDataReader reader1 = cmd.ExecuteReader();

                while (reader1.Read())
                {
                    int idol = reader1.GetInt32(0);
                    string con = reader1.GetString(1);
                    contests_idcontest.Add(con, idol);
                    comboBoxContests.Items.Add(con);
                }
            }
        }

        private void comboBoxContests_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (comboBoxContests.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали контест!");
                return;
            }
            contest = comboBoxContests.SelectedItem.ToString();
            DialogResult = DialogResult.OK;
        }
    }
}
