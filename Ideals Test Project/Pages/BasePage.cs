using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ideals_Test_Project.Pages
{
    public class BasePage
    {
        protected IWebDriver? driver;
        private const string _homePage = "http://automationpractice.com/index.php";

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
        }
      
    }
}
