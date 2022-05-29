using Ideals_Test_Project.Helpers;
using Ideals_Test_Project.Pages;
using OpenQA.Selenium;
using System.Reflection;

namespace Ideals_Test_Project.Tests
{
    public class DriverSource
    {
        public static IEnumerable<string> Drivers => new string[] { Constants.ChromeBrowserName, Constants.FirefoxBrowserName };
        
    }

    [TestFixture]
    [TestFixtureSource(typeof(DriverSource), nameof(DriverSource.Drivers))]
    public class CheckoutTests : BaseTest
    {
        private SearchPage searchPage;

        public CheckoutTests(string driverSouce) : base(driverSouce)
        {
        }

        [SetUp]
        public void Setup()
        {
            searchPage = new SearchPage(driver);
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                string title = TestContext.CurrentContext.Test.Name;
                string runname = $"{title} {DateTime.Now.ToString("yyyy - MM - dd - HH_mm_ss")}";
                string screenshotfilename = $"{Assembly.GetEntryAssembly().Location}{runname}.jpg";
                screenshot.SaveAsFile(screenshotfilename, ScreenshotImageFormat.Jpeg);


                ReporterHelper.SaveScreenshot(screenshotfilename);
            }

            driver.Quit();            
        }

        [Test]
        public void Scenario1()
        {
            PerformSearch(searchPage);
            AddItemToTheCart(searchPage);
            Login();
            CheckItemsInCart();
        }

        private void PerformSearch(SearchPage searchPage)
        {
            var searchText = searchPage.SelectRandomItemFromHomePage();
            ReporterHelper.Log(AventStack.ExtentReports.Status.Info, $"Random item is selected: {searchText}");

            searchPage.PerformSearch(searchText);
        }

        private void AddItemToTheCart(SearchPage searchPage)
        {
            searchPage.AddFirstFoundItemToCart();
        }

        private void Login()
        {

        }

        private void CheckItemsInCart()
        {

        }

    }
}
