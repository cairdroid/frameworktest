using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace StrategyPattern.Util
{
    class SValues
    {
        public static IWebDriver _driver;
        public static List<object> valuesList = new List<object>();
        public static WebDriverWait _wait;
        public const int MaximunWaitTime = 10;
        public static readonly log4net.ILog Logger =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    }
}
