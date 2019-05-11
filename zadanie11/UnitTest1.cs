using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;
using System.Collections.Generic;

namespace zadanie11
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
            driver.FindElement(By.LinkText("New customers click here")).Click();
            driver.FindElement(By.Name("firstname")).SendKeys("Ira");
            driver.FindElement(By.Name("lastname")).SendKeys("leo");
            driver.FindElement(By.Name("address1")).SendKeys("kzn");
            driver.FindElement(By.Name("postcode")).SendKeys("12345");
            driver.FindElement(By.Name("city")).SendKeys("abc");
           
            SelectElement objSelect = new SelectElement(driver.FindElement(By.Name("country_code")));
            driver.FindElement(By.Name("email")).SendKeys("leoonttevaira95@gmail.com"); 
            objSelect.SelectByText("United States");
            driver.FindElement(By.Name("phone")).SendKeys("+12345678900");
            driver.FindElement(By.Name("password")).SendKeys("1234");
            driver.FindElement(By.Name("confirmed_password")).SendKeys("1234");
        
            driver.FindElement(By.Name("create_account")).Click();
            driver.FindElement(By.LinkText("Logout")).Click();
            driver.FindElement(By.Name("email")).SendKeys("leoonttevaira95@gmail.com");
            driver.FindElement(By.Name("password")).SendKeys("1234");
            driver.FindElement(By.Name("login")).Click();
            driver.FindElement(By.LinkText("Logout")).Click();
        }
    [TearDown]
    public void stop()
    {
        driver.Quit();
       driver = null;
    }
}
}