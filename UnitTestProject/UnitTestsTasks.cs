using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using FSPSystem;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTestsTasks
    {
        #region Coefficient
        [TestMethod]
        public void ValidateInput_didNotChosenCoefficient_falseReturned()
        {
            TaskForm taskForm = new TaskForm();
            taskForm.comboBoxCoefficient.SelectedItem = "";

            bool isFalse = taskForm.ValidateInput();

            Assert.IsFalse(isFalse);
        }
        #endregion Coefficient
        #region TaskWeight
        [TestMethod]
        public void ValidateInput_didNotChosenTaskWeight_falseReturned()
        {
            TaskForm taskForm = new TaskForm();
            taskForm.comboBoxCoefficient.Items.Add("0.6");
            taskForm.comboBoxCoefficient.SelectedItem = "0.6";
            taskForm.comboBoxTaskWeight.SelectedItem = "";

            bool isFalse = taskForm.ValidateInput();

            Assert.IsFalse(isFalse);
        }
        #endregion TaskWeight
        #region CodeforcesID
        [TestMethod]
        public void ValidateInput_CodeforcesIDIsEmpty_falseReturned()
        {
            TaskForm taskForm = new TaskForm();
            taskForm.comboBoxCoefficient.Items.Add("0.6");
            taskForm.comboBoxCoefficient.SelectedItem = "0.6";
            taskForm.comboBoxTaskWeight.Items.Add("20");
            taskForm.comboBoxTaskWeight.SelectedItem = "20";
            taskForm.textBoxCFID.Text = "";

            bool isFalse = taskForm.ValidateInput();

            Assert.IsFalse(isFalse);
        }
        [TestMethod]
        public void ValidateInput_CodeforcesIDStartWithLettersAndWithNumbers_falseReturned()
        {
            TaskForm taskForm = new TaskForm();
            taskForm.comboBoxCoefficient.Items.Add("0.6");
            taskForm.comboBoxCoefficient.SelectedItem = "0.6";
            taskForm.comboBoxTaskWeight.Items.Add("20");
            taskForm.comboBoxTaskWeight.SelectedItem = "20";
            taskForm.textBoxCFID.Text = "A27";

            bool isFalse = taskForm.ValidateInput();

            Assert.IsFalse(isFalse);
        }
        [TestMethod]
        public void ValidateInput_CodeforcesIDOnlyLetters_falseReturned()
        {
            TaskForm taskForm = new TaskForm();
            taskForm.comboBoxCoefficient.Items.Add("0.6");
            taskForm.comboBoxCoefficient.SelectedItem = "0.6";
            taskForm.comboBoxTaskWeight.Items.Add("20");
            taskForm.comboBoxTaskWeight.SelectedItem = "20";
            taskForm.textBoxCFID.Text = "AA";

            bool isFalse = taskForm.ValidateInput();

            Assert.IsFalse(isFalse);
        }
        [TestMethod]
        public void ValidateInput_CodeforcesIDOnlyNumbers_falseReturned()
        {
            TaskForm taskForm = new TaskForm();
            taskForm.comboBoxCoefficient.Items.Add("0.6");
            taskForm.comboBoxCoefficient.SelectedItem = "0.6";
            taskForm.comboBoxTaskWeight.Items.Add("20");
            taskForm.comboBoxTaskWeight.SelectedItem = "20";
            taskForm.textBoxCFID.Text = "AA";

            bool isFalse = taskForm.ValidateInput();

            Assert.IsFalse(isFalse);
        }
        [TestMethod]
        public void ValidateInput_CodeforcesIDNotAvaibaleSymbols_falseReturned()
        {
            TaskForm taskForm = new TaskForm();
            taskForm.comboBoxCoefficient.Items.Add("0.6");
            taskForm.comboBoxCoefficient.SelectedItem = "0.6";
            taskForm.comboBoxTaskWeight.Items.Add("20");
            taskForm.comboBoxTaskWeight.SelectedItem = "20";
            taskForm.textBoxCFID.Text = "!!!";

            bool isFalse = taskForm.ValidateInput();

            Assert.IsFalse(isFalse);
        }
        [TestMethod]
        public void ValidateInput_CodeforcesIDStartWithZero_falseReturned()
        {
            TaskForm taskForm = new TaskForm();
            taskForm.comboBoxCoefficient.Items.Add("0.6");
            taskForm.comboBoxCoefficient.SelectedItem = "0.6";
            taskForm.comboBoxTaskWeight.Items.Add("20");
            taskForm.comboBoxTaskWeight.SelectedItem = "20";
            taskForm.textBoxCFID.Text = "0A";

            bool isFalse = taskForm.ValidateInput();

            Assert.IsFalse(isFalse);
        }
        [TestMethod]
        public void ValidateInput_CodeforcesIDMoreThanOneLetter_falseReturned()
        {
            TaskForm taskForm = new TaskForm();
            taskForm.comboBoxCoefficient.Items.Add("0.6");
            taskForm.comboBoxCoefficient.SelectedItem = "0.6";
            taskForm.comboBoxTaskWeight.Items.Add("20");
            taskForm.comboBoxTaskWeight.SelectedItem = "20";
            taskForm.textBoxCFID.Text = "1AA";

            bool isFalse = taskForm.ValidateInput();

            Assert.IsFalse(isFalse);
        }

        [TestMethod]
        public void ValidateInput_CodeforcesIDIsCorrect_trueReturned()
        {
            TaskForm taskForm = new TaskForm();
            taskForm.comboBoxCoefficient.Items.Add("0.6");
            taskForm.comboBoxCoefficient.SelectedItem = "0.6";
            taskForm.comboBoxTaskWeight.Items.Add("20");
            taskForm.comboBoxTaskWeight.SelectedItem = "20";
            taskForm.textBoxCFID.Text = "27A";

            bool isTrue = taskForm.ValidateInput();

            Assert.IsTrue(isTrue);
        }
        #endregion CodeforcesID
    }
}
