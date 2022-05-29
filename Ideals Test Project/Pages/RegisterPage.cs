﻿using Ideals_Test_Project.Models;
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
        private IWebElement _emailAddressOnCreate => driver.FindElement(By.CssSelector("#email_create"));
        private IWebElement _createAccountBtn => driver.FindElement(By.CssSelector("#SubmitCreate"));       
        private IWebElement _firstName => driver.FindElement(By.CssSelector("#customer_firstname"));
        private IWebElement _lastName => driver.FindElement(By.CssSelector("#customer_lastname"));
        private IWebElement _email => driver.FindElement(By.CssSelector("#email"));
        private IWebElement _password => driver.FindElement(By.CssSelector("#passwd"));
        private IWebElement _address => driver.FindElement(By.CssSelector("#address1"));
        private IWebElement _addressFirstName => driver.FindElement(By.CssSelector("input.form-control#firstname"));
        private IWebElement _addressLastName => driver.FindElement(By.CssSelector("input.form-control#lastname"));
        private IWebElement _city => driver.FindElement(By.CssSelector("#city"));
        private IWebElement _postcode => driver.FindElement(By.CssSelector("#postcode"));
        private IWebElement _state => driver.FindElement(By.CssSelector("#id_state"));       
        private IWebElement _mobilePhone => driver.FindElement(By.CssSelector("#phone_mobile"));
        private IWebElement _addressAlias => driver.FindElement(By.CssSelector("#alias"));
        private IWebElement _registerBtn => driver.FindElement(By.CssSelector("button#submitAccount"));

        public void EnterEmail(string emailAddress)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#email_create")));

            _emailAddressOnCreate.SendKeys(emailAddress);
        }

        public void OpenCreateAccountForm()
        {
            _createAccountBtn.Click();
        }

        public void FillInRegistrationForm(CustomerInfoModel customerInfo)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
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

            _registerBtn.Click();
        }
    }
}
