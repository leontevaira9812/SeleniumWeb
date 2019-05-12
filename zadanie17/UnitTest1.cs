using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;
using System.Collections.Generic;
using System.IO;

namespace zadanie17
{

    [TestFixture]
    public class MyFirstTest
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void FirstTest()
        {
            driver.Url = "http://localhost/litecart/admin/?app=catalog&doc=catalog&category_id=1";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();

            driver.FindElement(By.LinkText("Rubber Ducks")).Click();
            driver.FindElement(By.LinkText("Subcategory")).Click();
            IList<IWebElement> productLinks = driver.FindElements(By.CssSelector(".dataTable td img+a"));
            for (int i = 0; i < productLinks.Count; i++)
            {
                IList<IWebElement> productLinksNew = driver.FindElements(By.CssSelector(".dataTable td img+a"));
                productLinksNew[i].Click();
                IList<LogEntry> browserLogs = driver.Manage().Logs.GetLog("browser");
                Assert.IsTrue(browserLogs.Count == 0);
                driver.Navigate().Back();
            }
        }
        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
