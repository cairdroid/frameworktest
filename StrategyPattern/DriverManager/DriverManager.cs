using OpenQA.Selenium;

namespace StrategyPattern.DriverManager
{
    abstract class DriverManager
    {
        protected IWebDriver Driver;
        protected abstract void CreateWebDriverInstance();

        public void QuitDriver()
        {
            if (Driver != null)
            {
                Driver.Quit();
                Driver = null;
            }
        }

        public IWebDriver GetDriver()
        {
            if (Driver == null)
            {
                CreateWebDriverInstance();
            }
            return Driver;
        }

    }
}
