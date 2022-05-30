using Ideals_Test_Project.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Ideals_Test_Project.Pages
{
    public class SearchPage: BasePage
    {
        public SearchPage(IWebDriver driver) : base(driver)
        {

        }
        private IWebElement _searchField => Driver.FindElement(By.ClassName("search_query"));
        private IWebElement _searchBtn => Driver.FindElement(By.CssSelector("#searchbox > button"));
        private IList<IWebElement> _searchMatchedItemsList => Driver.FindElements(By.CssSelector(".product_list.grid.row div.product-image-container"));
        private IWebElement _searcResultItemName => Driver.FindElement(By.CssSelector(".right-block .product-name"));

        private IWebElement _addToCartBtn => Driver.FindElement(By.CssSelector(".button-container a.button.ajax_add_to_cart_button"));
        private IWebElement _checkoutFirstBtn => Driver.FindElement(By.CssSelector("a[title='Proceed to checkout']"));

        public void PerformSearch(string searchText)
        {
            _searchField.SendKeys(searchText);
            _searchBtn.Click();
            ReporterHelper.Log(AventStack.ExtentReports.Status.Info,
                $"Search for {searchText} is performed");
        }

        public string AddFirstFoundItemToCart()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".product_list.grid.row div.product-image-container")));

            Actions actions = new Actions(Driver);

            var itemName = _searcResultItemName.Text;

            actions.MoveToElement(_searchMatchedItemsList[0]);
            actions.MoveToElement(_addToCartBtn);

            actions.Click().Build().Perform();

            ReporterHelper.Log(AventStack.ExtentReports.Status.Info,
                $"Product item {itemName} is added to the Cart");
            return itemName;
        }

        public void ProceedToCheckout()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("a[title='Proceed to checkout']")));

            _checkoutFirstBtn.Click();
            ReporterHelper.Log(AventStack.ExtentReports.Status.Info,
                $"Proceeding to checkout");
        }
    }
}
