using NUnit.Framework;
using OpenQA.Selenium;

namespace NUnitTestProject1
{
    [Order(1)]
    public class LoginPage
    {
        private Driver driver;

        public LoginPage()
        {
            driver = new Driver();
        }

        public LoginPage(Driver d)
        {
            driver = d;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            EmptyForm();
        }

        [OneTimeSetUp]
        public void BeforeAllTests()
        {
            driver.webDriver.Navigate().GoToUrl("https://qa-sandbox.apps.htec.rs/login");
        }

        [OneTimeTearDown]
        public void CloseBrowser()
        {
            driver.webDriver.Quit();
        }

        public void EmptyForm()
        {
            var loginInput = driver.GetElement(By.CssSelector("input[name='email']"));
            loginInput.Clear();
            var passwordInput = driver.GetElement(By.CssSelector("input[name='password']"));
            passwordInput.Clear();
        }

        [Test]
        [Order(1)]
        public void FormValidation()
        {
            var loginInputExist = driver.IsElementPresent(By.CssSelector("input[name='email']"));
            Assert.IsTrue(loginInputExist, "There is no login input field.");

            var passwordInputExist = driver.IsElementPresent(By.CssSelector("input[name='password']"));
            Assert.IsTrue(passwordInputExist, "There is no login input field.");

            var submitButton = driver.IsElementPresent(By.CssSelector("button[data-testid='submit_btn']"));
            Assert.IsTrue(submitButton, "There is no login input field.");
        }

        [Test]
        [Order(2)]
        public void EmailAddressFieldRequired()
        {
            var loginInput = driver.GetElement(By.CssSelector("input[name='email']"));
            Assert.IsNotNull(loginInput, "There is no login input field.");

            if (loginInput == null)
                return;

            loginInput.Clear();

            var passwordInput = driver.GetElement(By.CssSelector("input[name='password']"));
            Assert.IsNotNull(passwordInput, "There is no login input field.");

            if (passwordInput == null)
                return;
            passwordInput.SendKeys("");

            var submitButton = driver.GetElement(By.CssSelector("button[data-testid='submit_btn']"));
            Assert.IsNotNull(submitButton, "There is no login input field.");

            if (submitButton == null)
                return;
            submitButton.Click();

            var warningDOM = driver.GetElement(By.ClassName("invalid-feedback"));
            Assert.AreEqual(warningDOM.Text, "Email field is required");
        }

        [Test]
        [Order(3)]
        public void PaaswrodFieldRequired()
        {
            var loginInput = driver.GetElement(By.CssSelector("input[name='email']"));
            Assert.IsNotNull(loginInput, "There is no login input field.");

            if (loginInput == null)
                return;

            loginInput.SendKeys("test@test.test");

            var passwordInput = driver.GetElement(By.CssSelector("input[name='password']"));
            Assert.IsNotNull(passwordInput, "There is no login input field.");

            if (passwordInput == null)
                return;

            passwordInput.Clear();

            var submitButton = driver.GetElement(By.CssSelector("button[data-testid='submit_btn']"));
            Assert.IsNotNull(submitButton, "There is no login input field.");

            if (submitButton == null)
                return;

            submitButton.Click();

            var warningDOM = driver.GetElement(By.ClassName("invalid-feedback"));
            Assert.AreEqual(warningDOM.Text, "Password is required");
        }

        [Test]
        [Order(4)]
        public void PasswordLenghtValidation()
        {
            var loginInput = driver.GetElement(By.CssSelector("input[name='email']"));
            Assert.IsNotNull(loginInput, "There is no login input field.");

            if (loginInput == null)
                return;

            loginInput.SendKeys("test@test.test");

            var passwordInput = driver.GetElement(By.CssSelector("input[name='password']"));
            Assert.IsNotNull(passwordInput, "There is no login input field.");

            if (passwordInput == null)
                return;

            passwordInput.SendKeys("test");

            var submitButton = driver.GetElement(By.CssSelector("button[data-testid='submit_btn']"));
            Assert.IsNotNull(submitButton, "There is no login input field.");

            if (submitButton == null)
                return;
            submitButton.Click();

            var warningDOM = driver.GetElement(By.ClassName("invalid-feedback"));
            Assert.AreEqual(warningDOM.Text, "Password must be at least 6 characters long");
        }

        [Test]
        [Order(5)]
        public void UserNotFound()
        {
            var loginInput = driver.GetElement(By.CssSelector("input[name='email']"));
            Assert.IsNotNull(loginInput, "There is no login input field.");

            if (loginInput == null)
                return;

            loginInput.SendKeys("test@test.test");

            var passwordInput = driver.GetElement(By.CssSelector("input[name='password']"));
            Assert.IsNotNull(passwordInput, "There is no login input field.");

            if (passwordInput == null)
                return;

            passwordInput.SendKeys("testttt");

            var submitButton = driver.GetElement(By.CssSelector("button[data-testid='submit_btn']"));
            Assert.IsNotNull(submitButton, "There is no login input field.");

            if (submitButton == null)
                return;
            submitButton.Click();

            var warningDOM = driver.GetElement(By.ClassName("invalid-feedback"));
            Assert.AreEqual(warningDOM.Text, "User not found");
        }

        [Test]
        [Order(6)]
        public void WrongPasswrod()
        {
            var loginInput = driver.GetElement(By.CssSelector("input[name='email']"));
            Assert.IsNotNull(loginInput, "There is no login input field.");

            if (loginInput == null)
                return;

            loginInput.SendKeys("milanmaximovic@gmail.com");

            var passwordInput = driver.GetElement(By.CssSelector("input[name='password']"));
            Assert.IsNotNull(passwordInput, "There is no login input field.");

            if (passwordInput == null)
                return;

            passwordInput.SendKeys("testttttt");

            var submitButton = driver.GetElement(By.CssSelector("button[data-testid='submit_btn']"));
            Assert.IsNotNull(submitButton, "There is no login input field.");

            if (submitButton == null)
                return;
            submitButton.Click();

            var warningDOM = driver.GetElement(By.ClassName("invalid-feedback"));
            Assert.AreEqual(warningDOM.Text, "Password incorrect");
        }

        [Test]
        [Order(7)]
        public void LoginSuccesefull()
        {
            var loginInput = driver.GetElement(By.CssSelector("input[name='email']"));
            Assert.IsNotNull(loginInput, "There is no login input field.");

            if (loginInput == null)
                return;

            loginInput.SendKeys("milanmaximovic@gmail.com");

            var passwordInput = driver.GetElement(By.CssSelector("input[name='password']"));
            Assert.IsNotNull(passwordInput, "There is no login input field.");
            passwordInput.Clear();

            if (passwordInput == null)
                return;
            passwordInput.SendKeys("abc1234.");

            var submitButton = driver.GetElement(By.CssSelector("button[data-testid='submit_btn']"));
            Assert.IsNotNull(submitButton, "There is no login input field.");

            if (submitButton == null)
                return;

            submitButton.Click();

            var dashboardElement = driver.GetElement(By.ClassName("dashboard"));
            Assert.IsNotNull(dashboardElement, "Login failed");
        }

    }
}