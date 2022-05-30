using Ideals_Test_Project.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System.Reflection;

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

        protected void BaseTearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                ReporterHelper.Log(AventStack.ExtentReports.Status.Error, TestContext.CurrentContext.Result.Message);

                Screenshot screenshot = (Driver as ITakesScreenshot).GetScreenshot();
                string title = TestContext.CurrentContext.Test.Name;
                string runname = $"{title} {DateTime.Now.ToString("yyyy - MM - dd - HH_mm_ss")}";
                string screenshotfilename = $"{Assembly.GetEntryAssembly().Location}{runname}.jpg";
                screenshot.SaveAsFile(screenshotfilename, ScreenshotImageFormat.Jpeg);

                ReporterHelper.SaveScreenshot(screenshotfilename);
            }

            Driver.Quit();
        }
    }
}
