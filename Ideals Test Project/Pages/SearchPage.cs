using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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

        private IWebElement _searchField => driver.FindElement(By.ClassName("search_query"));
        private IWebElement _searchBtn => driver.FindElement(By.CssSelector("#searchbox > button"));
        private IList<IWebElement> _searchMatchedItemsList => driver.FindElements(By.CssSelector(".product_list.grid.row div.product-image-container"));
        private IWebElement _searcResultItemName => driver.FindElement(By.CssSelector(".right-block .product-name"));

        private IWebElement _addToCartBtn => driver.FindElement(By.CssSelector(".button-container a.button.ajax_add_to_cart_button"));
        private IWebElement _checkoutFirstBtn => driver.FindElement(By.CssSelector("a[title='Proceed to checkout']"));

        public void PerformSearch(string searchText)
        {
            _searchField.SendKeys(searchText);
            _searchBtn.Click();
        }

        public string AddFirstFoundItemToCart()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".product_list.grid.row div.product-image-container")));

            Actions actions = new Actions(driver);

            var itemName = _searcResultItemName.Text;

            actions.MoveToElement(_searchMatchedItemsList[0]);
            actions.MoveToElement(_addToCartBtn);

            actions.Click().Build().Perform();

            return itemName;
        }

        public void ProceedToCheckout()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("a[title='Proceed to checkout']")));

            _checkoutFirstBtn.Click();
        }
    }
}
