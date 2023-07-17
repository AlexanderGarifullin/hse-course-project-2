using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FSPSystem
{
    public partial class StudentForm : Form
    {
        public string LastName;
        public string FirstName;
        public string MiddleName;
        public string CodeforcesNickname;
        public string GroupName;

        public Dictionary<string, int> groupsNamse = new Dictionary<string, int>();
        public StudentForm()
        {
            InitializeComponent();
            using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
            {
                cn.Open();
                var sql = @"select GroupID, Trim(GroupName) as GroupName from StudyGroups";
                var cmd = new SqlCommand(sql, cn);

                SqlDataReader reader = cmd.ExecuteReader();

                Dictionary<string, int> groupsName = new Dictionary<string, int>();

                while (reader.Read())
                {
                    int groupId = reader.GetInt32(0);
                    string groupName = reader.GetString(1);
                    groupsName.Add(groupName, groupId);
                }
                groupsNamse = groupsName;

                foreach (KeyValuePair<string, int> pair in groupsName)
                {
                    comboBoxGroupNames.Items.Add(pair.Key);
                }
            }
        }


        public StudentForm(Dictionary<string, int> groupsName)
        {
            InitializeComponent();
            foreach (KeyValuePair<string, int> pair in groupsName)
            {
                comboBoxGroupNames.Items.Add(pair.Key);
            }
        }
        public StudentForm(Dictionary<string, int> groupsName , string LastName, string FirstName, string MiddleName, string CodeforcesNickname, string GroupName) : this(groupsName)
        {
            textBoxFirstName.Text = FirstName;
            textBoxMiddleName.Text = MiddleName;
            textBoxLastName.Text = LastName;
            textBoxCodeforcesNickname.Text = CodeforcesNickname;
            comboBoxGroupNames.SelectedItem = GroupName;
        }

        private void comboBoxGroupNames_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        public bool ValidateInput()
        {
            // Паттерны для валидации
            string namesPatter = "^[А-ЯЁ][а-яё]+$";
            string codeforcesPattern = "^(?=.*[A-Za-z])[A-Za-z0-9_-]+$";
            // Получаем значения полей и удаляем пробелы в начале и конце
            string lastName = textBoxLastName.Text.Trim();
            string firstName = textBoxFirstName.Text.Trim();
            string middleName = textBoxMiddleName.Text.Trim();
            string codeforcesNickname = textBoxCodeforcesNickname.Text.Trim();
            // Проверяем выбрана ли группа
            if (comboBoxGroupNames.SelectedItem == null)
            {
                MessageBox.Show($"Вы не выбрали группу!");
                return false;
            }
            // Получаем выбранную группу
            string groupName = comboBoxGroupNames.SelectedItem.ToString();
            // Форматируем поля с ФИО
            if (lastName != "")
                lastName = lastName.Substring(0, 1).ToUpper() + lastName.Substring(1).ToLower();
            if (firstName != "")
                firstName = firstName.Substring(0, 1).ToUpper() + firstName.Substring(1).ToLower();
            if (middleName != "")
                middleName = middleName.Substring(0, 1).ToUpper() + middleName.Substring(1).ToLower();
            // Проверяем соответствие ФИО и ника на Codeforces паттерну
            bool isMatchLastname = Regex.IsMatch(lastName, namesPatter);
            bool isMatchFirstName = Regex.IsMatch(firstName, namesPatter);
            bool isMatchMiddleName = Regex.IsMatch(middleName, namesPatter);
            bool isMatchCodeforcesNickname = Regex.IsMatch(codeforcesNickname, codeforcesPattern);
            // Выводим сообщение об ошибке, если соответствие не было найдено
            if (!isMatchCodeforcesNickname)
            {
                MessageBox.Show($"Ник студента на Codeforces не может быть {codeforcesNickname}! Ник обязан содержать хотя бы 1 латинскую буквы и, возможно, цирф, символы '_' и '-' ");
                return false;
            }
            else if (!isMatchLastname)
            {
                MessageBox.Show("Фамилия студента может быть лишь из русских букв. Минимальная длина = 2!");
                return false;
            }
            else if (!isMatchFirstName)
            {
                MessageBox.Show("Имя студента может быть лишь из русских букв. Минимальная длина = 2!");
                return false;
            }
            else if (!isMatchMiddleName)
            {
                MessageBox.Show("Отчество студента может быть лишь из русских букв! Минимальная длина = 2!");
                return false;
            }
            LastName = lastName;
            MiddleName = middleName;
            FirstName = firstName;
            CodeforcesNickname = codeforcesNickname;
            GroupName = groupName;
            return true;
        }


        private void btnOk_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                DialogResult = DialogResult.OK;
            }
        }
    }
}
