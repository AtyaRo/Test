using OpenQA.Selenium;

namespace Ideals_Test_Project.Extensions
{
    public static class DriverExtensions
    {
        public static IWebElement TryGetWebElement(this IWebDriver driver, By selector)
        {
            try
            {
                return driver.FindElement(selector);
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }
    }
}
