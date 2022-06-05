using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.Reflection;
using Xunit;

namespace UITests
{
    public class LoginUITest
    {
        private readonly IWebDriver driver;
        public LoginUITest() => driver = new ChromeDriver(Environment.CurrentDirectory);

        [Theory]
        [InlineData("A3a4cC", "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void LoginWithCorrectAccount(string user, string password)
        {
            driver.Navigate().GoToUrl("https://localhost:44313/Login");

            driver.FindElement(By.Id("id_textbox")).SendKeys(user);
            driver.FindElement(By.Id("password_textbox")).SendKeys(password);
            //driver.FindElement(By.Id("btnLogin")).Click();
            var message = driver.FindElement(By.Id("msg_label")).Text;

            Assert.Equal("Chutbook", driver.Title);
        }
    }
}