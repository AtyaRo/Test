using Ideals_Test_Project.Builders;
using Ideals_Test_Project.Pages;
using Ideals_Test_Project.TestSources;

namespace Ideals_Test_Project.Tests
{
    [TestFixture]
    [TestFixtureSource(typeof(DriverSource), nameof(DriverSource.Drivers))]
    public class CheckoutThroughUrlFixture : BaseTest
    {
        private HomePage _homePage;
        private ShoppingCartSummary _shoppingCartSummary;
        private RegisterPage _registerPage;

        public CheckoutThroughUrlFixture(string driverSource) : base(driverSource)
        {
        }

        [SetUp]
        public void Setup()
        {
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

        private void OpenHomePage()
        {
            _homePage.OpenHomePage();
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

        private string AddItemToTheCartFromHomePage()
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
