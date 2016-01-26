using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Threading;

namespace FirstTestSolved
{
    [TestFixture]
    class SliderPageTests
    {
        IWebDriver driver;

        [SetUp]
        public void Login()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--no-sandbox");
            //options.AddArgument("--incognito");
            options.AddArgument("--disable-extensions");
            options.AddArgument("--start-maximized");
            driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl("http://www.qa.way2automation.com");
            driver.FindElement(By.CssSelector("#load_form > h3"));
            driver.FindElement(By.CssSelector("#load_form > div > div.span_3_of_4 > p > a[href='#login']")).Click();
            driver.FindElement(By.CssSelector("#load_form > fieldset:nth-child(5) > input[name='username']")).SendKeys("j2bwebdriver");
            driver.FindElement(By.CssSelector("#load_form > fieldset:nth-child(6) > input[name='password']")).SendKeys("j2bwebdriver");
            driver.FindElements(By.CssSelector("#load_form > div > div.span_1_of_4 > input"))[1].Submit();
            Thread.Sleep(1000);
        }
        
        [Test]
        public void SliderPageOpens()
        {
            driver.Navigate().GoToUrl("http://way2automation.com/way2auto_jquery/slider.php");
            Assert.IsTrue(driver.FindElement(By.CssSelector(".heading")).Text.Contains("Slider"));
            Assert.IsTrue(driver.FindElement(By.CssSelector(".active>a[target='_self']")).Text.Contains("RANGE SLIDER"));
        }

        //TEST NOT FIXED YET
        //[Test]
        //public void SliderMoveThreeTimes()
        //{
        //    driver.Navigate().GoToUrl("http://way2automation.com/way2auto_jquery/slider.php");
        //    driver.SwitchTo().Frame(driver.FindElement(By.CssSelector(".demo-frame")));
        //    SetSliderPercentage("//*[@id='slider-range-max']", "//*[@id='slider-range-max']/span", 0);
        //    Thread.Sleep(1000);
        //    //Assert.IsTrue(driver.FindElement(By.CssSelector(".heading")).Text.Contains("Slider"));
        //    SetSliderPercentage("//*[@id='slider-range-max']", "//*[@id='slider-range-max']/span", 3);
        //    Thread.Sleep(1000);
        //    //Assert.IsTrue(driver.FindElement(By.CssSelector(".heading")).Text.Contains("Slider"));
        //    SetSliderPercentage("//*[@id='slider-range-max']", "//*[@id='slider-range-max']/span", 5);
        //    Thread.Sleep(1000);
        //    //Assert.IsTrue(driver.FindElement(By.CssSelector(".heading")).Text.Contains("Slider"));
        //    SetSliderPercentage("//*[@id='slider-range-max']", "//*[@id='slider-range-max']/span", 8);
        //    Thread.Sleep(1000);
        //    //Assert.IsTrue(driver.FindElement(By.CssSelector(".heading")).Text.Contains("Slider"));
        //}

        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }

        public void SetSliderPercentage(string sliderTrackXpath, string sliderHandleXpath, int percentage)
        {
            var sliderHandle = driver.FindElement(By.XPath(sliderHandleXpath));
            var sliderTrack = driver.FindElement(By.XPath(sliderTrackXpath));
            var width = int.Parse(sliderTrack.GetCssValue("width").Replace("px", ""));
            var dx = (int)((percentage / 100.0) * width);
            new Actions(driver)
                        .DragAndDropToOffset(sliderHandle, dx, 0)
                        .Build()
                        .Perform();
        }
    }
}
