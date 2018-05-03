using System.Configuration;
using OpenQA.Selenium.Chrome;

namespace FrameworkDresses.DriverManager
{
    class ChromeDriverManager : DriverManager
    {
        protected override void CreateWebDriverInstance()
        {
            Driver = new ChromeDriver(new ChromeOptions()) {Url = ConfigurationManager.AppSettings["URL"]};
            Driver.Manage().Window.Maximize();
        }
    }
}
