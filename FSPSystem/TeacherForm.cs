using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FSPSystem
{
    public partial class TeacherForm : Form
    {
        public string LastName;
        public string FirstName;
        public string MiddleName;
        public string CodeforcesNickname;
        public string Position;



        public TeacherForm()
        {
            InitializeComponent();
        }
        public TeacherForm(string lastname, string firstname, string middlename, string nick, string pos):this()
        {
            textBoxLastName.Text = lastname;
            textBoxFirstName.Text = firstname;
            textBoxMiddleName.Text = middlename;
            textBoxCodeforcesNickName.Text = nick;
            textBoxPosition.Text = pos;
        }

        public bool ValidateInput()
        {
            string namesPatter = "^[А-ЯЁ][а-яё]+$";
            string codeforcesPattern = "^(?=.*[A-Za-z])[A-Za-z0-9_-]+$";

            LastName = textBoxLastName.Text.Trim();
            FirstName = textBoxFirstName.Text.Trim();
            MiddleName = textBoxMiddleName.Text.Trim();
            CodeforcesNickname = textBoxCodeforcesNickName.Text.Trim();
            Position = textBoxPosition.Text.Trim();
            if (LastName != "")
                LastName = LastName.Substring(0, 1).ToUpper() + LastName.Substring(1).ToLower();
            if (FirstName != "")
                FirstName = FirstName.Substring(0, 1).ToUpper() + FirstName.Substring(1).ToLower();
            if (MiddleName != "")
                MiddleName = MiddleName.Substring(0, 1).ToUpper() + MiddleName.Substring(1).ToLower();

            if (LastName == "" || !Regex.IsMatch(LastName, namesPatter) || LastName.Length < 2)
            {
                MessageBox.Show("Фамилия преподавателя может быть лишь из русских букв. Минимальная длина = 2!");
                return false;
            }

            if (FirstName == "" || !Regex.IsMatch(FirstName, namesPatter) || FirstName.Length < 2)
            {
                MessageBox.Show("Имя преподавателя может быть лишь из русских букв. Минимальная длина = 2!");
                return false;
            }

            if (MiddleName == "" || !Regex.IsMatch(MiddleName, namesPatter) || MiddleName.Length < 2)
            {
                MessageBox.Show("Отчество преподавателя может быть лишь из русских букв! Минимальная длина = 2!");
                return false;
            }

            if (CodeforcesNickname != "" && !Regex.IsMatch(CodeforcesNickname, codeforcesPattern))
            {
                MessageBox.Show("Ник преподавателя на Codeforces не соответствует требованиям! Ник обязан содержать хотя бы 1 латинскую букву и, возможно, цифры, символы '_' и '-' ");
                return false;
            }

            if (Position == "")
            {
                MessageBox.Show("Вы не написали должность преподавателя!");
                return false;
            }

            return true;
        }


        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (ValidateInput()) {
                DialogResult = DialogResult.OK;
            }
        }

        private void TeacherForm_Load(object sender, EventArgs e)
        {

        }
    }
}
