using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FSPSystem
{
    public partial class DataViewForm : Form
    {
        MainForm mainForm;
        Tables table;
        bool uploaded = false;
        public  Dictionary<string, int> groupsName = new Dictionary<string, int>();
        public DataTable dataTable = new DataTable();
        public DataViewForm(MainForm _mainForm, Tables _table)
        {
            this.mainForm = _mainForm;
            this.table = _table;
            InitializeComponent();
            if (_table == Tables.Olympiads || _table == Tables.Contests)
            {
                buttonResults.Visible = true;
                buttonResults.Enabled = true;
                btnChange.Visible = false;
                btnChange.Enabled = false;
                if (_table == Tables.Contests)
                {
                    buttonTasks.Visible = true;
                    buttonTasks.Enabled = true;
                }
            }
            else
            {
                btnChange.Visible = true;
                btnChange.Enabled = true;
                buttonResults.Visible = false;
                buttonResults.Enabled = false;
                buttonTasks.Visible = false;
                buttonTasks.Enabled = false;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (uploaded)
            {
                switch (table)
                {
                    case Tables.Teams:
                        TeamsCrud.AddTeam(this);
                        break;
                    case Tables.Tasks:
                        TaskCrud.AddTask(this);
                        break;
                    case Tables.Lessons:
                        LessonCrud.AddLesson(this);
                        break;
                    case Tables.Students:
                        StudentsCrud.AddStudent(this);
                        break;
                    case Tables.Olympiads:
                        OlympiadsCrud.AddOlympiad(this);
                        break;
                    case Tables.Contests:
                        ContestsCrud.AddContest(this);
                        break;
                    case Tables.Teachers:
                        TeachersCrud.AddTeacher(this);
                        break;
                    default:
                        break;
                }
            }
            else MessageBox.Show("Ошибка! Вы не загрузили данные!");
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            if (uploaded)
            {
                if (dataGridView.CurrentCell == null) MessageBox.Show("Вы не выбрали ячейку для изменения!");
                else
                {
                    int selectedRowIndex = dataGridView.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dataGridView.SelectedCells[0].OwningRow;
                    int idToChange;
                    switch (table)
                    {
                        case Tables.Teams:
                            TeamsCrud.ChangeTeam(this, dataGridView.Rows[selectedRowIndex].Cells["TeamID"].Value.ToString(), dataGridView.Rows[selectedRowIndex].Cells["Команда"].Value.ToString());
                            break;
                        case Tables.Tasks:
                             idToChange = Convert.ToInt32(dataGridView.Rows[selectedRowIndex].Cells["TaskID"].Value);
                            TaskCrud.ChangeTask(this, idToChange);
                            break;
                        case Tables.Lessons:
                             idToChange = Convert.ToInt32(dataGridView.Rows[selectedRowIndex].Cells["LessonID"].Value);
                            string teacher = dataGridView.Rows[selectedRowIndex].Cells["ФИО"].Value.ToString();
                            string topic = dataGridView.Rows[selectedRowIndex].Cells["Тема занятия"].Value.ToString();
                            DateTime date = (DateTime)dataGridView.Rows[selectedRowIndex].Cells["Дата"].Value;
                             LessonCrud.ChangeLesson(this, idToChange, teacher, topic,  date);
                            break;
                        case Tables.Students:
                            StudentsCrud.ChangeStudent(this, selectedRowIndex, selectedRow.Cells[0].Value.ToString(), selectedRow.Cells[1].Value.ToString(), selectedRow.Cells[2].Value.ToString(),
                                selectedRow.Cells[3].Value.ToString(), selectedRow.Cells[4].Value.ToString(), selectedRow.Cells[5].Value.ToString(), selectedRow.Cells[6].Value.ToString()
                                 );
                            break;
                        case Tables.Olympiads:
                            OlympiadsCrud.ChangeOlympiad(this, selectedRow.Cells[0].Value.ToString(), selectedRow.Cells[1].Value.ToString(), selectedRow.Cells[2].Value.ToString(),
                                selectedRow.Cells[3].Value.ToString(), selectedRow.Cells[4].Value.ToString(), selectedRow.Cells[5].Value.ToString()
                                );
                            break;
                        case Tables.Contests:
                            ContestsCrud.ChangeContest(this, selectedRow.Cells[0].Value.ToString(), selectedRow.Cells[1].Value.ToString(), selectedRow.Cells[2].Value.ToString(), selectedRow.Cells[3].Value.ToString());
                            break;
                        case Tables.Teachers:
                            TeachersCrud.ChangeTeacher(this, selectedRow.Cells[0].Value.ToString(), selectedRow.Cells[1].Value.ToString(), selectedRow.Cells[2].Value.ToString(), selectedRow.Cells[3].Value.ToString(),
                             selectedRow.Cells[4].Value.ToString() , selectedRow.Cells[5].Value.ToString());
                            break;
                        default:
                            break;
                    }
                }
            }
            else MessageBox.Show("Ошибка! Вы не загрузили данные!");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (uploaded)
            {
                if (dataGridView.CurrentCell == null) MessageBox.Show("Вы не выбрали ячейку для удаления!");
                else
                {
                    int selectedRowIndex = dataGridView.SelectedCells[0].RowIndex;
                    int idToDelete = 0;  
                    switch (table)
                    {
                        case Tables.Teams:
                            idToDelete = Convert.ToInt32(dataGridView.Rows[selectedRowIndex].Cells["TeamID"].Value);
                            TeamsCrud.DeleteTeam(this, idToDelete);
                            break;
                        case Tables.Tasks:
                            idToDelete = Convert.ToInt32(dataGridView.Rows[selectedRowIndex].Cells["TaskID"].Value);
                            TaskCrud.DeleteTask(this, idToDelete);
                            break;
                        case Tables.Lessons:
                            idToDelete = Convert.ToInt32(dataGridView.Rows[selectedRowIndex].Cells["LessonID"].Value);
                            LessonCrud.DeleteLesson(this, idToDelete);
                            break;
                        case Tables.Students:
                            idToDelete = Convert.ToInt32(dataGridView.Rows[selectedRowIndex].Cells["StudentID"].Value);
                            StudentsCrud.DeleteStudent(this, idToDelete);
                            break;
                        case Tables.Olympiads:
                            idToDelete = Convert.ToInt32(dataGridView.Rows[selectedRowIndex].Cells["OlympiadID"].Value);
                            OlympiadsCrud.DeleteOlympiad(this, idToDelete);
                            break;
                        case Tables.Contests:
                            idToDelete = Convert.ToInt32(dataGridView.Rows[selectedRowIndex].Cells["ContestID"].Value);
                            ContestsCrud.DeleteContest(this, idToDelete);
                            break;
                        case Tables.Teachers:
                            idToDelete = Convert.ToInt32(dataGridView.Rows[selectedRowIndex].Cells["TeacherID"].Value);
                            TeachersCrud.DeleteTeacher(this, idToDelete);
                            break;
                        default:
                            break;
                    }
                    dataGridView.Rows.RemoveAt(selectedRowIndex);
                }
            }
            else
                MessageBox.Show("Ошибка! Вы не загрузили данные!");

        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            switch (table)
            {
                case Tables.Teams:
                    uploaded = true;
                    TeamsCrud.UploadTeams(this); 
                    break;
                case Tables.Tasks:
                    uploaded = true;
                    TaskCrud.UploadTasks(this);
                    break;
                case Tables.Lessons:
                    uploaded = true;
                    LessonCrud.UploadLesson(this);
                    break;
                case Tables.Students:
                    uploaded = true;
                    StudentsCrud.UploadStudent(this);
                    break;
                case Tables.Olympiads:
                    uploaded = true;
                    OlympiadsCrud.UploadOlympiads(this);
                    break;
                case Tables.Contests:
                    uploaded = true;
                    ContestsCrud.UploadContests(this);
                    break;
                case Tables.Teachers:
                    uploaded = true;
                    TeachersCrud.UploadTeachers(this);
                    break;
                default:
                    break;
            }
            dataGridView.CurrentCell = null;
        }

        private void buttonResults_Click(object sender, EventArgs e)
        {
            if (uploaded)
            {
                if (dataGridView.CurrentCell == null) MessageBox.Show("Вы не выбрали ячейку!");
                else
                {
                    int selectedRowIndex = 1;
                    try
                    {
                        selectedRowIndex = dataGridView.SelectedCells[0].RowIndex;
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        MessageBox.Show("Вы не выбрали контест");
                        return;
                    }
                        int celllId = 0;
                    string name;
                    switch (table)
                    {
                        case Tables.Olympiads:
                            celllId = Convert.ToInt32(dataGridView.Rows[selectedRowIndex].Cells["OlympiadID"].Value);
                            name = dataGridView.Rows[selectedRowIndex].Cells["Олимпиада"].Value.ToString();
                            OlympiadsCrud.UploadResults(this, celllId, name);
                            break;
                        case Tables.Contests:
                            celllId = Convert.ToInt32(dataGridView.Rows[selectedRowIndex].Cells["ContestID"].Value);
                            name = dataGridView.Rows[selectedRowIndex].Cells["Контест"].Value.ToString();
                            ContestsCrud.UploadResults(this, celllId, name);
                            break;
                        default:
                            break;
                    }
                }
            }
            else
                MessageBox.Show("Ошибка! Вы не загрузили данные!");
        }

        private void buttonTasks_Click(object sender, EventArgs e)
        {
            if (uploaded)
            {
                if (dataGridView.CurrentCell == null) MessageBox.Show("Вы не выбрали ячейку!");
                else
                {
                    int selectedRowIndex = dataGridView.SelectedCells[0].RowIndex;
                    int celllId = 0;
                    string name;
                    switch (table)
                    {
                        case Tables.Contests:
                            celllId = Convert.ToInt32(dataGridView.Rows[selectedRowIndex].Cells["ContestID"].Value);
                            name = dataGridView.Rows[selectedRowIndex].Cells["Контест"].Value.ToString();
                            ContestsCrud.UploadTasks(this, celllId, name);
                            break;
                        default:
                            break;
                    }
                }
            }
            else
                MessageBox.Show("Ошибка! Вы не загрузили данные!");
        }
    }
}

