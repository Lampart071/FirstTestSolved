using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Threading;

namespace FirstTestSolved
{
    [TestFixture]
    class SandboxTests
    {
        IWebDriver driver;
        int maxvalue = 11;

        [SetUp]
        public void Login()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--no-sandbox");
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

        
        //TEST NOT FIXED YET
        [Ignore]
        [Test, Category("In Development")]
        public void SliderMoveThreeTimes()
        {
            driver.Navigate().GoToUrl("http://way2automation.com/way2auto_jquery/slider.php");
            driver.SwitchTo().Frame(driver.FindElement(By.CssSelector(".demo-frame")));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            Assert.AreEqual( 2, js.ExecuteScript("return $('#slider-range-max').slider('value');"));
            SetSliderPercentage("#slider-range-max", ".ui-slider-handle", 0);
            Assert.AreEqual( 0, js.ExecuteScript("return $('#slider-range-max').slider('value');"));
            SetSliderPercentage("#slider-range-max", ".ui-slider-handle", 3);
            Assert.AreEqual( 3, js.ExecuteScript("return $('#slider-range-max').slider('value');"));
            SetSliderPercentage("#slider-range-max", ".ui-slider-handle", 5);
            Assert.AreEqual( 5, js.ExecuteScript("return $('#slider-range-max').slider('value');"));
            SetSliderPercentage("#slider-range-max", ".ui-slider-handle", 8);
            Assert.AreEqual( 8, js.ExecuteScript("return $('#slider-range-max').slider('value');"));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }

        public void SetSliderLeftWithArrows(string sliderHandleCss)
        {
            var sliderHandle = driver.FindElement(By.CssSelector(sliderHandleCss));
            sliderHandle.Click();
            sliderHandle.SendKeys(Keys.ArrowLeft);
        }

        public void SetSliderRightWithArrows(string sliderHandleCss)
        {
            var sliderHandle = driver.FindElement(By.CssSelector(sliderHandleCss));
            sliderHandle.Click();
            sliderHandle.SendKeys(Keys.ArrowRight);
        }

        public void SetSlider(int value)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("$('#slider-range-max').slider('value', " + value + "); $('#amount').val($('#slider-range-max').slider('value'));");
        }

        public void SetSliderPercentage(string sliderTrackCss, string sliderHandleCss, int percentage)
        {
            var sliderTrack = driver.FindElement(By.CssSelector(sliderTrackCss));
            var sliderHandle = driver.FindElement(By.CssSelector(sliderHandleCss));
            var width = int.Parse(sliderTrack.GetCssValue("width").Replace("px", ""));
            var dx = (int)(percentage * width / 100.0);
            new Actions(driver)
                        .DragAndDropToOffset(sliderHandle, dx + width/10, 0)
                        .Build()
                        .Perform();
        }

        public void SetSlider(string sliderTrackCss, string sliderHandleCss, int chosennumber)
        {
            var sliderTrack = driver.FindElement(By.CssSelector(sliderTrackCss));
            var sliderHandle = driver.FindElement(By.CssSelector(sliderHandleCss));
            var width = int.Parse(sliderTrack.GetCssValue("width").Replace("px", ""));
            var dx = (int)(width / (maxvalue - chosennumber));

            new Actions(driver)
                        .DragAndDropToOffset(sliderHandle, dx, 0)
                        .Build()
                        .Perform();
        }



        //public void JSExecutor(string script)
        //{
        //    IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
        //    js.ExecuteScript(script);
        //}
    }
}
