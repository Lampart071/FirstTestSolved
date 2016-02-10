using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Threading;

namespace FirstTestSolved
{
    [TestFixture, Category("Semi-Pro")]
    class SliderPageTests
    {
        IWebDriver driver;

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

        [Test, Category("Medium level")]
        public void SliderPageOpens()
        {
            driver.Navigate().GoToUrl("http://way2automation.com/way2auto_jquery/slider.php");
            Assert.IsTrue(driver.FindElement(By.CssSelector(".heading")).Text.Contains("Slider"));
            Assert.IsTrue(driver.FindElement(By.CssSelector(".active>a[target='_self']")).Text.Contains("RANGE SLIDER"));
        }

        [Test]
        public void SliderMoveByJavaScript()
        {
            driver.Navigate().GoToUrl("http://way2automation.com/way2auto_jquery/slider.php");
            driver.SwitchTo().Frame(driver.FindElement(By.CssSelector(".demo-frame")));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            Assert.AreEqual(2, js.ExecuteScript("return $('#slider-range-max').slider('value');"));
            SetSlider(5);
            Assert.AreEqual(5, js.ExecuteScript("return $('#slider-range-max').slider('value');"));
            SetSlider(8);
            Assert.AreEqual(8, js.ExecuteScript("return $('#slider-range-max').slider('value');"));
            SetSlider(1);
            Assert.AreEqual(1, js.ExecuteScript("return $('#slider-range-max').slider('value');"));
            SetSlider(10);
            Assert.AreEqual(10, js.ExecuteScript("return $('#slider-range-max').slider('value');"));
        }

        [Test]
        public void SliderMoveByArrows()
        {
            driver.Navigate().GoToUrl("http://way2automation.com/way2auto_jquery/slider.php");
            driver.SwitchTo().Frame(driver.FindElement(By.CssSelector(".demo-frame")));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            Assert.AreEqual(2, js.ExecuteScript("return $('#slider-range-max').slider('value');"));
            SetSliderLeftWithArrows(".ui-slider-handle");
            Assert.AreEqual(1, js.ExecuteScript("return $('#slider-range-max').slider('value');"));
            SetSliderRightWithArrows(".ui-slider-handle");
            SetSliderRightWithArrows(".ui-slider-handle");
            SetSliderRightWithArrows(".ui-slider-handle");
            SetSliderRightWithArrows(".ui-slider-handle");
            Assert.AreEqual(5, js.ExecuteScript("return $('#slider-range-max').slider('value');"));
            SetSliderRightWithArrows(".ui-slider-handle");
            SetSliderRightWithArrows(".ui-slider-handle");
            SetSliderRightWithArrows(".ui-slider-handle");
            Assert.AreEqual(8, js.ExecuteScript("return $('#slider-range-max').slider('value');"));
            SetSliderRightWithArrows(".ui-slider-handle");
            SetSliderRightWithArrows(".ui-slider-handle");
            Assert.AreEqual(10, js.ExecuteScript("return $('#slider-range-max').slider('value');"));
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

        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }

    }
}
