using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace NUnitTestProject1
{
    [Order(6)]
    class EditPeopleTest
    {
        private Driver driver { get; set; }
        private PeopleTabTest peopleTabTest { get; set; }
        public EditPeopleTest()
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

            var peopleTab = driver.GetElement(By.CssSelector("a[href^=\"/people\"]"));
            peopleTab.Click();

            //create 3 people
            peopleTabTest = new PeopleTabTest(driver);

            for (int i = 1; i < 4; i++)
            {
                peopleTabTest.createPersonWithParam("Milan" + i + " Maksimovic" + i);
            }
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

                peopleTabTest.deletePerson(person);
            }

            driver.webDriver.Quit();
        }

        [Test]
        [Order(1)]
        public void SwitchNameAllPeople()
        {
            //swichName
            int i = 1;
            while (true)
            {
                var people = getPersonFromList(i);
                if (people == null)
                    break;

                people.Click();

                string newName;
                var teamNameField = driver.GetElement(By.CssSelector("input[name ='people_name']"));
                string teamNameValue = teamNameField.GetAttribute("value");

                string[] words = teamNameValue.Split(' ');

                newName = words[1] + " " + words[0];

                var teamTitle = driver.GetElement(By.CssSelector("input[name='people_name']"));
                teamTitle.Clear();

                teamTitle.SendKeys(newName);

                var submitButton = driver.GetElement(By.CssSelector("button[type='Submit']"));
                submitButton.Click();

                //check
                var peopleEdited = getPersonFromList(i);
                peopleEdited.Click();

                var teamNameFieldEdit = driver.GetElement(By.CssSelector("input[name ='people_name']"));
                Assert.AreEqual(newName, teamNameFieldEdit.GetAttribute("value"));

                var backButton = driver.GetElement(By.CssSelector(".btn-lg"));
                backButton.Click();
                i++;
            }
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

    }
}
