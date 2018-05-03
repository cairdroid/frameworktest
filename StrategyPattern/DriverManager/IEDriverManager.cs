using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;

namespace StrategyPattern.DriverManager
{
    class IEDriverManager : DriverManager
    {
        protected override void CreateWebDriverInstance()
        {
            Driver = new InternetExplorerDriver(new InternetExplorerOptions()) { Url = ConfigurationManager.AppSettings["URL"] };
            Driver.Manage().Window.Maximize();
        }
    }
}
