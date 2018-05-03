using System;
using OpenQA.Selenium;
using StrategyPattern.Util;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;

namespace StrategyPattern.ActionsLayer
{
    class WaitKeywordAction : IKeywordAction
    {
        public enum WaitAction
        {
            WaitForElement
        }

        public void DoAction()
        {
            switch (Enum.Parse(typeof(WaitAction), (string)SValues.valuesList[2]))
            {
                case WaitAction.WaitForElement:
                    WaitForElement((string)SValues.valuesList[3], (IWebDriver)SValues.valuesList[0]);

                    break;
                default:

                    Assert.IsFalse(false);
                    break;
            }
        }

        public static void WaitForElement(string locator, IWebDriver driver)
        {
            try
            {
                SValues._wait = new WebDriverWait(driver, TimeSpan.FromSeconds(SValues.MaximunWaitTime));
                SValues._wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath(locator)));
            }
            catch (WebDriverTimeoutException wdException)
            {
                SValues.Logger.Info("Failed trying to locate element : " + locator + " * Error Message * : " + wdException);
            }
        }
    }
}
