﻿using Ideals_Test_Project.Helpers;
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
        private SearchPage _searchPage;
        private HomePage _homePage;

        public CheckoutTests(string driverSouce) : base(driverSouce)
        {
        }

        [SetUp]
        public void Setup()
        {
            _searchPage = new SearchPage(driver);
            _homePage = new HomePage(driver);
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
            PerformSearch(_searchPage);
            AddItemToTheCart(_searchPage);
            Login();
            CheckItemsInCart();
        }

        [Test]
        public void CheckHomePageMainElements()
        {
            OpenHomePage();
            CheckHeaderElements();
            CheckMainBlockElements();
            CheckFooterElements();
        }

        private void OpenHomePage()
        {
            _homePage.OpenHomePage();
        }

        private void CheckHeaderElements()
        {
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(_homePage.ContactUs, "Contact us element is not present on the page");
                Assert.IsNotNull(_homePage.HeaderBanner, "HeaderBannerelement is not present on the page");
                Assert.IsNotNull(_homePage.SignIn, "SignIn element is not present on the page");
                Assert.IsNotNull(_homePage.CategoriesBlock, "CategoriesBlock us element is not present on the page");
                Assert.IsNotNull(_homePage.SearchField, "Search element is not present on the page");
                Assert.IsNotNull(_homePage.AddToCartBtn, "Add to cart element is not present on the page");
            });
        }

        private void CheckMainBlockElements()
        {
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(_homePage.SaleBlock, "SAle block element is not present on the page");
                Assert.IsNotNull(_homePage.Popular, "Popular element is not present on the page");
                Assert.IsNotNull(_homePage.BestSellers, "BestSellers element is not present on the page");
                Assert.IsNotEmpty(_homePage.FeaturedHomePageItems, "Featured items are not present on the page");
            });
        }

        private void CheckFooterElements()
        {
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(_homePage.SocialBlock, "Social block element is not present on the page");
                Assert.IsNotNull(_homePage.Newsletter, "Newsletter element is not present on the page");
                Assert.IsNotNull(_homePage.StoreInfo, "StoreInfo element is not present on the page");
            });
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
