using FSPSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTestsLessons
    {
        #region Topic
        [TestMethod]
        public void ValidateInput_TopicIsEmpty_falseReturned()
        {
            LessonForm lessonForm = new LessonForm();
            lessonForm.textBoxLessonTopic.Text = "";

            bool isFalse = lessonForm.ValidateInput();

            Assert.IsFalse(isFalse);
        }
        #endregion Topic

        #region Teacher
        [TestMethod]
        public void ValidateInput_TeacherIsEmpty_falseReturned()
        {
            LessonForm lessonForm = new LessonForm();
            lessonForm.textBoxLessonTopic.Text = "Topic";
            lessonForm.comboBoxTeachers.Items.Add("1");
            lessonForm.comboBoxTeachers.SelectedItem = "";

            bool isFalse = lessonForm.ValidateInput();

            Assert.IsFalse(isFalse);
        }
        #endregion Teacher

        #region Date
        [TestMethod]
        public void ValidateInput_DateYaerIsLessThanMinimum_falseReturned()
        {
            LessonForm lessonForm = new LessonForm();
            lessonForm.textBoxLessonTopic.Text = "Topic";
            lessonForm.comboBoxTeachers.Items.Add("1");
            lessonForm.comboBoxTeachers.SelectedItem = "1";
            DateTime dateTime = new DateTime(2020, 12, 1);
            lessonForm.dateTimePickerLessonDate.Value = dateTime;

            bool isFalse = lessonForm.ValidateInput();

            Assert.IsFalse(isFalse);
        }
        [TestMethod]
        public void ValidateInput_DateYaerIsMoreThanMinimum_falseReturned()
        {
            LessonForm lessonForm = new LessonForm();
            lessonForm.textBoxLessonTopic.Text = "Topic";
            lessonForm.comboBoxTeachers.Items.Add("1");
            lessonForm.comboBoxTeachers.SelectedItem = "1";
            DateTime dateTime = new DateTime(2040, 12, 1);
            lessonForm.dateTimePickerLessonDate.Value = dateTime;

            bool isFalse = lessonForm.ValidateInput();

            Assert.IsFalse(isFalse);
        }
        [TestMethod]
        public void ValidateInput_DateYaerIsAvailable_trueReturned()
        {
            LessonForm lessonForm = new LessonForm();
            lessonForm.textBoxLessonTopic.Text = "Topic";
            lessonForm.comboBoxTeachers.Items.Add("1");
            lessonForm.comboBoxTeachers.SelectedItem = "1";
            DateTime dateTime = new DateTime(2023, 12, 1);
            lessonForm.dateTimePickerLessonDate.Value = dateTime;

            bool isTrue = lessonForm.ValidateInput();

            Assert.IsTrue(isTrue);
        }
        #endregion Date
    }
}
