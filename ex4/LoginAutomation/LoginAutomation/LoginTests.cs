using CsvHelper;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Xunit;

namespace LoginAutomation
{
    public class LoginTests
    {
        public static IEnumerable<object[]> GetTestData()
        {
            using (var reader = new StreamReader("LoginTestData.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                foreach (var record in csv.GetRecords<TestData>())
                {
                    yield return new object[] { record.Username, record.Password };
                }
            }
        }

        [Theory]
        [MemberData(nameof(GetTestData))]
        public void Login_WithValidCredentials_ShouldSucceed(string username, string password)
        {
            // Initialize the EdgeDriver 
            using (IWebDriver driver = new EdgeDriver())
            {
                // Navigate to the login page 
                driver.Navigate().GoToUrl("C:\\Users\\mnmnm\\OneDrive\\Desktop\\c#\\Lab09\\ex3\\LoginAutomation\\login.html");

                // Find the username and password fields and fill them 
                driver.FindElement(By.Id("username")).SendKeys(username);
                driver.FindElement(By.Id("password")).SendKeys(password);

                // Click the login button 
                driver.FindElement(By.Id("loginButton")).Click();
                // Add assertions as needed, e.g., check for a successful login message 
                // For this example, we'll just wait for a few seconds to simulate the login process
                System.Threading.Thread.Sleep(2000);
            }
        }

        public class TestData
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}