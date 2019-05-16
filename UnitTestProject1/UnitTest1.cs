using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Reflection;
using System.Resources;

namespace zadanie12
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
            driver.FindElement(By.XPath("//*[@id='app-'][2]/a")).Click();
            driver.FindElement(By.XPath("//*[@id='content']/div[1]/a[2]")).Click();
            driver.FindElement(By.Name("name[en]")).SendKeys("juice");
            driver.FindElement(By.Name("code")).SendKeys("777");
            IWebElement check = driver.FindElement(By.Name("product_groups[]"));
            check.GetAttribute("1-2");
            check.Click();
            driver.FindElement(By.Name("quantity")).SendKeys(Keys.Control + "a");
            driver.FindElement(By.Name("quantity")).SendKeys(Keys.Backspace);
            driver.FindElement(By.Name("quantity")).SendKeys("1");

            IWebElement element = driver.FindElement(By.Name("new_images[]"));
            FileInfo fileInfo = new FileInfo(Assembly.GetExecutingAssembly().Location);
            string dir = Path.Combine(fileInfo.DirectoryName, @"images\juice.jpg");

            element.SendKeys(dir);

            driver.FindElement(By.Name("date_valid_from")).SendKeys("12122012");
            driver.FindElement(By.Name("date_valid_to")).SendKeys("12122019");

            driver.FindElement(By.LinkText("Information")).Click();
            SelectElement objSelect = new SelectElement(driver.FindElement(By.Name("manufacturer_id")));
            objSelect.SelectByText("ACME Corp.");
            driver.FindElement(By.Name("keywords")).SendKeys("12122019");
            driver.FindElement(By.Name("short_description[en]")).SendKeys("abc");
            driver.FindElement(By.Name("description[en]")).SendKeys("abc");
            driver.FindElement(By.Name("head_title[en]")).SendKeys("abc");
            driver.FindElement(By.Name("meta_description[en]")).SendKeys("abc");
            driver.FindElement(By.LinkText("Prices")).Click();
            driver.FindElement(By.Name("purchase_price")).SendKeys(Keys.Control + "a");
            driver.FindElement(By.Name("purchase_price")).SendKeys(Keys.Backspace);
            driver.FindElement(By.Name("purchase_price")).SendKeys("1");
            SelectElement objSelect1 = new SelectElement(driver.FindElement(By.Name("purchase_price_currency_code")));
            objSelect1.SelectByText("Euros");
            driver.FindElement(By.Name("prices[USD]")).SendKeys("0.5");
            driver.FindElement(By.Name("save")).Click();
            if (!AreElementsPresent(driver, By.LinkText("juice")))
            {
                Assert.Fail("Error message");
            }
        }
        public bool AreElementsPresent(IWebDriver driver, By locator)
        {
            return driver.FindElements(locator).Count > 0;
        }



        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }

    }
}