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
using System.Globalization;

namespace zadanie10
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
            //   IWebElement el = driver.FindElement((By.Id
            IWebElement box = driver.FindElement(By.Id("box-campaigns"));
            IWebElement el = box.FindElement(By.ClassName("name"));
            IWebElement el1 = box.FindElement(By.ClassName("regular-price"));
            // обычная цена перечеркнутая
            Assert.IsTrue(el1.TagName == "s");
            String titleColor_el1 = el1.GetCssValue("color");
            Color regularColor = ColorHelper.ParseColor(titleColor_el1);
            //обычная цена серая
            Assert.IsTrue((regularColor.R == regularColor.G) && (regularColor.B == regularColor.R));

            //Акционная цена
            IWebElement el2 = box.FindElement(By.ClassName("campaign-price"));
            String titleColor_el2 = el2.GetCssValue("color");
            Color campaignColor = ColorHelper.ParseColor(titleColor_el2);
            //акционная цена жирная
            Assert.IsTrue(el2.TagName == "strong");
            //акционная цена красная
            Assert.IsTrue((campaignColor.G == 0) && (campaignColor.B == 0));
            //акционная цена крупнее обычной
            Assert.IsTrue(el2.GetCssValue("font-size").CompareTo(el1.GetCssValue("font-size")) == 1);
            string txt = el.Text;
            string txt1 = el1.Text;
            string txt2 = el2.Text;

            driver.FindElement(By.XPath("//*[@id='box-campaigns']/div/ul/li/a[1]")).Click();
            IWebElement h = driver.FindElement(By.TagName("h1"));
            IWebElement price = driver.FindElement(By.ClassName("regular-price"));
            Assert.IsTrue(price.TagName == "s");
            String titleColor_price = price.GetCssValue("color");
            Color priceRegularColor = ColorHelper.ParseColor(titleColor_price);
            Assert.IsTrue((priceRegularColor.R == priceRegularColor.G) && (priceRegularColor.B == priceRegularColor.R));

            IWebElement price1 = driver.FindElement(By.ClassName("campaign-price"));
            Assert.IsTrue(price1.TagName == "strong");
            String titleColor_price1 = price1.GetCssValue("color");
            Color priceCampaignColor = ColorHelper.ParseColor(titleColor_price1);
            Assert.IsTrue((priceCampaignColor.G == 0) && (priceCampaignColor.B == 0));

            Assert.IsTrue(price1.GetCssValue("font-size").CompareTo(price.GetCssValue("font-size")) == 1);
            if (txt.CompareTo(h.Text) > 0)
            {

                Assert.Fail("error");
            }
            if (txt1.CompareTo(price.Text) > 0)
            {
                Assert.Fail("error");
            }
            if (txt2.CompareTo(price1.Text) > 0)
            {
                Assert.Fail("error");
            }

        }
        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }

    public static class ColorHelper
    {
        public static Color ParseColor(string cssColor)
        {
            cssColor = cssColor.Trim();

            if (cssColor.StartsWith("#"))
            {
                return ColorTranslator.FromHtml(cssColor);
            }
            else if (cssColor.StartsWith("rgb")) //rgb or argb
            {
                int left = cssColor.IndexOf('(');
                int right = cssColor.IndexOf(')');

                if (left < 0 || right < 0)
                    throw new FormatException("rgba format error");
                string noBrackets = cssColor.Substring(left + 1, right - left - 1);

                string[] parts = noBrackets.Split(',');

                int r = int.Parse(parts[0], CultureInfo.InvariantCulture);
                int g = int.Parse(parts[1], CultureInfo.InvariantCulture);
                int b = int.Parse(parts[2], CultureInfo.InvariantCulture);

                if (parts.Length == 3)
                {
                    return Color.FromArgb(r, g, b);
                }
                else if (parts.Length == 4)
                {
                    float a = float.Parse(parts[3], CultureInfo.InvariantCulture);
                    return Color.FromArgb((int)(a * 255), r, g, b);
                }
            }
            throw new FormatException("Not rgb, rgba or hexa color string");
        }
    }
}
