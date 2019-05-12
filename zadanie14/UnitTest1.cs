using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;
using System.Collections.Generic;
using System.IO;

namespace zadanie14
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
            driver.Url = "http://localhost/litecart/admin/";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            driver.FindElement(By.XPath("//*[@id='app-'][3]/a")).Click();
            driver.FindElement(By.LinkText("Afghanistan")).Click();
            IList<IWebElement> activeLinks = driver.FindElements(By.CssSelector("form a[target='_blank'"));
            for (int i = 0; i < activeLinks.Count; i++)
            {
                string mainWindow = driver.CurrentWindowHandle;
                activeLinks[i].Click();
                driver.SwitchTo().Window(driver.WindowHandles[1]);
                driver.Close();
                driver.SwitchTo().Window(mainWindow);
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
