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
    public partial class LessonForm : Form
    {
        public string Topic;
        public string Teacher;
        public DateTime date;

        public LessonForm()
        {
            InitializeComponent();
            foreach (KeyValuePair<string, int> pair in LessonCrud.Teachers)
            {
                comboBoxTeachers.Items.Add(pair.Key);
            }
        }
        public LessonForm(int idchange, string teacher, string topic, DateTime date) : this()
        {
            textBoxLessonTopic.Text = topic;
            comboBoxTeachers.SelectedItem = teacher;
            dateTimePickerLessonDate.Value = date;
        }

        private void comboBoxTeachers_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void dateTimePickerLessonDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void LessonForm_Load(object sender, EventArgs e)
        {

        }
        public bool ValidateInput()
        {
            string topic = textBoxLessonTopic.Text.Trim();
            if (string.IsNullOrEmpty(topic))
            {
                MessageBox.Show("Вы не ввели тему занятия!");
                return false;
            }

            if (comboBoxTeachers.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали преподавателя!");
                return false;
            }
            string teacher = comboBoxTeachers.SelectedItem.ToString();

            DateTime date = dateTimePickerLessonDate.Value;
            if (date.Year < 2022 || date.Year > 2023)
            {
                MessageBox.Show("Год не может быть лишь от 2022 до 2023!");
                return false;
            }

            return true;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                Topic = textBoxLessonTopic.Text.Trim();
                Teacher = comboBoxTeachers.SelectedItem.ToString();
                date = dateTimePickerLessonDate.Value;
                DialogResult = DialogResult.OK;
            };
        }
    }
}
