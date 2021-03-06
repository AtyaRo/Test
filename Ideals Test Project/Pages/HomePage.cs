using Ideals_Test_Project.Extensions;
using Ideals_Test_Project.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Ideals_Test_Project.Pages
{
    public class HomePage : BasePage
    {
        public HomePage(IWebDriver driver) : base(driver)
        {
        }

        private IList<IWebElement> _featuredItems => Driver.FindElements(By.CssSelector("#homefeatured a.product_img_link"));
        private IList<IWebElement> _featuredHomePageItemNames => Driver.FindElements(By.CssSelector("ul#homefeatured h5 a.product-name"));
        private IWebElement _addToCartBtn => Driver.FindElement(By.CssSelector(".button-container a.button.ajax_add_to_cart_button"));

        //Header
        public IWebElement HeaderBanner => Driver.TryGetWebElement(By.CssSelector("#header div.banner a img"));
        public IWebElement ContactUs => Driver.TryGetWebElement(By.CssSelector("div#contact-link a"));
        public IWebElement SignIn => Driver.TryGetWebElement(By.CssSelector("a.login"));
        public IWebElement CategoriesBlock => Driver.TryGetWebElement(By.CssSelector("ul.sf-menu.menu-content.sf-js-enabled"));
        public IWebElement SearchField => Driver.TryGetWebElement(By.ClassName("search_query"));
        public IWebElement AddToCartBtn => Driver.TryGetWebElement(By.CssSelector(".button-container a.button.ajax_add_to_cart_button"));

        //Main block
        public IWebElement SaleBlock => Driver.TryGetWebElement(By.CssSelector("#homeslider a img"));
        public IWebElement Popular => Driver.TryGetWebElement(By.CssSelector("a.homefeatured"));
        public IWebElement BestSellers => Driver.TryGetWebElement(By.CssSelector("a.blockbestsellers"));
        public IList<IWebElement> FeaturedHomePageItems => Driver.FindElements(By.CssSelector("ul#homefeatured h5 a.product-name"));

        //Footer
        public IWebElement SocialBlock => Driver.TryGetWebElement(By.CssSelector("#social_block"));
        public IWebElement Newsletter => Driver.TryGetWebElement(By.CssSelector("#newsletter-input"));
        public IWebElement StoreInfo => Driver.TryGetWebElement(By.CssSelector("#block_contact_infos"));

        public string SelectRandomItemTextToSearch()
        {
            OpenHomePage();

            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("ul#homefeatured h5 a.product-name")));

            var itemsCount = _featuredHomePageItemNames.Count();
            var random = new Random();
            var randomItem = random.Next(itemsCount - 1);

            ReporterHelper.Log(AventStack.ExtentReports.Status.Info, 
                $"Random item product name is: {_featuredHomePageItemNames[randomItem].Text}");
            return _featuredHomePageItemNames[randomItem].Text;
        }

        public string AddItemToTheCart()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("#homefeatured a.product_img_link")));
            
            var itemName = _featuredHomePageItemNames[0].Text;

            Actions actions = new Actions(Driver);

            actions.MoveToElement(_featuredItems[0]);
            actions.MoveToElement(_addToCartBtn);

            actions.Click().Build().Perform();

            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div#layer_cart")));           
            ReporterHelper.Log(AventStack.ExtentReports.Status.Info,
                $"Item {itemName} is added to the cart");

            return itemName;
        }

    }

}
