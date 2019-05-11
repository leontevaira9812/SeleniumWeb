using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;
using System.Collections.Generic;

namespace zadanie7
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
            driver.Url = "http://localhost/litecart/admin/login.php";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
      
            IList<IWebElement> menuPoints = driver.FindElements(By.Id("app-"));
            int count = menuPoints.Count;

            for (int i = 0; i < count; i++)
            {

                IWebElement menuElement = driver.FindElements(By.Id("app-"))[i];
                IWebElement menuHref = menuElement.FindElement(By.TagName("a"));
                if (menuHref != null)
                {

                    menuHref.Click();
                    if (!AreElementsPresent(driver, By.TagName("h1")))
                    {
                        Assert.Fail("Error message");
                    }
                    IList<IWebElement> submenu;
                    try
                    {
                        submenu = driver.FindElement(By.ClassName("docs")).FindElements(By.TagName("li"));
                    }
                    catch
                    {
                        continue;
                    }
                    int submenuCount = submenu.Count;

                    for (int j = 0; j < submenuCount; j++)
                    {

                        IWebElement submenuElement = driver.FindElement(By.ClassName("docs")).FindElements(By.TagName("li"))[j];
                        IWebElement submenuHref = submenuElement.FindElement(By.TagName("a"));
                        if (submenuHref != null)
                        {

                            submenuHref.Click();
                            if (!AreElementsPresent(driver, By.TagName("h1")))
                            {
                                Assert.Fail("Error message");
                            }
                        }
                    }
                }

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