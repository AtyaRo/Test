using Ideals_Test_Project.Helpers;
using Ideals_Test_Project.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Ideals_Test_Project.Pages
{
    public class RegisterPage: BasePage
    {
        public RegisterPage(IWebDriver driver) : base(driver)
        {

        }
        private IWebElement _emailAddressOnCreate => Driver.FindElement(By.CssSelector("#email_create"));
        private IWebElement _createAccountBtn => Driver.FindElement(By.CssSelector("#SubmitCreate"));       
        private IWebElement _firstName => Driver.FindElement(By.CssSelector("#customer_firstname"));
        private IWebElement _lastName => Driver.FindElement(By.CssSelector("#customer_lastname"));
        private IWebElement _email => Driver.FindElement(By.CssSelector("#email"));
        private IWebElement _password => Driver.FindElement(By.CssSelector("#passwd"));
        private IWebElement _address => Driver.FindElement(By.CssSelector("#address1"));
        private IWebElement _addressFirstName => Driver.FindElement(By.CssSelector("input.form-control#firstname"));
        private IWebElement _addressLastName => Driver.FindElement(By.CssSelector("input.form-control#lastname"));
        private IWebElement _city => Driver.FindElement(By.CssSelector("#city"));
        private IWebElement _postcode => Driver.FindElement(By.CssSelector("#postcode"));
        private IWebElement _state => Driver.FindElement(By.CssSelector("#id_state"));       
        private IWebElement _mobilePhone => Driver.FindElement(By.CssSelector("#phone_mobile"));
        private IWebElement _addressAlias => Driver.FindElement(By.CssSelector("#alias"));
        private IWebElement _registerBtn => Driver.FindElement(By.CssSelector("button#submitAccount"));

        public void EnterEmail(string emailAddress)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#email_create")));

            _emailAddressOnCreate.SendKeys(emailAddress);
            ReporterHelper.Log(AventStack.ExtentReports.Status.Info, $"Email address {emailAddress} is entered");
        }

        public void OpenCreateAccountForm()
        {
            _createAccountBtn.Click();
            ReporterHelper.Log(AventStack.ExtentReports.Status.Info, $"Proceeding to Create Account form");
        }

        public void FillInRegistrationForm(CustomerInfoModel customerInfo)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#customer_firstname")));

            _firstName.SendKeys(customerInfo.FirstName);
            _lastName.SendKeys(customerInfo.LastName);
            _password.SendKeys(customerInfo.Password);
            _addressFirstName.SendKeys(customerInfo.FirstName);
            _addressLastName.SendKeys(customerInfo.LastName);
            _address.SendKeys(customerInfo.Address);
            _city.SendKeys(customerInfo.City);
            _postcode.SendKeys(customerInfo.PostalCode);
            _mobilePhone.SendKeys(customerInfo.MobilePhone);
            _addressAlias.SendKeys(customerInfo.AddressAlias);

            var selectElement = new SelectElement(_state);
            selectElement.SelectByIndex(1);
            ReporterHelper.Log(AventStack.ExtentReports.Status.Info, 
                $"CustomerInfo form is prefilled with Customer data");

            _registerBtn.Click();
            ReporterHelper.Log(AventStack.ExtentReports.Status.Info,
                $"Submitting registration form");
        }
    }
}
