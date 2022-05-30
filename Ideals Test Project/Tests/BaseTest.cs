using Ideals_Test_Project.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace Ideals_Test_Project.Tests
{
    [TestFixture]
    public class BaseTest
    {
        protected IWebDriver? driver;

        public BaseTest(string driverName)
        {
            switch (driverName)
            {
                case "Chrome":
                    driver = new ChromeDriver();
                    break;
                case "Firefox":
                    driver = new FirefoxDriver();
                    break;
                default:
                    driver = new ChromeDriver();
                    break;
            }

            driver.Manage().Window.Maximize();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            ReporterHelper.CloseReporter();
        }
    }
}
