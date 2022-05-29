using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Ideals_Test_Project.Pages
{
    public class SearchPage: BasePage
    {
        private const string _homePage = "http://automationpractice.com/index.php";

        public SearchPage(IWebDriver driver) : base(driver)
        {

        }

        private IList<IWebElement> _featuredHomePageItems => driver.FindElements(By.CssSelector("ul#homefeatured h5 a.product-name"));
        private IWebElement _searchField => driver.FindElement(By.ClassName("search_query"));
        private IWebElement _searchBtn => driver.FindElement(By.CssSelector("#searchbox > button"));
        private IList<IWebElement> _searchMatchedItemsList => driver.FindElements(By.CssSelector(".product_list.grid.row div.product-image-container"));
        private IWebElement _addToCartBtn => driver.FindElement(By.CssSelector(".button-container a.button.ajax_add_to_cart_button"));

        public string SelectRandomItemFromHomePage()
        {
            driver.Navigate().GoToUrl(_homePage);           

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("ul#homefeatured h5 a.product-name")));

            var itemsCount = _featuredHomePageItems.Count();
            var random = new Random();
            var randomItem = random.Next(itemsCount - 1);

            return _featuredHomePageItems[randomItem].Text;
        }

        public void PerformSearch(string searchText)
        {
            _searchField.SendKeys(searchText);
            _searchBtn.Click();
        }

        public void AddFirstFoundItemToCart()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".product_list.grid.row div.product-image-container")));

            //Instantiating Actions class
            Actions actions = new Actions(driver);

            actions.MoveToElement(_searchMatchedItemsList[0]);
            actions.MoveToElement(_addToCartBtn);

            actions.Click().Build().Perform();
        }
    }
}
