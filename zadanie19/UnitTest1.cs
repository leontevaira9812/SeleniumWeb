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
        [Test]
        public void FirstTest()
        {
            Application app = new Application();
            for (int i = 0; i < 3; i++)
            {
                app.mainPage.RememberBasketValue();
                app.mainPage.GoIntoProductDetail();

                app.productDetailPage.CheckRequiredInput();
                app.productDetailPage.AddProductToCart();

                app.mainPage.WaitingForBasketItemRefresh();
                app.NavigateBack();
            }

            app.mainPage.GoIntoBasketPage();

            for (int i = 0; i < 3; i++)
            {
                app.basketPage.DeleteItemsWithWaitingTableRedrawing();
            }

            app.Quit();
        }

        [TearDown]
        public void stop()
        {
            // driver.Quit();
            // driver = null;
        }
    }

    public class MainPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public IWebElement basketItemCount;

        public MainPage(IWebDriver chromeDriver, WebDriverWait webDriverWait)
        {
            this.driver = chromeDriver;
            this.wait = webDriverWait;
            SetDriverUrl();
        }

        private void SetDriverUrl()
        {
            driver.Url = "http://localhost/litecart";
        }

        public void GoIntoProductDetail()
        {
            driver.FindElement(By.XPath("//*[@id='box-most-popular']/div/ul/li[1]/a[1]")).Click();
        }

        public void RememberBasketValue()
        {
            this.basketItemCount = driver.FindElement(By.CssSelector("div#cart a.content span.formatted_value"));
        }

        public void GoIntoBasketPage()
        {
            driver.FindElement(By.CssSelector("div#cart a.link")).Click();
        }

        public void WaitingForBasketItemRefresh()
        {
            wait.Until(ExpectedConditions.StalenessOf(this.basketItemCount));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("div#cart a.content span.formatted_value")));
        }
    }

    public class ProductDetailPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        public IWebElement requiredElement;

        public ProductDetailPage(IWebDriver chromeDriver, WebDriverWait webDriverWait)
        {
            this.driver = chromeDriver;
            this.wait = webDriverWait;
            this.requiredElement = null;
        }

        public void CheckRequiredInput()
        {
            try
            {
                this.requiredElement = driver.FindElement(By.CssSelector(".information select:required"));
            }
            catch (NoSuchElementException)
            {
                return;
            }

            FillRequiredInput();
        }

        private void FillRequiredInput()
        {
            SelectElement objSelect = new SelectElement(requiredElement);
            objSelect.SelectByText(objSelect.Options[1].Text);
        }

        public void AddProductToCart()
        {
            driver.FindElement(By.Name("add_cart_product")).Click();
        }

    }

    public class BasketPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public BasketPage(IWebDriver chromeDriver, WebDriverWait webDriverWait)
        {
            this.driver = chromeDriver;
            this.wait = webDriverWait;
        }

        public void DeleteItemsWithWaitingTableRedrawing()
        {
            IList<IWebElement> trList = driver.FindElements(By.CssSelector(".dataTable tr"));
            if (trList.Count > 0)
            {
                DeleteOneItemFromBasket();
                WaitingForTableElement(trList[0]);
            }
        }

        private void DeleteOneItemFromBasket()
        {
            driver.FindElement(By.Name("remove_cart_item")).Click();
        }

        private void WaitingForTableElement(IWebElement element)
        {
            wait.Until(ExpectedConditions.StalenessOf(element));
        }

    }

    public class Application
    {
        private static IWebDriver driver;
        private static WebDriverWait wait;

        public MainPage mainPage;
        public ProductDetailPage productDetailPage;
        public BasketPage basketPage;

        public Application()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            InitializePages();
        }

        private void InitializePages()
        {
            this.mainPage = new MainPage(Application.driver, Application.wait);
            this.productDetailPage = new ProductDetailPage(Application.driver, Application.wait);
            this.basketPage = new BasketPage(Application.driver, Application.wait);
        }

        public void NavigateBack()
        {
            driver.Navigate().Back();
        }

        public void Quit()
        {
            driver.Quit();
        }
    }
}
