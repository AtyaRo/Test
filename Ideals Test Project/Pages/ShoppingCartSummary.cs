using Ideals_Test_Project.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Ideals_Test_Project.Pages
{
    public class ShoppingCartSummary: BasePage
    {
        public ShoppingCartSummary(IWebDriver driver) : base(driver)
        {

        }
        
        private IWebElement _proceedToCheckout => Driver.FindElement(By.CssSelector("p.cart_navigation a[title='Proceed to checkout']"));
        private IWebElement _confirmAddress => Driver.FindElement(By.CssSelector("button[name='processAddress']"));
        private IWebElement _processCarrierBtn => Driver.FindElement(By.CssSelector("button[name='processCarrier']"));
        private IWebElement _agreeTermsCheckbox => Driver.FindElement(By.CssSelector("#cgv"));
        public IWebElement _orderedItem => Driver.FindElement(By.CssSelector(".cart_description p.product-name a"));
        public IWebElement _itemNameOnPaymentScreen => Driver.FindElement(By.CssSelector("p.product-name a"));
        public IWebElement _payByChequeBtn => Driver.FindElement(By.CssSelector("a.cheque"));
        public IWebElement _orderConfirmationBtn => Driver.FindElement(By.CssSelector("p.cart_navigation button[type='submit']"));
        public IWebElement _orderConfirmationAlert => Driver.FindElement(By.CssSelector("p.alert-success"));
        public IWebElement _cartPageBreadcrumb => Driver.FindElement(By.CssSelector("span.navigation_page"));


        public void WaitForSummaryElementsLoaded()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".cart_description p.product-name a")));

            ReporterHelper.Log(AventStack.ExtentReports.Status.Info,
                $"Cart Summary is loaded");
        }

        public void ProceedToCheckout()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("p.cart_navigation a[title='Proceed to checkout']")));

            _proceedToCheckout.Click();
            ReporterHelper.Log(AventStack.ExtentReports.Status.Info, $"Proceeding to checkout");
        }

        public void ConfirmShippingAddress()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("button[name='processAddress']")));

            _confirmAddress.Click();
            ReporterHelper.Log(AventStack.ExtentReports.Status.Info, $"Shipping address is confirmed");
        }

        public void ConfirmProcessCarrier()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("button[name='processCarrier']")));

            Actions action = new Actions(Driver);
            action.MoveToElement(_agreeTermsCheckbox).Click().Perform();
            ReporterHelper.Log(AventStack.ExtentReports.Status.Info, $"Agreed on Terms and conditions");

            _processCarrierBtn.Click();
            ReporterHelper.Log(AventStack.ExtentReports.Status.Info, $"Processing carrier info");
        }

        public void WaitForItemsLoadedOnPaymentScreen()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("p.product-name a")));

            ReporterHelper.Log(AventStack.ExtentReports.Status.Info, $"Produt item is loaded on Payments screen");
        }

        public void PayByChecque()
        {
            _payByChequeBtn.Click();
            ReporterHelper.Log(AventStack.ExtentReports.Status.Info, $"Selecting Paying by Cheque option");
        }

        public void ConfirmOrder()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("p.cart_navigation button[type='submit']")));

            _orderConfirmationBtn.Click();
            ReporterHelper.Log(AventStack.ExtentReports.Status.Info, $"Confirming the Order");
        }

        public void WaitForOrderConfirmationMessage()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("p.alert-success")));

            ReporterHelper.Log(AventStack.ExtentReports.Status.Info, $"Confirmation message is displayed");
        }

        public void NavigateToCartByUrl()
        {
            Driver.Navigate().GoToUrl(Constants.CartPage);

            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.TextToBePresentInElement(_cartPageBreadcrumb,
                "Your shopping cart"));

            ReporterHelper.Log(AventStack.ExtentReports.Status.Info, $"Cart is opened");
        }

    }
}
