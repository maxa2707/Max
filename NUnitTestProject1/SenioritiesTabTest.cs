using NUnit.Framework;
using OpenQA.Selenium;

namespace NUnitTestProject1
{
    [Order(3)]
    class SenioritiesTabTest
    {
        private Driver driver;

        public SenioritiesTabTest()
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

            var senioritiesTab = driver.GetElement(By.CssSelector("a[href^=\"/seniorities\"]"));
            senioritiesTab.Click();
        }

        [OneTimeTearDown]
        public void CloseBrowser()
        {
            driver.webDriver.Quit();
        }

        [Test]
        [Order(1)]
        public void Createseniority()
        {
            string seniorityName = "Team Lead";

            var createSeniorityButton = driver.GetElement(By.ClassName("btn-text"));
            createSeniorityButton.Click();

            var seniorityTitle = driver.GetElement(By.CssSelector("input[name='seniority_title']"));
            seniorityTitle.SendKeys(seniorityName);

            var submitButton = driver.GetElement(By.CssSelector("button[type='Submit']"));
            submitButton.Click();

            var seniorityExists = driver.IsElementPresent(By.CssSelector(".card-profile > a"));
            Assert.IsTrue(seniorityExists, "Failed to create team");

            var seniorityElement = driver.GetElement(By.CssSelector(".card-profile > a"));
            seniorityElement.Click();

            var seniorityEdit = driver.GetElement(By.CssSelector("input[name='seniority_title']"));
            Assert.AreEqual(seniorityName, seniorityEdit.GetAttribute("value"));
        }
    }
}
