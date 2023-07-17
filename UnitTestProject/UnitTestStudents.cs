using FSPSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTestStudents
    {
        #region Groups
        [TestMethod]
        public void ValidateInput_GroupIsNull_falseReturned()
        {
            StudentForm studentForm = new StudentForm();

            bool isFalse = studentForm.ValidateInput();

            Assert.IsFalse(isFalse);
        }
        #endregion Groups
        #region CodeforcesNickname
        [TestMethod]
        public void ValidateInput_CodeforcesNicknameIsEmpty_falseReturned()
        {
            StudentForm studentForm = new StudentForm();
            studentForm.comboBoxGroupNames.Items.Add("1");
            studentForm.comboBoxGroupNames.SelectedItem = "1";
            studentForm.textBoxCodeforcesNickname.Text = "";

            bool isFalse = studentForm.ValidateInput();

            Assert.IsFalse(isFalse);
        }
        [TestMethod]
        public void ValidateInput_CodeforcesNicknameWithUnavailableSymbols_falseReturned()
        {
            StudentForm studentForm = new StudentForm();
            studentForm.comboBoxGroupNames.Items.Add("1");
            studentForm.comboBoxGroupNames.SelectedItem = "1";
            studentForm.textBoxCodeforcesNickname.Text = "петря";

            bool isFalse = studentForm.ValidateInput();

            Assert.IsFalse(isFalse);
        }
        [TestMethod]
        public void ValidateInput_CodeforcesNicknameWithoutEngLetter_falseReturned()
        {
            StudentForm studentForm = new StudentForm();
            studentForm.comboBoxGroupNames.Items.Add("1");
            studentForm.comboBoxGroupNames.SelectedItem = "1";
            studentForm.textBoxCodeforcesNickname.Text = "12345";

            bool isFalse = studentForm.ValidateInput();

            Assert.IsFalse(isFalse);
        }
        #endregion CodeforcesNickname
        #region LastName
        [TestMethod]
        public void ValidateInput_LastNameIsEmpty_falseReturned()
        {
            StudentForm studentForm = new StudentForm();
            studentForm.comboBoxGroupNames.Items.Add("1");
            studentForm.comboBoxGroupNames.SelectedItem = "1";
            studentForm.textBoxCodeforcesNickname.Text = "pet";
            studentForm.textBoxLastName.Text = "";

            bool isFalse = studentForm.ValidateInput();

            Assert.IsFalse(isFalse);
        }
        [TestMethod]
        public void ValidateInput_LastNameLenIsOne_falseReturned()
        {
            StudentForm studentForm = new StudentForm();
            studentForm.comboBoxGroupNames.Items.Add("1");
            studentForm.comboBoxGroupNames.SelectedItem = "1";
            studentForm.textBoxCodeforcesNickname.Text = "pet";
            studentForm.textBoxLastName.Text = "Я";

            bool isFalse = studentForm.ValidateInput();

            Assert.IsFalse(isFalse);
        }
        [TestMethod]
        public void ValidateInput_LastNameWithUnavailableSymbols_falseReturned()
        {
            StudentForm studentForm = new StudentForm();
            studentForm.comboBoxGroupNames.Items.Add("1");
            studentForm.comboBoxGroupNames.SelectedItem = "1";
            studentForm.textBoxCodeforcesNickname.Text = "pet";
            studentForm.textBoxLastName.Text = "!!!dada";

            bool isFalse = studentForm.ValidateInput();

            Assert.IsFalse(isFalse);
        }
        #endregion LastName
        #region FirstName
        [TestMethod]
        public void ValidateInput_FirstNameIsEmpty_falseReturned()
        {
            StudentForm studentForm = new StudentForm();
            studentForm.comboBoxGroupNames.Items.Add("1");
            studentForm.comboBoxGroupNames.SelectedItem = "1";
            studentForm.textBoxCodeforcesNickname.Text = "pet";
            studentForm.textBoxLastName.Text = "Пет";
            studentForm.textBoxFirstName.Text = "";

            bool isFalse = studentForm.ValidateInput();

            Assert.IsFalse(isFalse);
        }
        [TestMethod]
        public void ValidateInput_FirstNameLenIsOne_falseReturned()
        {
            StudentForm studentForm = new StudentForm();
            studentForm.comboBoxGroupNames.Items.Add("1");
            studentForm.comboBoxGroupNames.SelectedItem = "1";
            studentForm.textBoxCodeforcesNickname.Text = "pet";
            studentForm.textBoxLastName.Text = "Пет";
            studentForm.textBoxFirstName.Text = "Я";

            bool isFalse = studentForm.ValidateInput();

            Assert.IsFalse(isFalse);
        }
        [TestMethod]
        public void ValidateInput_FirstNameWithUnavailableSymbols_falseReturned()
        {
            StudentForm studentForm = new StudentForm();
            studentForm.comboBoxGroupNames.Items.Add("1");
            studentForm.comboBoxGroupNames.SelectedItem = "1";
            studentForm.textBoxCodeforcesNickname.Text = "pet";
            studentForm.textBoxLastName.Text = "Пет";
            studentForm.textBoxFirstName.Text = "!11zz";

            bool isFalse = studentForm.ValidateInput();

            Assert.IsFalse(isFalse);
        }
        #endregion FirstName
        #region MiddleName
        [TestMethod]
        public void ValidateInput_MiddleNameIsEmpty_falseReturned()
        {
            StudentForm studentForm = new StudentForm();
            studentForm.comboBoxGroupNames.Items.Add("1");
            studentForm.comboBoxGroupNames.SelectedItem = "1";
            studentForm.textBoxCodeforcesNickname.Text = "pet";
            studentForm.textBoxLastName.Text = "Пет";
            studentForm.textBoxFirstName.Text = "Пет";
            studentForm.textBoxMiddleName.Text = "";

            bool isFalse = studentForm.ValidateInput();

            Assert.IsFalse(isFalse);
        }
        [TestMethod]
        public void ValidateInputMiddleNameLenIsOne_falseReturned()
        {
            StudentForm studentForm = new StudentForm();
            studentForm.comboBoxGroupNames.Items.Add("1");
            studentForm.comboBoxGroupNames.SelectedItem = "1";
            studentForm.textBoxCodeforcesNickname.Text = "pet";
            studentForm.textBoxLastName.Text = "Пет";
            studentForm.textBoxFirstName.Text = "Пет";
            studentForm.textBoxMiddleName.Text = "Я";

            bool isFalse = studentForm.ValidateInput();

            Assert.IsFalse(isFalse);
        }
        [TestMethod]
        public void ValidateInput_MiddleNameWithUnavailableSymbols_falseReturned()
        {
            StudentForm studentForm = new StudentForm();
            studentForm.comboBoxGroupNames.Items.Add("1");
            studentForm.comboBoxGroupNames.SelectedItem = "1";
            studentForm.textBoxCodeforcesNickname.Text = "pet";
            studentForm.textBoxLastName.Text = "Пет";
            studentForm.textBoxFirstName.Text = "Пет";
            studentForm.textBoxMiddleName.Text = "ZxZ!21";

            bool isFalse = studentForm.ValidateInput();

            Assert.IsFalse(isFalse);
        }
        [TestMethod]
        public void ValidateInput_AllOk_trueReturned()
        {
            StudentForm studentForm = new StudentForm();
            studentForm.comboBoxGroupNames.Items.Add("1");
            studentForm.comboBoxGroupNames.SelectedItem = "1";
            studentForm.textBoxCodeforcesNickname.Text = "pet";
            studentForm.textBoxLastName.Text = "Пет";
            studentForm.textBoxFirstName.Text = "Пет";
            studentForm.textBoxMiddleName.Text = "Пет";

            bool isTrue = studentForm.ValidateInput();

            Assert.IsTrue(isTrue);
        }
        #endregion MiddleName
    }
}
