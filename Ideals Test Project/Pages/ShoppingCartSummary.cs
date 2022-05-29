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
        
        private IWebElement _proceedToCheckout => driver.FindElement(By.CssSelector("p.cart_navigation a[title='Proceed to checkout']"));
        private IWebElement _confirmAddress => driver.FindElement(By.CssSelector("button[name='processAddress']"));
        private IWebElement _processCarrierBtn => driver.FindElement(By.CssSelector("button[name='processCarrier']"));
        private IWebElement _agreeTermsCheckbox => driver.FindElement(By.CssSelector("#cgv"));
        public IWebElement _orderedItem => driver.FindElement(By.CssSelector(".cart_description p.product-name a"));
        public IWebElement _itemNameOnPaymentScreen => driver.FindElement(By.CssSelector("p.product-name a"));
        public IWebElement _payByChequeBtn => driver.FindElement(By.CssSelector("a.cheque"));
        public IWebElement _orderConfirmationBtn => driver.FindElement(By.CssSelector("p.cart_navigation button[type='submit']"));
        public IWebElement _orderConfirmationAlert => driver.FindElement(By.CssSelector("p.alert-success"));
        public IWebElement _cartPageBreadcrumb => driver.FindElement(By.CssSelector("span.navigation_page"));


        public void WaitForSummaryElementsLoaded()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".cart_description p.product-name a")));
        }

        public void ProceedToCheckout()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("p.cart_navigation a[title='Proceed to checkout']")));

            _proceedToCheckout.Click();
        }

        public void ConfirmShippingAddress()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("button[name='processAddress']")));

            _confirmAddress.Click();
        }

        public void ConfirmProcessCarrier()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("button[name='processCarrier']")));

            Actions action = new Actions(driver);
            action.MoveToElement(_agreeTermsCheckbox).Click().Perform();

            _processCarrierBtn.Click();
        }

        public void WaitForItemsLoadedOnPaymentScreen()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("p.product-name a")));
        }

        public void PayByChecque()
        {
            _payByChequeBtn.Click();
        }

        public void ConfirmOrder()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("p.cart_navigation button[type='submit']")));

            _orderConfirmationBtn.Click();
        }

        public void WaitForOrderConfirmationMessage()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("p.alert-success")));
        }

        public void NavigateToCartByUrl()
        {
            driver.Navigate().GoToUrl(Constants.CartPage);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.TextToBePresentInElement(_cartPageBreadcrumb,
                "Your shopping cart"));
        }

    }
}
