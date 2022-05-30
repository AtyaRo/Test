using Ideals_Test_Project.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;


namespace Ideals_Test_Project.Pages
{
    public class BasePage
    {
        protected IWebDriver? Driver;

        public BasePage(IWebDriver driver)
        {
            this.Driver = driver;
        }

        public void OpenHomePage()
        {
            Driver.Navigate().GoToUrl(Constants.HomePage);
            ReporterHelper.Log(AventStack.ExtentReports.Status.Info, $"Home page is opened: {Constants.HomePage}");

            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("a.login")));
        }

    }
}
