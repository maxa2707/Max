using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace NUnitTestProject1
{
    [Order(5)]
    class PeopleTabTest
    {
        private Driver driver;
        private string defaultName = "milan maksimovic";
        public PeopleTabTest()
        {
            driver = new Driver();
        }

        public PeopleTabTest(Driver d)
        {
            driver = d;
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

            var peopleTab = driver.GetElement(By.CssSelector("a[href^=\"/people\"]"));
            peopleTab.Click();
        }

        [OneTimeTearDown]
        public void CloseBrowser()
        {
            int i = 1;
            while (true)
            {
                var person = getPersonFromList(i);
                if (person == null)
                    break;

                deletePerson(person);
            }

            driver.webDriver.Quit();
        }

        private IWebElement getPersonFromList(int i)
        {
            IWebElement personElement = null;
            try
            {
                personElement = driver.GetElement(By.CssSelector(".card-profile a:nth-of-type(" + i + ")"));
            }
            catch (Exception)
            {
                Assert.IsNotNull(personElement, "Person not exist");
            }
            return personElement;
        }

        [Test]
        [Order(1)]
        public void CreatePerson()
        {
            createPersonWithParam();

            Assert.IsTrue(driver.IsElementPresent(By.CssSelector(".card-profile a:nth-of-type(1)")), "Failed to create person");

            var personElement = driver.GetElement(By.CssSelector(".card-profile a:nth-of-type(1)"));
            personElement.Click();

            var fullNameEdit = driver.GetElement(By.CssSelector("input[name='people_name']"));
            if (fullNameEdit != null)
                Assert.AreEqual(defaultName, fullNameEdit.GetAttribute("value"));

            var backButton = driver.GetElement(By.CssSelector(".btn-lg"));
            backButton.Click();
        }

        public void createPersonWithParam(string personName = null)
        {
            if (personName == null)
                personName = defaultName;

            var createPersonButton = driver.GetElement(By.ClassName("btn-text"));
            createPersonButton.Click();

            var fullName = driver.GetElement(By.CssSelector("input[name='people_name']"));
            fullName.SendKeys(personName);

            var selectTechnology = driver.GetElement(By.CssSelector("div:nth-child(2) > div > div> div.react-dropdown-select-content.react-dropdown-select-type-multi.css-jznujr-ContentComponent.e1gn6jc30"));
            selectTechnology.Click();

            var pickTechnology = driver.GetElement(By.CssSelector("div:nth-child(2) > div > div > div.react-dropdown-select-dropdown.react-dropdown-select-dropdown-position-bottom.css-h6zzds-DropDown.e1qjn9k90 > span"));
            pickTechnology.Click();

            var selectSeniority = driver.GetElement(By.CssSelector("div:nth-child(3) > div > div > div.react-dropdown-select-content.react-dropdown-select-type-single.css-jznujr-ContentComponent.e1gn6jc30"));
            selectSeniority.Click();

            var pickSeniority = driver.GetElement(By.CssSelector("div:nth-child(3) > div > div > div.react-dropdown-select-dropdown.react-dropdown-select-dropdown-position-bottom.css-h6zzds-DropDown.e1qjn9k90 > span:nth-child(1)"));
            pickSeniority.Click();

            var selectTeam = driver.GetElement(By.CssSelector("div:nth-child(4) > div > div > div.react-dropdown-select-content.react-dropdown-select-type-single.css-jznujr-ContentComponent.e1gn6jc30"));
            selectTeam.Click();

            var pickTeam = driver.GetElement(By.CssSelector("div:nth-child(4) > div > div > div.react-dropdown-select-dropdown.react-dropdown-select-dropdown-position-bottom.css-h6zzds-DropDown.e1qjn9k90 > span"));
            pickTeam.Click();

            var submitButton = driver.GetElement(By.CssSelector("button[type='Submit']"));
            submitButton.Click();
        }

        [Test]
        [Order(2)]
        public void DeletePerson()
        {
            var personElement = driver.GetElement(By.CssSelector(".card-profile a:nth-of-type(" + 1 + ")"));
            var personName = personElement.Text;

            deletePerson(personElement);

            var personElement2 = driver.GetElement(By.CssSelector(".card-profile a:nth-of-type(" + 1 + ")"));
            if (personElement2 != null)
                Assert.AreNotEqual(personName, personElement2.Text);
        }

        public void deletePerson(IWebElement personElement)
        {
            personElement.Click();

            var deleteButton = driver.GetElement(By.CssSelector("button[aria-label='delete-button']"));
            deleteButton.Click();

            var deleteModal = driver.GetElement(By.CssSelector(".sweet-alert"));
            Assert.IsNotNull(deleteModal);

            var cancelButton = driver.GetElement(By.CssSelector(".btn-default"));
            cancelButton.Click();

            deleteButton.Click();

            var deleteModal2 = driver.GetElement(By.CssSelector(".sweet-alert"));
            Assert.IsNotNull(deleteModal2);

            var confirmButton = driver.GetElement(By.CssSelector(".btn-danger"));
            confirmButton.Click();
        }

    }
}
