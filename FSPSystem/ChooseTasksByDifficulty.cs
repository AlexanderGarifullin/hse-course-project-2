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
    public partial class ChooseTasksByDifficulty : Form
    {
        public double coefficient;

        public Dictionary<double, int> coefficient_idcoef = new Dictionary<double, int>();


        public ChooseTasksByDifficulty()
        {
            InitializeComponent();
            using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
            {
                cn.Open();
                var sql = @"select * from FirstDifficulties";
                var cmd = new SqlCommand(sql, cn);

                SqlDataReader reader1 = cmd.ExecuteReader();

                while (reader1.Read())
                {
                    int idol = reader1.GetInt32(0);
                    double ol = reader1.GetDouble(1);
                    coefficient_idcoef.Add(ol, idol);
                    comboBoxCoefficient.Items.Add(ol);
                }
            }
        }

        private void comboBoxCoefficient_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void comboBoxCoefficient_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (comboBoxCoefficient.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали сложность!");
                return;
            }
            coefficient = Convert.ToDouble(comboBoxCoefficient.SelectedItem.ToString());
            DialogResult = DialogResult.OK;
        }
    }
}
