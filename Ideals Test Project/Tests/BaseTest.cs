using Ideals_Test_Project.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace Ideals_Test_Project.Tests
{
    [TestFixture]
    public class BaseTest
    {
        protected IWebDriver? Driver;

        public BaseTest(string driverName)
        {
            switch (driverName)
            {
                case "Chrome":
                    Driver = new ChromeDriver();
                    break;
                case "Firefox":
                    Driver = new FirefoxDriver();
                    break;
                default:
                    Driver = new ChromeDriver();
                    break;
            }

            Driver.Manage().Window.Maximize();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            ReporterHelper.CloseReporter();
        }
    }
}
