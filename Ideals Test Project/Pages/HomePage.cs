using Ideals_Test_Project.Extensions;
using OpenQA.Selenium;

namespace Ideals_Test_Project.Pages
{
    public class HomePage : BasePage
    {
        public HomePage(IWebDriver driver) : base(driver)
        {
        }

        //Header
        public IWebElement HeaderBanner => driver.TryGetWebElement(By.CssSelector("#header div.banner a img"));
        public IWebElement ContactUs => driver.TryGetWebElement(By.CssSelector("div#contact-link a"));
        public IWebElement SignIn => driver.TryGetWebElement(By.CssSelector("a.login"));
        public IWebElement CategoriesBlock => driver.TryGetWebElement(By.CssSelector("ul.sf-menu.menu-content.sf-js-enabled"));
        public IWebElement SearchField => driver.TryGetWebElement(By.ClassName("search_query"));
        public IWebElement AddToCartBtn => driver.TryGetWebElement(By.CssSelector(".button-container a.button.ajax_add_to_cart_button"));

        //Main block
        public IWebElement SaleBlock => driver.TryGetWebElement(By.CssSelector("#homeslider a img"));
        public IWebElement Popular => driver.TryGetWebElement(By.CssSelector("a.homefeatured"));
        public IWebElement BestSellers => driver.TryGetWebElement(By.CssSelector("a.blockbestsellers"));
        public IList<IWebElement> FeaturedHomePageItems => driver.FindElements(By.CssSelector("ul#homefeatured h5 a.product-name"));

        //Footer
        public IWebElement SocialBlock => driver.TryGetWebElement(By.CssSelector("#social_block"));
        public IWebElement Newsletter => driver.TryGetWebElement(By.CssSelector("#newsletter-input"));
        public IWebElement StoreInfo => driver.TryGetWebElement(By.CssSelector("#block_contact_infos"));
    }

}
