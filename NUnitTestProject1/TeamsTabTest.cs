using NUnit.Framework;
using OpenQA.Selenium;

namespace NUnitTestProject1
{
    [Order(2)]
    public class TeamsTabTest
    {
        private Driver driver;
        public TeamsTabTest()
        {
            driver = new Driver();
        }

        [OneTimeSetUp]
        public void BeforeAllTests()
        {
            LoginPage loginPage = new LoginPage(driver);
            loginPage.BeforeAllTests();
            loginPage.EmptyForm();
            loginPage.LoginSuccesefull();

            var projectCard = driver.GetElement(By.CssSelector("a[href^=\"/projects\"]"));
            projectCard.Click();

            var rolesTab = driver.GetElement(By.CssSelector("a[href^=\"/roles\"]"));
            rolesTab.Click();
        }

        [OneTimeTearDown]
        public void CloseBrowser()
        {
            driver.webDriver.Quit();
        }

        [Test]
        [Order(1)]
        public void CreateTeam()
        {
            string teamName = "team name";

            var createTeamButton = driver.GetElement(By.ClassName("btn-text"));
            createTeamButton.Click();

            var teamTitle = driver.GetElement(By.CssSelector("input[name='role_name']"));
            teamTitle.SendKeys(teamName);

            var submitButton = driver.GetElement(By.CssSelector("button[type='Submit']"));
            submitButton.Click();

            var teamExists = driver.IsElementPresent(By.CssSelector(".card-profile > a"));
            Assert.IsTrue(teamExists, "Failed to create team");

            var teamElement = driver.GetElement(By.CssSelector(".card-profile > a"));
            teamElement.Click();

            var teamTitleEdit = driver.GetElement(By.CssSelector("input[name='role_name']"));
            Assert.AreEqual(teamName, teamTitleEdit.GetAttribute("value"));
        }

    }
}
