using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;
using System.Collections.Generic;

namespace zadanie_9
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
           // bool b = true;
            driver.Url = "http://localhost/litecart/admin/?app=countries&doc=countries";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            //wait.Until(ExpectedConditions.UrlContains("http://localhost/litecart/admin/"));

            //  foreach (ChromeDriver item in driver.FindElement(By.TagName("a"))) ;


        
            IList<IWebElement> menuPoints = driver.FindElements(By.ClassName("row"));
     
            int count = menuPoints.Count;
        
            for (int i = 0; i < count-1; i++)
            {
               IWebElement menuElement = driver.FindElements(By.ClassName("row"))[i];
                // IWebElement menuElement = menuPoints[i];
             //  IWebElement zona = menuElement.FindElement(By.LinkText("0")) 
                //IWebElement zona=menuElement.FindElement(By.XPath("//*[@id='content']/form/table/tbody/tr[2]/td[6]"));
                //if (int.Parse(zona.Text) > 0)
                //{

                //}
                IWebElement menuHref = menuElement.FindElement(By.TagName("a"));
                IWebElement zona = menuElement.FindElement(By.XPath("//*[@id='content']/form/table/tbody/tr[" + (i + 2) + "]/ td[6]"));
                if (int.Parse(zona.Text) > 0)
                {
                  //  b = false;
                    menuHref.Click();
                    IWebElement submenuPoints = driver.FindElement(By.ClassName("dataTable"));
                   IList<IWebElement> submenuPoints_tr = submenuPoints.FindElements(By.TagName("tr"));
                    int count_sub = submenuPoints_tr.Count;
                    for (int k = 1; k < count_sub - 2; k++)
                    {
                        IWebElement submenuElement = submenuPoints.FindElements(By.TagName("tr"))[k];
                        IWebElement submenu_city = submenuElement.FindElement(By.XPath(".//td[3]"));
                      
                        IWebElement submenuElement1 = submenuPoints.FindElements(By.TagName("tr"))[k+1];
                        IWebElement submenu_city1 = submenuElement1.FindElement(By.XPath(".//td[3]"));
                        if (submenu_city.Text.CompareTo(submenu_city1.Text) > 0)
                        {
                            Assert.Fail("error");
                        }
                    

                    }
                    driver.Navigate().Back();
                   // IWebElement menuElement = driver.FindElements(By.ClassName("row"))[i];
                    // driver.Url = "http://localhost/litecart/admin/?app=countries&doc=countries";
                }

                //if (!b)
                //{
                   
                //    IWebElement menuElement = driver.FindElements(By.ClassName("row"))[i];
                //}

                menuElement = driver.FindElements(By.ClassName("row"))[i];
                menuHref = menuElement.FindElement(By.TagName("a"));
               

                IWebElement menuElement1 = driver.FindElements(By.ClassName("row"))[i+1];
                IWebElement menuHref1 = menuElement1.FindElement(By.TagName("a"));
                if (menuHref.Text.CompareTo(menuHref1.Text) > 0)
                {
                    Assert.Fail("error");
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