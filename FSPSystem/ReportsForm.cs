using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;


namespace FSPSystem
{
    public partial class ReportsForm : Form
    {
        public ReportsForm()
        {
            InitializeComponent();

        }

        private void buttonRating_Click(object sender, EventArgs e)
        {
            using (var package = new ExcelPackage())
            {
                var sql = @"SELECT
                      s.LastName as 'Фамилия',
                      s.FirstName as 'Имя',
                      s.MiddleName as 'Отчество',
                      s.CodeforcesNickname as 'Ник на Codeforces',
                      g.GroupName as 'Группа',
                      COALESCE(SUM(rp.RP), 0) AS RP,
                      COALESCE(SUM(op.BP), 0) AS BP,
                      COALESCE(SUM(rp.RP), 0) + COALESCE(SUM(op.BP), 0) AS Total
                    FROM
                      Students s
                      INNER JOIN Participants p ON s.StudentID = p.StudentID
                      INNER JOIN StudyGroups g ON s.GroupID = g.GroupID
                      LEFT JOIN (
                        SELECT
                          sol.ParticipantID,
                          SUM(CASE
                            WHEN sol.SolutionStatus = 'Решена' THEN s.TaskWeight
                            WHEN sol.SolutionStatus = 'Дорешена' THEN s.TaskWeight * f.Coefficient
                            ELSE 0
                          END) AS RP
                        FROM
                          Solutions sol
                          INNER JOIN TaskContests tc ON sol.TaskContestID = tc.TaskContestID
                          INNER JOIN Tasks t ON tc.TaskID = t.TaskID
                          INNER JOIN FirstDifficulties f ON t.FirstDifficultyID = f.FirstDifficultyID
						  INNER JOIN SecondDifficulties s on s.SecondDifficultyID = t.SecondDifficultyID
                        GROUP BY
                          sol.ParticipantID
                      ) AS rp ON p.ParticipantID = rp.ParticipantID
                      LEFT JOIN (
                        SELECT
                          po.ParticipantID,
                          SUM(o.BaseWeight + o.WeightPerProblem * po.SolvedProblemsCount) AS BP
                        FROM
                          ParticipantOlympiads po
                          INNER JOIN Olympiads o ON po.OlympiadID = o.OlympiadID
                        GROUP BY
                          po.ParticipantID
                      ) AS op ON p.ParticipantID = op.ParticipantID
                    GROUP BY
                      s.LastName,
                      s.FirstName,
                      s.MiddleName,
                      s.CodeforcesNickname,
                      g.GroupName
                    ORDER BY
                      Total DESC,
                      RP DESC,
                      BP DESC,
                      s.LastName ASC,
                      s.FirstName ASC,
                      s.MiddleName ASC,
                      s.CodeforcesNickname ASC,
                      g.GroupName ASC;
                    ";

                var worksheet = package.Workbook.Worksheets.Add("Rating");

                using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
                {
                    cn.Open();
                    using (var command = new SqlCommand(sql, cn))
                    {
                        // Выполняем запрос и получаем данные
                        using (var reader = command.ExecuteReader())
                        {
                            // Записываем названия столбцов в первую строку таблицы
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                worksheet.Cells[1, i + 1].Value = reader.GetName(i);
                                worksheet.Cells[1, i+1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                                worksheet.Cells[1, i + 1].Style.Font.Bold = true;
                            }

                            // Записываем данные в таблицу
                            int row = 2;
                            while (reader.Read())
                            {
                                for (int col = 0; col < reader.FieldCount; col++)
                                {
                                    worksheet.Cells[row, col + 1].Value = reader.GetValue(col);
                                }
                                row++;
                            }
                            worksheet.Cells[1, 1, row - 1, reader.FieldCount].AutoFitColumns();
                        }
                    }

                }
                // Добавляем строку заголовка
                worksheet.InsertRow(1, 1);
                worksheet.Cells[1, 1].Value = "Отчёт: Рейтинг факультатива";
                // Получаем последний столбец и объединяем ячейки в строке заголовка
                int lastColumn = worksheet.Dimension.End.Column;
                worksheet.Cells[1, lastColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                worksheet.Cells[1, 1, 1, lastColumn-1].Merge = true;
                // Записываем текущую дату в последнюю ячейку
                worksheet.Cells[1, lastColumn].Value = DateTime.Now.ToShortDateString();
                worksheet.Cells[1, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells[1, 1].Style.Font.Bold = true;

                lastColumn = worksheet.Dimension.End.Column;
                worksheet.Cells[worksheet.Dimension.End.Row, lastColumn].AutoFitColumns();

                var saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                saveFileDialog.FileName = "Rating.xlsx";
                saveFileDialog.RestoreDirectory = true;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var file = new FileInfo(saveFileDialog.FileName);                
                    try
                    {
                        package.SaveAs(file);
                    }
                    catch (InvalidOperationException)
                    {
                        MessageBox.Show("Перед тем как сохранять файл, закройте файл!");
                    }
                }
            }
        }

        private void buttonContestResult_Click(object sender, EventArgs e)
        {
            ChooseContestForm chooseContestForm = new ChooseContestForm();
            if (chooseContestForm.ShowDialog() == DialogResult.OK)
            {
                using (var package = new ExcelPackage())
                {
                    var sql = @"SELECT 
                        Trim(LastName) as 'Фамилия', 
                         Trim(FirstName) as 'Имя', 
                         Trim(MiddleName) as 'Отчество', 
                         Trim(GroupName) as 'Группа', 
                         Trim(CodeforcesNickname) as 'Ник на Codeforces', 
                        SUM(CASE 
                            WHEN SolutionStatus = 'Решена' THEN TaskWeight
                            WHEN SolutionStatus = 'Дорешена' THEN Coefficient * TaskWeight
                            ELSE 0
                        END) AS RP
                    FROM (
                        SELECT 
                            Students.LastName, 
                            Students.FirstName, 
                            Students.MiddleName, 
                            StudyGroups.GroupName, 
                            Students.CodeforcesNickname,
                            Solutions.SolutionStatus, 
                            FirstDifficulties.Coefficient, 
                            SecondDifficulties.TaskWeight,
                            TaskContests.ContestID
                        FROM Solutions  
                        LEFT JOIN Participants ON Solutions.ParticipantID = Participants.ParticipantID 
                        LEFT JOIN Students ON Students.StudentID = Participants.StudentID
                        LEFT JOIN StudyGroups ON StudyGroups.GroupID = Students.GroupID
                        LEFT JOIN TaskContests ON TaskContests.TaskContestID = Solutions.TaskContestID
                        LEFT JOIN Tasks ON Tasks.TaskID = TaskContests.TaskID
                        LEFT JOIN FirstDifficulties ON Tasks.FirstDifficultyID = FirstDifficulties.FirstDifficultyID
                        LEFT JOIN SecondDifficulties ON Tasks.SecondDifficultyID = SecondDifficulties.SecondDifficultyID
                        WHERE TaskContests.ContestID = @contestid
                    ) AS subquery 
                    GROUP BY LastName, FirstName, MiddleName , GroupName, CodeforcesNickname
                    ORDER BY RP DESC, LastName ASC, FirstName ASC, MiddleName ASC, GroupName ASC, CodeforcesNickname ASC;
                                        ";

                    var worksheet = package.Workbook.Worksheets.Add($"{chooseContestForm.contest}");

                    using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
                    {
                        cn.Open();

                        using (var command = new SqlCommand(sql, cn))
                        {
                            command.Parameters.AddWithValue("@contestid", chooseContestForm.contests_idcontest[chooseContestForm.contest]);
                            // Выполняем запрос и получаем данные
                            using (var reader = command.ExecuteReader())
                            {
                                // Записываем названия столбцов в первую строку таблицы
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    worksheet.Cells[1, i + 1].Value = reader.GetName(i);
                                    worksheet.Cells[1, i + 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                                    worksheet.Cells[1, i + 1].Style.Font.Bold = true;
                                }

                                // Записываем данные в таблицу
                                int row = 2;
                                while (reader.Read())
                                {
                                    for (int col = 0; col < reader.FieldCount; col++)
                                    {
                                        worksheet.Cells[row, col + 1].Value = reader.GetValue(col);
                                    }
                                    row++;
                                }
                                worksheet.Cells[1, 1, row - 1, reader.FieldCount].AutoFitColumns();
                            }
                        }

                    }
                    // Добавляем строку заголовка
                    worksheet.InsertRow(1, 1);
                    worksheet.Cells[1, 1].Value = $"Отчёт: Положение контеста {chooseContestForm.contest}";
                    // Получаем последний столбец и объединяем ячейки в строке заголовка
                    int lastColumn = worksheet.Dimension.End.Column;
                    worksheet.Cells[1, lastColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                    worksheet.Cells[1, 1, 1, lastColumn - 1].Merge = true;
                    // Записываем текущую дату в последнюю ячейку
                    worksheet.Cells[1, lastColumn].Value = DateTime.Now.ToShortDateString();
                    worksheet.Cells[1, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Cells[1, 1].Style.Font.Bold = true;

                    lastColumn = worksheet.Dimension.End.Column;
                    worksheet.Cells[worksheet.Dimension.End.Row, lastColumn].AutoFitColumns();

                    var saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                    saveFileDialog.FileName = $"{chooseContestForm.contest}.xlsx";
                    saveFileDialog.RestoreDirectory = true;
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        var file = new FileInfo(saveFileDialog.FileName);
                        try
                        {
                            package.SaveAs(file);
                        }
                        catch (InvalidOperationException)
                        {
                            MessageBox.Show("Перед тем как сохранять файл, закройте файл!");
                        }
                    }
                }
            }
        }

        private void buttonOlympiadResult_Click(object sender, EventArgs e)
        {
            ChooseOlympiadForm chooseOlympiadForm = new ChooseOlympiadForm();
            if (chooseOlympiadForm.ShowDialog() == DialogResult.OK)
            {
                using (var package = new ExcelPackage())
                {
                    var sql = @"
                                    SELECT 
                        Trim(LastName) as 'Фамилия', Trim(FirstName) as 'Имя', Trim(MiddleName) as 'Отчество', Trim(GroupName) as 'Группа', 
	                    Trim(CodeforcesNickname) as 'Ник на Codeforces', 
                        COALESCE(SUM(BaseWeight + WeightPerProblem * SolvedProblemsCount), 0) AS BP
                    FROM (
                        SELECT 
                            Students.LastName, Students.FirstName, Students.MiddleName, StudyGroups.GroupName, 
                            Students.CodeforcesNickname, ParticipantOlympiads.SolvedProblemsCount, Olympiads.BaseWeight, 
                            Olympiads.WeightPerProblem
                        FROM ParticipantOlympiads  
                        LEFT JOIN Participants ON ParticipantOlympiads.ParticipantID = Participants.ParticipantID 
                        LEFT JOIN Students ON Students.StudentID = Participants.StudentID
                        LEFT JOIN StudyGroups ON StudyGroups.GroupID = Students.GroupID
                        LEFT JOIN Olympiads ON Olympiads.OlympiadID = ParticipantOlympiads.OlympiadID
                        WHERE Olympiads.OlympiadID = @olympid
                    ) AS subquery 
                    GROUP BY LastName, FirstName, MiddleName, GroupName, CodeforcesNickname
                    ORDER BY BP DESC, LastName ASC, FirstName ASC, MiddleName ASC, GroupName ASC, CodeforcesNickname ASC
                        ";
                    var worksheet = package.Workbook.Worksheets.Add($"{chooseOlympiadForm.olymp}");

                    using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
                    {
                        cn.Open();

                        using (var command = new SqlCommand(sql, cn))
                        {
                            command.Parameters.AddWithValue("@olympid", chooseOlympiadForm.olymp_idolymp[chooseOlympiadForm.olymp]);
                            // Выполняем запрос и получаем данные
                            using (var reader = command.ExecuteReader())
                            {
                                // Записываем названия столбцов в первую строку таблицы
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    worksheet.Cells[1, i + 1].Value = reader.GetName(i);
                                    worksheet.Cells[1, i + 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                                    worksheet.Cells[1, i + 1].Style.Font.Bold = true;
                                }

                                // Записываем данные в таблицу
                                int row = 2;
                                while (reader.Read())
                                {
                                    for (int col = 0; col < reader.FieldCount; col++)
                                    {
                                        worksheet.Cells[row, col + 1].Value = reader.GetValue(col);
                                    }
                                    row++;
                                }
                                worksheet.Cells[1, 1, row - 1, reader.FieldCount].AutoFitColumns();
                            }
                        }

                    }
                    // Добавляем строку заголовка
                    worksheet.InsertRow(1, 1);
                    worksheet.Cells[1, 1].Value = $"Отчёт: Положение олимпиады {chooseOlympiadForm.olymp}";
                    // Получаем последний столбец и объединяем ячейки в строке заголовка
                    int lastColumn = worksheet.Dimension.End.Column;
                    worksheet.Cells[1, lastColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                    worksheet.Cells[1, 1, 1, lastColumn - 1].Merge = true;
                    // Записываем текущую дату в последнюю ячейку
                    worksheet.Cells[1, lastColumn].Value = DateTime.Now.ToShortDateString();
                    worksheet.Cells[1, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Cells[1, 1].Style.Font.Bold = true;

                    lastColumn = worksheet.Dimension.End.Column;
                    worksheet.Cells[worksheet.Dimension.End.Row, lastColumn].AutoFitColumns();

                    var saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                    saveFileDialog.FileName = $"{chooseOlympiadForm.olymp}.xlsx";
                    saveFileDialog.RestoreDirectory = true;
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        var file = new FileInfo(saveFileDialog.FileName);
                        try
                        {
                            package.SaveAs(file);
                        }
                        catch (InvalidOperationException)
                        {
                            MessageBox.Show("Перед тем как сохранять файл, закройте файл!");
                        }
                    }
                }
            }
        }

        private void buttonTasksByDifficulty_Click(object sender, EventArgs e)
        {
            ChooseTasksByDifficulty chooseTasksByDifficulty = new ChooseTasksByDifficulty();
            if (chooseTasksByDifficulty.ShowDialog() == DialogResult.OK)
            {
                using (var package = new ExcelPackage())
                {
                    var sql = @"
                                     SELECT 
                          t.CodeforcesTaskID,
                          STRING_AGG(TRIM(th.ThemeName), ', ') WITHIN GROUP (ORDER BY th.ThemeName) AS Themes,
                          sd.TaskWeight
                        FROM 
                          Tasks t
                          LEFT JOIN FirstDifficulties fd ON fd.FirstDifficultyID = t.FirstDifficultyID
                          LEFT JOIN SecondDifficulties sd ON sd.SecondDifficultyID = t.SecondDifficultyID
                          LEFT JOIN (
                            SELECT DISTINCT TaskID, ThemeName
                            FROM TaskThemes
                            LEFT JOIN Themes ON Themes.ThemeID = TaskThemes.ThemeID
                          ) th ON th.TaskID = t.TaskID
                        WHERE 
                          fd.FirstDifficultyID = @firstid
                        GROUP BY 
                          t.CodeforcesTaskID, sd.TaskWeight
                        ORDER BY 
                          sd.TaskWeight, t.CodeforcesTaskID, Themes 
                        ";
                    var worksheet = package.Workbook.Worksheets.Add($" Tasks {chooseTasksByDifficulty.coefficient}");

                    using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
                    {
                        cn.Open();

                        using (var command = new SqlCommand(sql, cn))
                        {
                            command.Parameters.AddWithValue("@firstid", chooseTasksByDifficulty.coefficient_idcoef[chooseTasksByDifficulty.coefficient]);
                            // Выполняем запрос и получаем данные
                            using (var reader = command.ExecuteReader())
                            {
                                // Записываем названия столбцов в первую строку таблицы
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    worksheet.Cells[1, i + 1].Value = reader.GetName(i);
                                    worksheet.Cells[1, i + 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                                    worksheet.Cells[1, i + 1].Style.Font.Bold = true;
                                }

                                // Записываем данные в таблицу
                                int row = 2;
                                while (reader.Read())
                                {
                                    for (int col = 0; col < reader.FieldCount; col++)
                                    {
                                        worksheet.Cells[row, col + 1].Value = reader.GetValue(col);
                                    }
                                    row++;
                                }
                                worksheet.Cells[1, 1, row - 1, reader.FieldCount].AutoFitColumns();
                            }
                        }

                    }
                    // Добавляем строку заголовка
                    worksheet.InsertRow(1, 1);
                    worksheet.Cells[1, 1].Value = $"Отчёт: Задачи сложности {chooseTasksByDifficulty.coefficient}";
                    // Получаем последний столбец и объединяем ячейки в строке заголовка
                    int lastColumn = worksheet.Dimension.End.Column;
                    worksheet.Cells[1, lastColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                    worksheet.Cells[1, 1, 1, lastColumn - 1].Merge = true;
                    // Записываем текущую дату в последнюю ячейку
                    worksheet.Cells[1, lastColumn].Value = DateTime.Now.ToShortDateString();
                    worksheet.Cells[1, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Cells[1, 1].Style.Font.Bold = true;

                    lastColumn = worksheet.Dimension.End.Column;
                    worksheet.Cells[worksheet.Dimension.End.Row, lastColumn].AutoFitColumns();

                    var saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                    saveFileDialog.FileName = $"Tasks {chooseTasksByDifficulty.coefficient}.xlsx";
                    saveFileDialog.RestoreDirectory = true;
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        var file = new FileInfo(saveFileDialog.FileName);
                        try
                        {
                            package.SaveAs(file);
                        }
                        catch (InvalidOperationException)
                        {
                            MessageBox.Show("Перед тем как сохранять файл, закройте файл!");
                        }
                    }
                }
            }
        }

        private void buttonTasksByTheme_Click(object sender, EventArgs e)
        {
            ChooseTasksByTheme chooseTasksByTheme = new ChooseTasksByTheme();
            if (chooseTasksByTheme.ShowDialog() == DialogResult.OK)
            {
                using (var package = new ExcelPackage())
                {
                    var sql = @"SELECT 
                          Tasks.CodeforcesTaskID, 
                          FirstDifficulties.Coefficient, 
                          SecondDifficulties.TaskWeight 
                        FROM 
                          TaskThemes 
                          LEFT JOIN Tasks ON Tasks.TaskID = TaskThemes.TaskID 
                          LEFT JOIN Themes ON Themes.ThemeID = TaskThemes.ThemeID 
                          LEFT JOIN FirstDifficulties ON FirstDifficulties.FirstDifficultyID = Tasks.FirstDifficultyID 
                          LEFT JOIN SecondDifficulties ON SecondDifficulties.SecondDifficultyID = Tasks.SecondDifficultyID 
                        WHERE 
                          Themes.ThemeID = @themeid
                        ORDER BY 
                          FirstDifficulties.Coefficient ASC, 
                          Tasks.CodeforcesTaskID ASC
                        ";
                    var worksheet = package.Workbook.Worksheets.Add($" Tasks {chooseTasksByTheme.theme}");

                    using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
                    {
                        cn.Open();

                        using (var command = new SqlCommand(sql, cn))
                        {
                            command.Parameters.AddWithValue("@themeid", chooseTasksByTheme.theme_themid[chooseTasksByTheme.theme]);
                            // Выполняем запрос и получаем данные
                            using (var reader = command.ExecuteReader())
                            {
                                // Записываем названия столбцов в первую строку таблицы
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    worksheet.Cells[1, i + 1].Value = reader.GetName(i);
                                    worksheet.Cells[1, i + 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                                    worksheet.Cells[1, i + 1].Style.Font.Bold = true;
                                }

                                // Записываем данные в таблицу
                                int row = 2;
                                while (reader.Read())
                                {
                                    for (int col = 0; col < reader.FieldCount; col++)
                                    {
                                        worksheet.Cells[row, col + 1].Value = reader.GetValue(col);
                                    }
                                    row++;
                                }
                                worksheet.Cells[1, 1, row - 1, reader.FieldCount].AutoFitColumns();
                            }
                        }

                    }
                    // Добавляем строку заголовка
                    worksheet.InsertRow(1, 1);
                    worksheet.Cells[1, 1].Value = $"Отчёт: Задачи на тему {chooseTasksByTheme.theme}";
                    // Получаем последний столбец и объединяем ячейки в строке заголовка
                    int lastColumn = worksheet.Dimension.End.Column;
                    worksheet.Cells[1, lastColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                    worksheet.Cells[1, 1, 1, lastColumn - 1].Merge = true;
                    // Записываем текущую дату в последнюю ячейку
                    worksheet.Cells[1, lastColumn].Value = DateTime.Now.ToShortDateString();
                    worksheet.Cells[1, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Cells[1, 1].Style.Font.Bold = true;

                    lastColumn = worksheet.Dimension.End.Column;
                    worksheet.Cells[worksheet.Dimension.End.Row, lastColumn].AutoFitColumns();

                    worksheet.Cells["1:1"].AutoFitColumns();
                    worksheet.Cells.AutoFitColumns();
                    var saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                    saveFileDialog.FileName = $"Tasks {chooseTasksByTheme.theme}.xlsx";
                    saveFileDialog.RestoreDirectory = true;
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        var file = new FileInfo(saveFileDialog.FileName);
                        try
                        {
                            package.SaveAs(file);
                        }
                        catch (InvalidOperationException)
                        {
                            MessageBox.Show("Перед тем как сохранять файл, закройте файл!");
                        }
                    }
                }
            }
        }
    }
}
