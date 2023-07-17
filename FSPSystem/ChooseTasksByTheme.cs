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
    public partial class ChooseTasksByTheme : Form
    {
        public string theme;

        public Dictionary<string, int> theme_themid = new Dictionary<string, int>();

        public ChooseTasksByTheme()
        {
            InitializeComponent();
            using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
            {
                cn.Open();
                var sql = @"SELECT ThemeID
                        ,Trim(ThemeName)
                    FROM [FSPDB].[dbo].[Themes] ";
                var cmd = new SqlCommand(sql, cn);

                SqlDataReader reader1 = cmd.ExecuteReader();

                while (reader1.Read())
                {
                    int idol = reader1.GetInt32(0);
                    string ol = reader1.GetString(1);
                    theme_themid.Add(ol, idol);
                    comboBox1.Items.Add(ol);
                }
            }
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали тему!");
                return;
            }
            theme = comboBox1.SelectedItem.ToString();
            DialogResult = DialogResult.OK;
        }
    }
}
