using FSPSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTestsTeams
    {
        #region TeamName
        [TestMethod]
        public void ValidateInput_teamNameIsEmpty_falseReturned()
        {
            TeamsForm teamsForm = new TeamsForm();
            teamsForm.textBoxTeamName.Text = "";

            bool isFalse = teamsForm.ValidateInput();
            
            Assert.IsFalse(isFalse);
        }
        [TestMethod]
        public void ValidateInput_teamNameIsNotEmpty_trueReturned()
        {
            TeamsForm teamsForm = new TeamsForm();
            teamsForm.textBoxTeamName.Text = "name";

            bool isTrue = teamsForm.ValidateInput();

            Assert.IsTrue(isTrue);
        }
        #endregion TeamName
    }
}
