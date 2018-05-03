using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace StrategyPattern.Util
{
    class FancyActions: Helper
    {
        public void Click(string elementId, IWebDriver driver)
        {
            Click(By.XPath(elementId), driver);
        }

        public void Click(By by, IWebDriver driver)
        {
            var element = driver.FindElement(by);
            HighlightElement(driver, element);
            element.Click();
        }
    }
}
