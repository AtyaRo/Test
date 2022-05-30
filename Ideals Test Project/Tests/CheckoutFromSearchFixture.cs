using Ideals_Test_Project.Builders;
using Ideals_Test_Project.Helpers;
using Ideals_Test_Project.Pages;
using Ideals_Test_Project.TestSources;

namespace Ideals_Test_Project.Tests
{
    [TestFixture]
    [TestFixtureSource(typeof(DriverSource), nameof(DriverSource.Drivers))]
    public class CheckoutFromSearchFixture : BaseTest
    {
        private SearchPage _searchPage;
        private HomePage _homePage;
        private ShoppingCartSummary _shoppingCartSummary;
        private RegisterPage _registerPage;

        public CheckoutFromSearchFixture(string driverSource) : base(driverSource)
        {
        }

        [SetUp]
        public void Setup()
        {
            _searchPage = new SearchPage(Driver);
            _homePage = new HomePage(Driver);
            _shoppingCartSummary = new ShoppingCartSummary(Driver);
            _registerPage = new RegisterPage(Driver);
        }

        [TearDown]
        public void TearDown()
        {
            BaseTearDown();
        }

        [Test]
        public void CheckBuyingItemsThroughSearchByNotRegisteredCustomer()
        {
            PerformSearch();
            var item = AddItemToTheCartFromSearch();
            ProceedToCheckout(item);
            CreateAccount();

            ConfirmAddress();
            ConfirmShipping();

            CheckItemsOnPaymentPage(item);
            PerformPayment();

            CheckOrderConfirmationPage();
        }

        private void PerformSearch()
        {
            var searchText = _homePage.SelectRandomItemTextToSearch();
            ReporterHelper.Log(AventStack.ExtentReports.Status.Info, $"Random item is selected: {searchText}");

            _searchPage.PerformSearch(searchText);
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
    }
}
