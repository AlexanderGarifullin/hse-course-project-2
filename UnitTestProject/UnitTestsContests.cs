using FSPSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;


namespace UnitTestProject
{
    [TestClass]
    public class UnitTestsContests
    {
        #region Duration
        [TestMethod]
        public void ValidateInput_durationNumberMoreThanMaximum_falseReturned()
        {
            AddContestForm addContestForm = new AddContestForm();
            addContestForm.textBoxName.Text = "Name";
            addContestForm.textBoxDuration.Text = "1000";
            bool isFalse = addContestForm.ValidateInput();
            Assert.IsFalse(isFalse);
        }
        [TestMethod]
        public void ValidateInput_durationNumberLessThanMinimum_falseReturned()
        {
            AddContestForm addContestForm = new AddContestForm();
            addContestForm.textBoxName.Text = "Name";
            addContestForm.textBoxDuration.Text = "-1000";
            bool isFalse = addContestForm.ValidateInput();
            Assert.IsFalse(isFalse);
        }
        [TestMethod]
        public void ValidateInput_durationIsNotNumber_falseReturned()
        {
            AddContestForm addContestForm = new AddContestForm();
            addContestForm.textBoxName.Text = "Name";
            addContestForm.textBoxDuration.Text = "duration";
            bool isFalse = addContestForm.ValidateInput();
            Assert.IsFalse(isFalse);
        }
        [TestMethod]
        public void ValidateInput_durationIsMaximumBorder_trueReturned()
        {
            AddContestForm addContestForm = new AddContestForm();
            addContestForm.textBoxName.Text = "Name";
            addContestForm.textBoxDuration.Text = "300";
            addContestForm.comboBoxParticipateType.SelectedItem = "Командный";
            bool isTrue = addContestForm.ValidateInput();
            Assert.IsTrue(isTrue);
        }
        [TestMethod]
        public void ValidateInput_durationIsMinimumBorder_trueReturned()
        {
            AddContestForm addContestForm = new AddContestForm();
            addContestForm.textBoxName.Text = "Name";
            addContestForm.textBoxDuration.Text = "60";
            addContestForm.comboBoxParticipateType.SelectedItem = "Командный";
            bool isTrue = addContestForm.ValidateInput();

            Assert.IsTrue(isTrue);
        }
        [TestMethod]
        public void ValidateInput_durationInAvaiableRange_trueReturned()
        {
            AddContestForm addContestForm = new AddContestForm();
            addContestForm.textBoxName.Text = "Name";
            addContestForm.textBoxDuration.Text = "100";
            addContestForm.comboBoxParticipateType.SelectedItem = "Командный";
            bool isTrue = addContestForm.ValidateInput();
            Assert.IsTrue(isTrue);
        }
        #endregion Duration
        #region Name
        [TestMethod]
        public void ValidateInput_nameIsEmpty_falseReturned()
        {
            AddContestForm addContestForm = new AddContestForm();
            addContestForm.textBoxName.Text = "";
            addContestForm.textBoxDuration.Text = "100";
            addContestForm.comboBoxParticipateType.SelectedItem = "Командный";
            bool isFalse = addContestForm.ValidateInput();
            Assert.IsFalse(isFalse);
        }
        #endregion Name
        #region ParticipantType
        [TestMethod]
        public void ValidateInput_participantTypeIsNull_falseReturned()
        {
            AddContestForm addContestForm = new AddContestForm();
            addContestForm.textBoxName.Text = "Name";
            addContestForm.textBoxDuration.Text = "100";
            addContestForm.comboBoxParticipateType.SelectedItem = "";
            bool isFalse = addContestForm.ValidateInput();
            Assert.IsFalse(isFalse);
        }
        #endregion ParticipantType
    }
}
