using NUnit.Framework;
using OpenQA.Selenium;

namespace NUnitTestProject1
{
    [Order(4)]
    class TechnologyTabTest
    {
        private Driver driver;
        public TechnologyTabTest()
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

            var senioritiesTab = driver.GetElement(By.CssSelector("a[href^=\"/technologies\"]"));
            senioritiesTab.Click();
        }

        [OneTimeTearDown]
        public void CloseBrowser()
        {
            driver.webDriver.Quit();
        }

        [Test]
        [Order(1)]
        public void Createtechnology()
        {
            string technologyName = "C#";

            var createTechnologyButton = driver.GetElement(By.ClassName("btn-text"));
            createTechnologyButton.Click();

            var technologyTitle = driver.GetElement(By.CssSelector("input[name='technology_title']"));
            technologyTitle.SendKeys(technologyName);

            var submitButton = driver.GetElement(By.CssSelector("button[type='Submit']"));
            submitButton.Click();

            var technologyExists = driver.IsElementPresent(By.CssSelector(".card-profile > a"));
            Assert.IsTrue(technologyExists, "Failed to create team");

            var technologyyElement = driver.GetElement(By.CssSelector(".card-profile > a"));
            technologyyElement.Click();

            var tehnologyTitleEdit = driver.GetElement(By.CssSelector("input[name='technology_title']"));
            Assert.AreEqual(technologyName, tehnologyTitleEdit.GetAttribute("value"));
        }
    }
}

