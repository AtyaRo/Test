using Ideals_Test_Project.Builders;
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
        private SearchPage _searchPage;
        private HomePage _homePage;
        private ShoppingCartSummary _shoppingCartSummary;
        private RegisterPage _registerPage;

        public CheckoutTests(string driverSouce) : base(driverSouce)
        {
        }

        [SetUp]
        public void Setup()
        {
            _searchPage = new SearchPage(driver);
            _homePage = new HomePage(driver);
            _shoppingCartSummary = new ShoppingCartSummary(driver);
            _registerPage = new RegisterPage(driver);
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                ReporterHelper.Log(AventStack.ExtentReports.Status.Error,
                    TestContext.CurrentContext.Result.Message);

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
        public void CheckBuyingItemsThroughSearchByNotRegisteredCustomer()
        {
            PerformSearch(_searchPage);
            var item = AddItemToTheCartFromSearch();
            ProceedToCheckout(item);
            CreateAccount();

            ConfirmAddress();
            ConfirmShipping();

            CheckItemsOnPaymentPage(item);
            PerformPayment();

            CheckOrderConfirmationPage();
        }

        [Test]
        public void CheckBuyingItemsThroughUrlByNotRegisteredCustomer()
        {
            OpenHomePage();
            var item = AddItemToTheCartFromHomePage();
            NavigateToCartThroughUrl();
            CheckItemsInTheCart(item);
            
            ProceedToCheckout();
            CreateAccount();
            ConfirmAddress();
            ConfirmShipping();

            CheckItemsOnPaymentPage(item);
            PerformPayment();

            CheckOrderConfirmationPage();
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
            var searchText = _homePage.SelectRandomItemTextToSearch();
            ReporterHelper.Log(AventStack.ExtentReports.Status.Info, $"Random item is selected: {searchText}");

            searchPage.PerformSearch(searchText);
        }

        private string AddItemToTheCartFromSearch()
        {
            return _searchPage.AddFirstFoundItemToCart();
        }

        private void ProceedToCheckout(string item)
        {
            _searchPage.ProceedToCheckout();
            _shoppingCartSummary.WaitForSummaryElementsLoaded();
            Assert.AreEqual(_shoppingCartSummary._orderedItem.Text, item,
                "Item added to the cart and the one shown on Cart summary page are not the same");

            _shoppingCartSummary.ProceedToCheckout();
        }

        private void CreateAccount()
        {
            var customerInfo = new CustomerInfoBuilder().CustomerFixture;
            _registerPage.EnterEmail(customerInfo.EmailAddress);
            _registerPage.OpenCreateAccountForm();
            _registerPage.FillInRegistrationForm(customerInfo);
        }

        private void ConfirmAddress()
        {
            _shoppingCartSummary.ConfirmShippingAddress();
        }

        private void ConfirmShipping()
        {
            _shoppingCartSummary.ConfirmProcessCarrier();
        }

        private void CheckItemsOnPaymentPage(string item)
        {
            _shoppingCartSummary.WaitForItemsLoadedOnPaymentScreen();
            Assert.AreEqual(_shoppingCartSummary._itemNameOnPaymentScreen.Text, item,
                "Item added to the cart and the one shown on Cart summary page are not the same");
        }

        private void PerformPayment()
        {
            _shoppingCartSummary.PayByChecque();
            _shoppingCartSummary.ConfirmOrder();
        }

        private void CheckOrderConfirmationPage()
        {
            _shoppingCartSummary.WaitForOrderConfirmationMessage();
            Assert.AreEqual(_shoppingCartSummary._orderConfirmationAlert.Text, "Your order on My Store is complete.",
                "Alert message text is not as expected");
        }

        private string  AddItemToTheCartFromHomePage()
        {
            return _homePage.AddItemToTheCart();
        }

        private void NavigateToCartThroughUrl()
        {
            _shoppingCartSummary.NavigateToCartByUrl();
        }

        private void CheckItemsInTheCart(string item)
        {
            _shoppingCartSummary.WaitForSummaryElementsLoaded();

            Assert.AreEqual(_shoppingCartSummary._orderedItem.Text, item,
                "Item added to the cart and the one shown on Cart summary page are not the same");
        }

        private void ProceedToCheckout()
        {
            _shoppingCartSummary.ProceedToCheckout();
        }
    }
}
