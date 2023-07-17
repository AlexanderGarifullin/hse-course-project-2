using FSPSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTestsTeachers
    {
        #region LastName
        [TestMethod]
        public void ValidateInput_LastNameIsEmpty_falseReturned()
        {
            TeacherForm teacherForm = new TeacherForm();
            teacherForm.textBoxLastName.Text = "";

            bool isFalse = teacherForm.ValidateInput();

            Assert.IsFalse(isFalse);
        }
        [TestMethod]
        public void ValidateInput_LastNameHaveNotAvailableSymbols_falseReturned()
        {
            TeacherForm teacherForm = new TeacherForm();
            teacherForm.textBoxLastName.Text = "ZZZZZ";

            bool isFalse = teacherForm.ValidateInput();

            Assert.IsFalse(isFalse);
        }
        [TestMethod]
        public void ValidateInput_LastNameLenghtsLessThan2_falseReturned()
        {
            TeacherForm teacherForm = new TeacherForm();
            teacherForm.textBoxLastName.Text = "Я";

            bool isFalse = teacherForm.ValidateInput();

            Assert.IsFalse(isFalse);
        }
        #endregion LastName
        #region FirstName
        [TestMethod]
        public void ValidateInput_FirstNameIsEmpty_falseReturned()
        {
            TeacherForm teacherForm = new TeacherForm();
            teacherForm.textBoxLastName.Text = "Петр";
            teacherForm.textBoxFirstName.Text = "";

            bool isFalse = teacherForm.ValidateInput();

            Assert.IsFalse(isFalse);
        }
        [TestMethod]
        public void ValidateInput_FirstNameHaveNotAvailableSymbols_falseReturned()
        {
            TeacherForm teacherForm = new TeacherForm();
            teacherForm.textBoxLastName.Text = "Петр";
            teacherForm.textBoxFirstName.Text = "ZZZZ";

            bool isFalse = teacherForm.ValidateInput();

            Assert.IsFalse(isFalse);
        }
        [TestMethod]
        public void ValidateInput_FirstNameLenghtsLessThan2_falseReturned()
        {
            TeacherForm teacherForm = new TeacherForm();
            teacherForm.textBoxLastName.Text = "Петр";
            teacherForm.textBoxFirstName.Text = "Я";

            bool isFalse = teacherForm.ValidateInput();

            Assert.IsFalse(isFalse);
        }
        #endregion FirstName
        #region MiddleName
        [TestMethod]
        public void ValidateInput_MiddleNameIsEmpty_falseReturned()
        {
            TeacherForm teacherForm = new TeacherForm();
            teacherForm.textBoxLastName.Text = "Петр";
            teacherForm.textBoxFirstName.Text = "Петр";
            teacherForm.textBoxMiddleName.Text = "";

            bool isFalse = teacherForm.ValidateInput();

            Assert.IsFalse(isFalse);
        }
        [TestMethod]
        public void ValidateInput_MiddleNameHaveNotAvailableSymbols_falseReturned()
        {
            TeacherForm teacherForm = new TeacherForm();
            teacherForm.textBoxLastName.Text = "Петр";
            teacherForm.textBoxFirstName.Text = "Петр";
            teacherForm.textBoxMiddleName.Text = "!!11";

            bool isFalse = teacherForm.ValidateInput();

            Assert.IsFalse(isFalse);
        }
        [TestMethod]
        public void ValidateInput_MiddleNameLenghtsLessThan2_falseReturned()
        {
            TeacherForm teacherForm = new TeacherForm();
            teacherForm.textBoxLastName.Text = "Петр";
            teacherForm.textBoxFirstName.Text = "Петр";
            teacherForm.textBoxMiddleName.Text = "Я";

            bool isFalse = teacherForm.ValidateInput();

            Assert.IsFalse(isFalse);
        }
        #endregion MiddleName
        #region CodeforcesNickname
        [TestMethod]
        public void ValidateInput_CodeforcesNicknameHaveUnavailableSymbols_falseReturned()
        {
            TeacherForm teacherForm = new TeacherForm();
            teacherForm.textBoxLastName.Text = "Петр";
            teacherForm.textBoxFirstName.Text = "Петр";
            teacherForm.textBoxMiddleName.Text = "Петр";
            teacherForm.textBoxCodeforcesNickName.Text = "Петр";

            bool isFalse = teacherForm.ValidateInput();

            Assert.IsFalse(isFalse);
        }
        [TestMethod]
        public void ValidateInput_CodeforcesNicknameHaveNotLetters_falseReturned()
        {
            TeacherForm teacherForm = new TeacherForm();
            teacherForm.textBoxLastName.Text = "Петр";
            teacherForm.textBoxFirstName.Text = "Петр";
            teacherForm.textBoxMiddleName.Text = "Петр";
            teacherForm.textBoxCodeforcesNickName.Text = "1234";

            bool isFalse = teacherForm.ValidateInput();

            Assert.IsFalse(isFalse);
        }
        #endregion CodeforcesNickname
        #region Position
        [TestMethod]
        public void ValidateInput_PositionEmpty_falseReturned()
        {
            TeacherForm teacherForm = new TeacherForm();
            teacherForm.textBoxLastName.Text = "Петр";
            teacherForm.textBoxFirstName.Text = "Петр";
            teacherForm.textBoxMiddleName.Text = "Петр";
            teacherForm.textBoxCodeforcesNickName.Text = "petr";
            teacherForm.textBoxPosition.Text = "";

            bool isFalse = teacherForm.ValidateInput();

            Assert.IsFalse(isFalse);
        }
        [TestMethod]
        public void ValidateInput_AllCorrect_trueReturned()
        {
            TeacherForm teacherForm = new TeacherForm();
            teacherForm.textBoxLastName.Text = "Петр";
            teacherForm.textBoxFirstName.Text = "Петр";
            teacherForm.textBoxMiddleName.Text = "Петр";
            teacherForm.textBoxCodeforcesNickName.Text = "petr";
            teacherForm.textBoxPosition.Text = "Студент";

            bool isTrue = teacherForm.ValidateInput();

            Assert.IsTrue(isTrue);
        }
        #endregion Position
    }
}
