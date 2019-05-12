using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;
using System.Collections.Generic;
using System.IO;

namespace UnitTestProject1
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
            driver.Url = "http://localhost/litecart";
            IWebElement requiredElement;
            for (int i = 0; i < 3; i++)
            {
                IWebElement itemCount = driver.FindElement(By.CssSelector("div#cart a.content span.formatted_value"));
                driver.FindElement(By.XPath("//*[@id='box-most-popular']/div/ul/li[1]/a[1]")).Click();
                try
                {
                    requiredElement = driver.FindElement(By.CssSelector(".information select:required"));
                }
                catch (NoSuchElementException)
                {
                    requiredElement = null;
                }

                if (requiredElement != null)
                {
                    SelectElement objSelect = new SelectElement(requiredElement);
                    objSelect.SelectByText(objSelect.Options[1].Text);
                }

                driver.FindElement(By.Name("add_cart_product")).Click();

                wait.Until(ExpectedConditions.StalenessOf(itemCount));
                wait.Until(ExpectedConditions.ElementExists(By.CssSelector("div#cart a.content span.formatted_value")));

                driver.Navigate().Back();
            }

            driver.FindElement(By.CssSelector("div#cart a.link")).Click();

            for (int i = 0; i < 3; i++)
            {
                IList<IWebElement> trList = driver.FindElements(By.CssSelector(".dataTable tr"));
                driver.FindElement(By.Name("remove_cart_item")).Click();
                wait.Until(ExpectedConditions.StalenessOf(trList[0]));
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
