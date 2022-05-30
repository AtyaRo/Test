using Ideals_Test_Project.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;


namespace Ideals_Test_Project.Pages
{
    public class BasePage
    {
        protected IWebDriver? driver;

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void OpenHomePage()
        {
            driver.Navigate().GoToUrl(Constants.HomePage);
            ReporterHelper.Log(AventStack.ExtentReports.Status.Info, $"Home page is opened: {Constants.HomePage}");

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("a.login")));
        }

    }
}
