using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace FrameworkDresses.DriverManager
{
    class FirefoxDriverManager : DriverManager
    {
        protected override void CreateWebDriverInstance()
        {
            Driver = new FirefoxDriver(new FirefoxOptions()) { Url = ConfigurationManager.AppSettings["URL"] };
            Driver.Manage().Window.Maximize();
        }
    }
}
