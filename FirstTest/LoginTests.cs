using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace FirstTestSolved
{
    [TestFixture]
    class LoginTests
    {
        [Test]
        public void SuccessfulLogin()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--no-sandbox");
            //options.AddArgument("--incognito");
            //options.AddArgument("--disable-extensions");
            options.AddArgument("--start-maximized");
            IWebDriver driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl("http://www.qa.way2automation.com/");
            driver.FindElement(By.CssSelector("#load_form > h3"));
            driver.FindElement(By.CssSelector("#load_form > div > div.span_3_of_4 > p > a[href='#login']")).Click();
            driver.FindElement(By.CssSelector("#load_form > fieldset:nth-child(5) > input[name='username']")).SendKeys("j2bwebdriver");
            driver.FindElement(By.CssSelector("#load_form > fieldset:nth-child(6) > input[name='password']")).SendKeys("j2bwebdriver");
            driver.FindElements(By.CssSelector("#load_form > div > div.span_1_of_4 > input"))[1].Submit();
            Thread.Sleep(3000);
            Assert.IsFalse(driver.FindElement(By.CssSelector("body")).Text.Contains("Username"));
            Assert.IsFalse(driver.FindElement(By.CssSelector("body")).Text.Contains("Password"));
            driver.Close();
        }
        
        [Test]
        public void FailedLogin()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--no-sandbox");
            //options.AddArgument("--incognito");
            //options.AddArgument("--disable-extensions");
            options.AddArgument("--start-maximized");
            IWebDriver driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl("http://www.qa.way2automation.com/");
            driver.FindElement(By.CssSelector("#load_form > h3"));
            driver.FindElement(By.CssSelector("#load_form > div > div.span_3_of_4 > p > a[href='#login']")).Click();
            driver.FindElement(By.CssSelector("#load_form > fieldset:nth-child(5) > input[name='username']")).SendKeys("j2bwebdriver");
            driver.FindElement(By.CssSelector("#load_form > fieldset:nth-child(6) > input[name='password']")).SendKeys("incorrectpassword");
            driver.FindElements(By.CssSelector("#load_form > div > div.span_1_of_4 > input"))[1].Submit();
            Thread.Sleep(3000); 
            Assert.IsTrue(driver.FindElement(By.CssSelector("#alert1")).Text.Contains("Invalid username password."));
            Assert.IsTrue(driver.FindElement(By.CssSelector("body")).Text.Contains("Username"));
            Assert.IsTrue(driver.FindElement(By.CssSelector("body")).Text.Contains("Password")); 
            driver.Close();
        }
    }
}
