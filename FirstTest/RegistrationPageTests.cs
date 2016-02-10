using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using System.Threading;

namespace FirstTestSolved
{
    [TestFixture, Category("Medium level")]
    class RegistrationPageTests
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
        
        [Test]
        public void RegistrationPage_TestThatRegistrationPageOpens()
        {
            driver.Navigate().GoToUrl("http://way2automation.com/way2auto_jquery/registration.php");
            Assert.IsTrue(driver.FindElement(By.CssSelector("#wrapper > div > div > h1")).Text.Contains("Registration"));
            Assert.IsTrue(driver.FindElement(By.CssSelector("#wrapper > div > div > div > h2")).Text.Contains("Registration Form"));
        }

        [Test]
        public void RegistrationPage_TestThatValidationWorksForSelectedElementsWhenPageIsEmptyAndSubmitted()
        {
            driver.Navigate().GoToUrl("http://way2automation.com/way2auto_jquery/registration.php");
            driver.FindElement(By.CssSelector("#register_form input[type='submit']")).Submit();
            Assert.IsTrue(driver.FindElement(By.CssSelector("#register_form > fieldset:nth-child(1) > p:nth-child(1) > label.error_p")).Text.Contains("This field is required."));
            Assert.IsTrue(driver.FindElement(By.CssSelector("#register_form > fieldset.fieldset.padding-bottom.error_p > div > label.relative > label")).Text.Contains("This field is required."));
            Assert.IsTrue(driver.FindElement(By.CssSelector("#register_form > fieldset:nth-child(6) > label.error_p")).Text.Contains("This field is required."));
            Assert.IsTrue(driver.FindElement(By.CssSelector("#register_form > fieldset:nth-child(7) > label.error_p")).Text.Contains("This field is required."));
            Assert.IsTrue(driver.FindElement(By.CssSelector("#register_form > fieldset:nth-child(8) > label.error_p")).Text.Contains("This field is required."));
            Assert.IsTrue(driver.FindElement(By.CssSelector("#register_form > fieldset:nth-child(11) > label.error_p")).Text.Contains("This field is required."));
            Assert.IsTrue(driver.FindElement(By.CssSelector("#register_form > fieldset:nth-child(12) > label.error_p")).Text.Contains("This field is required."));
        }

        [Test]
        public void RegistrationPage_TestThatUserCanFillElementsOnPageAndSubmit()
        {
            driver.Navigate().GoToUrl("http://way2automation.com/way2auto_jquery/registration.php");
            //ADD FIRST NAME AND SURNAME
            driver.FindElement(By.CssSelector("#register_form > fieldset:nth-child(1) > p:nth-child(1) > input[type='text']")).SendKeys("TestFirstName");
            driver.FindElement(By.CssSelector("#register_form > fieldset:nth-child(1) > p:nth-child(2) > input[type='text']")).SendKeys("TestSurname");
            //CHOOSE MARITIAL STATUS (RADIO BUTTON - ONE ANSWER)
            driver.FindElement(By.CssSelector("#register_form > fieldset:nth-child(2) > div > label:nth-child(1) > input[type='radio']")).Click();
            driver.FindElement(By.CssSelector("#register_form > fieldset:nth-child(2) > div > label:nth-child(2) > input[type='radio']")).Click();
            driver.FindElement(By.CssSelector("#register_form > fieldset:nth-child(2) > div > label:nth-child(3) > input[type='radio']")).Click();
            //CHOOSE HOBBY (CHECKBOX - MULTI ANSWER)
            driver.FindElement(By.CssSelector("#register_form > fieldset.fieldset.padding-bottom > div > label:nth-child(1)")).Click();
            driver.FindElement(By.CssSelector("#register_form > fieldset.fieldset.padding-bottom > div > label:nth-child(2)")).Click();
            driver.FindElement(By.CssSelector("#register_form > fieldset.fieldset.padding-bottom > div > label:nth-child(3)")).Click();
            //SELECT COUNTRY (DROP-DOWN LIST)
            driver.FindElement(By.CssSelector("#register_form > fieldset:nth-child(4) > select")).SendKeys("India");
            //SELECT DATE OF BIRTH (DROP-DOWN LIST)
            driver.FindElement(By.CssSelector("#register_form > fieldset:nth-child(5) > div:nth-child(2) > select")).SendKeys("1");
            driver.FindElement(By.CssSelector("#register_form > fieldset:nth-child(5) > div:nth-child(3) > select")).SendKeys("1");
            driver.FindElement(By.CssSelector("#register_form > fieldset:nth-child(5) > div:nth-child(4) > select")).SendKeys("2014");
            //ADD PHONE NUMBER
            driver.FindElement(By.CssSelector("#register_form > fieldset:nth-child(6) > input")).SendKeys("+48 666888000");
            //ADD USERNAME
            driver.FindElement(By.CssSelector("#register_form > fieldset:nth-child(7) > input")).SendKeys("TestUserName");
            //ADD MAIL
            driver.FindElement(By.CssSelector("#register_form > fieldset:nth-child(8) > input")).SendKeys("j2bwebdriver@selenium.com.pl");
            //ADD PROFILE PICTURE
            driver.FindElement(By.CssSelector("#register_form > fieldset:nth-child(9) > input[type='file']")).SendKeys(Path.GetFullPath(@"image.png"));
            //ADD COMMENT
            driver.FindElement(By.CssSelector("#register_form > fieldset:nth-child(10) > textarea")).SendKeys("Short note about ourselves.");
            //ADD PASSWORD
            driver.FindElement(By.CssSelector("#register_form > fieldset:nth-child(11) > input")).SendKeys("MyPassword");
            //ADD PASSWORD CONFIRMATION
            driver.FindElement(By.CssSelector("#register_form > fieldset:nth-child(12) > input")).SendKeys("MyPassword");
            //SUBMIT
            driver.FindElement(By.CssSelector("#register_form input[type='submit']")).Submit();
            }

        [Test]
        public void RegistrationPage_TestMailValidation()
        {
            driver.Navigate().GoToUrl("http://way2automation.com/way2auto_jquery/registration.php");
            driver.FindElement(By.CssSelector("#register_form input[type='submit']")).Submit();
            Assert.IsTrue(driver.FindElement(By.CssSelector("#register_form > fieldset:nth-child(8) > label.error_p")).Text.Contains("This field is required."));
            driver.FindElement(By.CssSelector("#register_form > fieldset:nth-child(8) > input")).SendKeys("j2bwebdriver");
            Assert.IsTrue(driver.FindElement(By.CssSelector("#register_form > fieldset:nth-child(8) > label.error_p")).Text.Contains("Please enter a valid email address."));
            driver.FindElement(By.CssSelector("#register_form > fieldset:nth-child(8) > input")).SendKeys("@selenium");
            Assert.IsTrue(driver.FindElement(By.CssSelector("#register_form > fieldset:nth-child(8) > label.error_p")).Text.Contains("Please enter a valid email address."));
            driver.FindElement(By.CssSelector("#register_form > fieldset:nth-child(8) > input")).SendKeys(".com");
            Assert.IsFalse(driver.FindElement(By.CssSelector("#register_form > fieldset:nth-child(8) > label.error_p")).Text.Contains("This field is required."));
            Assert.IsFalse(driver.FindElement(By.CssSelector("#register_form > fieldset:nth-child(8) > label.error_p")).Text.Contains("Please enter a valid email address."));
            driver.FindElement(By.CssSelector("#register_form > fieldset:nth-child(8) > input")).SendKeys(".pl");
            Assert.IsFalse(driver.FindElement(By.CssSelector("#register_form > fieldset:nth-child(8) > label.error_p")).Text.Contains("This field is required."));
            Assert.IsFalse(driver.FindElement(By.CssSelector("#register_form > fieldset:nth-child(8) > label.error_p")).Text.Contains("Please enter a valid email address."));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }
    }
}
