using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Threading;

namespace NUnitTestProject1
{
    public class Driver
    {
        public IWebDriver webDriver;
        private const int wait = 1000;

        public Driver()
        {
            webDriver = new ChromeDriver(Environment.CurrentDirectory);
        }

        public bool IsElementPresent(By by)
        {
            try
            {
                Thread.Sleep(wait);
                webDriver.FindElement(by);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public IWebElement GetElement(By by)
        {
            IWebElement iWebEement = null;
            try
            {
                Thread.Sleep(wait);
                iWebEement = webDriver.FindElement(by);
            }
            catch (Exception ex)
            {
            }
            return iWebEement;
        }


        public IEnumerable<IWebElement> GetElements(By by)
        {
            try
            {
                Thread.Sleep(wait);
                return webDriver.FindElements(by);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
