using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;
using System.Collections.Generic;

namespace zadanie9_2_
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
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
        }

        [Test]
        public void FirstTest()
        {
    
            driver.Url = "http://localhost/litecart/admin/?app=geo_zones&doc=geo_zones";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
      
            IWebElement menu = driver.FindElement(By.ClassName("dataTable"));
            IList<IWebElement> menuPoints = menu.FindElements(By.ClassName("row"));

            int count = menuPoints.Count;

            for (int i = 0; i < count ; i++)
            {
                IWebElement menuElementt = driver.FindElements(By.ClassName("row"))[i];

       
                IWebElement zona = menuElementt.FindElement(By.XPath("//*[@id='content']/form/table/tbody/tr[" + (i + 2) + "]/ td[3]/a"));
                if (zona != null)
                {
                    zona.Click();
                    
                    IWebElement menu_Can = driver.FindElement(By.ClassName("dataTable"));
                    IList<IWebElement> menuElement = menu_Can.FindElements(By.TagName("tr"));

                    int count1 = menuElement.Count;

                    for (int j = 1; j < count1 - 2; j++)
                    {
                        IWebElement menuElement_city = menu_Can.FindElements(By.TagName("tr"))[j];
                        IWebElement submenu_city = menuElement_city.FindElement(By.XPath(".//td[3]"));
                        IWebElement ss = submenu_city.FindElement(By.CssSelector("option[selected='selected']"));
                        IWebElement menuElement_city1 = menu_Can.FindElements(By.TagName("tr"))[j +1];
                        IWebElement submenu_city1 = menuElement_city1.FindElement(By.XPath(".//td[3]"));
                        IWebElement ss1 = submenu_city1.FindElement(By.CssSelector("option[selected='selected']"));
                        if (ss.Text.CompareTo(ss1.Text) > 0)
                        {
                            Assert.Fail("error");
                        }


                    }
                }
                driver.Navigate().Back();
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
