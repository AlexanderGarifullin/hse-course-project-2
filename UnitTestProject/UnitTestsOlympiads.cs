using FSPSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTestsOlympiads
    {
        #region ParticipantType
        [TestMethod]
        public void ValidateInput_ParticipantTypeIsNull_falseReturned()
        {
            AddOlympiadForm addOlympiadForm = new AddOlympiadForm();   

            bool isFalse = addOlympiadForm.ValidateInput();

            Assert.IsFalse(isFalse);
        }
        #endregion ParticipantType
        #region Name
        [TestMethod]
        public void ValidateInput_NameIsEmpty_falseReturned()
        {
            AddOlympiadForm addOlympiadForm = new AddOlympiadForm();
            addOlympiadForm.comboBoxParticipateType.Items.Add("1");
            addOlympiadForm.comboBoxParticipateType.SelectedItem = "1";
            addOlympiadForm.textBoxName.Text = "";

            bool isFalse = addOlympiadForm.ValidateInput();

            Assert.IsFalse(isFalse);
        }
        #endregion Name
        #region Year
        [TestMethod]
        public void ValidateInput_YearIsEmpty_falseReturned()
        {
            AddOlympiadForm addOlympiadForm = new AddOlympiadForm();
            addOlympiadForm.comboBoxParticipateType.Items.Add("1");
            addOlympiadForm.comboBoxParticipateType.SelectedItem = "1";
            addOlympiadForm.textBoxName.Text = "Nmae";
            addOlympiadForm.textBoxYear.Text = "";

            bool isFalse = addOlympiadForm.ValidateInput();

            Assert.IsFalse(isFalse);
        }
        [TestMethod]
        public void ValidateInput_YearIsLessThanMinimumYear_falseReturned()
        {
            AddOlympiadForm addOlympiadForm = new AddOlympiadForm();
            addOlympiadForm.comboBoxParticipateType.Items.Add("1");
            addOlympiadForm.comboBoxParticipateType.SelectedItem = "1";
            addOlympiadForm.textBoxName.Text = "Nmae";
            addOlympiadForm.textBoxYear.Text = "1000";
            addOlympiadForm.textBoxBP.Text = "20";
            addOlympiadForm.textBoxBPperTask.Text = "20";

            bool isFalse = addOlympiadForm.ValidateInput();

            Assert.IsFalse(isFalse);
        }
        [TestMethod]
        public void ValidateInput_YearIsGreaterThanMaximumYear_falseReturned()
        {
            AddOlympiadForm addOlympiadForm = new AddOlympiadForm();
            addOlympiadForm.comboBoxParticipateType.Items.Add("1");
            addOlympiadForm.comboBoxParticipateType.SelectedItem = "1";
            addOlympiadForm.textBoxName.Text = "Nmae";
            addOlympiadForm.textBoxYear.Text = "3000";
            addOlympiadForm.textBoxBP.Text = "20";
            addOlympiadForm.textBoxBPperTask.Text = "20";

            bool isFalse = addOlympiadForm.ValidateInput();

            Assert.IsFalse(isFalse);
        }
        [TestMethod]
        public void ValidateInput_YearIsNotNumber_falseReturned()
        {
            AddOlympiadForm addOlympiadForm = new AddOlympiadForm();
            addOlympiadForm.comboBoxParticipateType.Items.Add("1");
            addOlympiadForm.comboBoxParticipateType.SelectedItem = "1";
            addOlympiadForm.textBoxName.Text = "Nmae";
            addOlympiadForm.textBoxYear.Text = "ad";
            addOlympiadForm.textBoxBP.Text = "20";
            addOlympiadForm.textBoxBPperTask.Text = "20";

            bool isFalse = addOlympiadForm.ValidateInput();

            Assert.IsFalse(isFalse);
        }
        [TestMethod]
        public void ValidateInput_YearIsMaximumYear_trueReturned()
        {
            AddOlympiadForm addOlympiadForm = new AddOlympiadForm();
            addOlympiadForm.comboBoxParticipateType.Items.Add("1");
            addOlympiadForm.comboBoxParticipateType.SelectedItem = "1";
            addOlympiadForm.textBoxName.Text = "Nmae";
            addOlympiadForm.textBoxYear.Text = "2023";
            addOlympiadForm.textBoxBP.Text = "20";
            addOlympiadForm.textBoxBPperTask.Text = "20";

            bool isTrue = addOlympiadForm.ValidateInput();

            Assert.IsTrue(isTrue);
        }
        [TestMethod]
        public void ValidateInput_YearIsMinimumYear_trueReturned()
        {
            AddOlympiadForm addOlympiadForm = new AddOlympiadForm();
            addOlympiadForm.comboBoxParticipateType.Items.Add("1");
            addOlympiadForm.comboBoxParticipateType.SelectedItem = "1";
            addOlympiadForm.textBoxName.Text = "Nmae";
            addOlympiadForm.textBoxYear.Text = "2021";
            addOlympiadForm.textBoxBP.Text = "20";
            addOlympiadForm.textBoxBPperTask.Text = "20";

            bool isTrue = addOlympiadForm.ValidateInput();

            Assert.IsTrue(isTrue);
        }
        [TestMethod]
        public void ValidateInput_YearInAvailableRange_trueReturned()
        {
            AddOlympiadForm addOlympiadForm = new AddOlympiadForm();
            addOlympiadForm.comboBoxParticipateType.Items.Add("1");
            addOlympiadForm.comboBoxParticipateType.SelectedItem = "1";
            addOlympiadForm.textBoxName.Text = "Nmae";
            addOlympiadForm.textBoxYear.Text = "2022";
            addOlympiadForm.textBoxBP.Text = "20";
            addOlympiadForm.textBoxBPperTask.Text = "20";

            bool isTrue = addOlympiadForm.ValidateInput();

            Assert.IsTrue(isTrue);
        }
        #endregion Year
        #region BP
        [TestMethod]
        public void ValidateInput_BPIsEmpty_falseReturned()
        {
            AddOlympiadForm addOlympiadForm = new AddOlympiadForm();
            addOlympiadForm.comboBoxParticipateType.Items.Add("1");
            addOlympiadForm.comboBoxParticipateType.SelectedItem = "1";
            addOlympiadForm.textBoxName.Text = "Nmae";
            addOlympiadForm.textBoxYear.Text = "2022";
            addOlympiadForm.textBoxBP.Text = "";

            bool isFalse = addOlympiadForm.ValidateInput();

            Assert.IsFalse(isFalse);
        }
        [TestMethod]
        public void ValidateInput_BPIsZero_falseReturned()
        {
            AddOlympiadForm addOlympiadForm = new AddOlympiadForm();
            addOlympiadForm.comboBoxParticipateType.Items.Add("1");
            addOlympiadForm.comboBoxParticipateType.SelectedItem = "1";
            addOlympiadForm.textBoxName.Text = "Nmae";
            addOlympiadForm.textBoxYear.Text = "2022";
            addOlympiadForm.textBoxBP.Text = "0";
            addOlympiadForm.textBoxBPperTask.Text = "20";

            bool isFalse = addOlympiadForm.ValidateInput();

            Assert.IsFalse(isFalse);
        }
        [TestMethod]
        public void ValidateInput_BPIsLessZero_falseReturned()
        {
            AddOlympiadForm addOlympiadForm = new AddOlympiadForm();
            addOlympiadForm.comboBoxParticipateType.Items.Add("1");
            addOlympiadForm.comboBoxParticipateType.SelectedItem = "1";
            addOlympiadForm.textBoxName.Text = "Nmae";
            addOlympiadForm.textBoxYear.Text = "2022";
            addOlympiadForm.textBoxBP.Text = "-20";
            addOlympiadForm.textBoxBPperTask.Text = "20";

            bool isFalse = addOlympiadForm.ValidateInput();

            Assert.IsFalse(isFalse);
        }
        [TestMethod]
        public void ValidateInput_BPIsNotNumber_falseReturned()
        {
            AddOlympiadForm addOlympiadForm = new AddOlympiadForm();
            addOlympiadForm.comboBoxParticipateType.Items.Add("1");
            addOlympiadForm.comboBoxParticipateType.SelectedItem = "1";
            addOlympiadForm.textBoxName.Text = "Nmae";
            addOlympiadForm.textBoxYear.Text = "2022";
            addOlympiadForm.textBoxBP.Text = "ada";
            addOlympiadForm.textBoxBPperTask.Text = "20";

            bool isFalse = addOlympiadForm.ValidateInput();

            Assert.IsFalse(isFalse);
        }
        [TestMethod]
        public void ValidateInput_BPIsOk_trueReturned()
        {
            AddOlympiadForm addOlympiadForm = new AddOlympiadForm();
            addOlympiadForm.comboBoxParticipateType.Items.Add("1");
            addOlympiadForm.comboBoxParticipateType.SelectedItem = "1";
            addOlympiadForm.textBoxName.Text = "Nmae";
            addOlympiadForm.textBoxYear.Text = "2022";
            addOlympiadForm.textBoxBP.Text = "20";
            addOlympiadForm.textBoxBPperTask.Text = "20";

            bool isTrue = addOlympiadForm.ValidateInput();

            Assert.IsTrue(isTrue);
        }
        #endregion BP
        #region BP
        [TestMethod]
        public void ValidateInput_BppertaskIsEmpty_falseReturned()
        {
            AddOlympiadForm addOlympiadForm = new AddOlympiadForm();
            addOlympiadForm.comboBoxParticipateType.Items.Add("1");
            addOlympiadForm.comboBoxParticipateType.SelectedItem = "1";
            addOlympiadForm.textBoxName.Text = "Nmae";
            addOlympiadForm.textBoxYear.Text = "2022";
            addOlympiadForm.textBoxBP.Text = "20";
            addOlympiadForm.textBoxBPperTask.Text = "";

            bool isFalse = addOlympiadForm.ValidateInput();

            Assert.IsFalse(isFalse);
        }
        [TestMethod]
        public void ValidateInput_BppertaskIsZero_falseReturned()
        {
            AddOlympiadForm addOlympiadForm = new AddOlympiadForm();
            addOlympiadForm.comboBoxParticipateType.Items.Add("1");
            addOlympiadForm.comboBoxParticipateType.SelectedItem = "1";
            addOlympiadForm.textBoxName.Text = "Nmae";
            addOlympiadForm.textBoxYear.Text = "2022";
            addOlympiadForm.textBoxBP.Text = "20";
            addOlympiadForm.textBoxBPperTask.Text = "0";

            bool isFalse = addOlympiadForm.ValidateInput();

            Assert.IsFalse(isFalse);
        }
        [TestMethod]
        public void ValidateInput_BppertaskIsLessZero_falseReturned()
        {
            AddOlympiadForm addOlympiadForm = new AddOlympiadForm();
            addOlympiadForm.comboBoxParticipateType.Items.Add("1");
            addOlympiadForm.comboBoxParticipateType.SelectedItem = "1";
            addOlympiadForm.textBoxName.Text = "Nmae";
            addOlympiadForm.textBoxYear.Text = "2022";
            addOlympiadForm.textBoxBP.Text = "20";
            addOlympiadForm.textBoxBPperTask.Text = "-10";

            bool isFalse = addOlympiadForm.ValidateInput();

            Assert.IsFalse(isFalse);
        }
        [TestMethod]
        public void ValidateInput_BppertaskIsNotNumber_falseReturned()
        {
            AddOlympiadForm addOlympiadForm = new AddOlympiadForm();
            addOlympiadForm.comboBoxParticipateType.Items.Add("1");
            addOlympiadForm.comboBoxParticipateType.SelectedItem = "1";
            addOlympiadForm.textBoxName.Text = "Nmae";
            addOlympiadForm.textBoxYear.Text = "2022";
            addOlympiadForm.textBoxBP.Text = "20";
            addOlympiadForm.textBoxBPperTask.Text = "dadad";

            bool isFalse = addOlympiadForm.ValidateInput();

            Assert.IsFalse(isFalse);
        }
        [TestMethod]
        public void ValidateInput_BppertaskIsOk_trueReturned()
        {
            AddOlympiadForm addOlympiadForm = new AddOlympiadForm();
            addOlympiadForm.comboBoxParticipateType.Items.Add("1");
            addOlympiadForm.comboBoxParticipateType.SelectedItem = "1";
            addOlympiadForm.textBoxName.Text = "Nmae";
            addOlympiadForm.textBoxYear.Text = "2022";
            addOlympiadForm.textBoxBP.Text = "20";
            addOlympiadForm.textBoxBPperTask.Text = "20";

            bool isTrue = addOlympiadForm.ValidateInput();

            Assert.IsTrue(isTrue);
        }
        #endregion BP
    }
}
