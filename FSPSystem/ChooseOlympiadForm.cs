using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FSPSystem
{
    public partial class ChooseOlympiadForm : Form
    {
        public string olymp;

        public Dictionary<string, int> olymp_idolymp = new Dictionary<string, int>();

        public ChooseOlympiadForm()
        {
            InitializeComponent();
            using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
            {
                cn.Open();
                var sql = @"select OlympiadID, Trim(OlympiadName) from Olympiads";
                var cmd = new SqlCommand(sql, cn);

                SqlDataReader reader1 = cmd.ExecuteReader();

                while (reader1.Read())
                {
                    int idol = reader1.GetInt32(0);
                    string ol = reader1.GetString(1);
                    olymp_idolymp.Add(ol, idol);
                    comboBoxChooseOlympiad.Items.Add(ol);
                }
            }
        }

        private void comboBoxChooseOlympiad_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (comboBoxChooseOlympiad.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали олимпиаду!");
                return;
            }
            olymp = comboBoxChooseOlympiad.SelectedItem.ToString();
            DialogResult = DialogResult.OK;
        }
    }
}
