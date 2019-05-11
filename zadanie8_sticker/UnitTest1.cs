using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;
using System.Collections.Generic;

namespace zadanie8_sticker
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
            driver.Url = "http://localhost/litecart/en/";
      
            IList<IWebElement> menuPoints = driver.FindElements(By.ClassName("image-wrapper"));
       

          
            foreach (IWebElement menuPoint in menuPoints) {
         
                if (!AreElementsPresent(menuPoint, By.ClassName("sticker"))) {
                    Assert.Fail("sticker count error");
                }
            }

        }


        public bool AreElementsPresent(IWebElement webElement, By locator)
        {
            return webElement.FindElements(locator).Count == 1;
        }



        [TearDown]
        public void stop()
        {
          driver.Quit();
           driver = null;
        }
    }
}