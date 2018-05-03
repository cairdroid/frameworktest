using System;
using System.Collections.Generic;
using StrategyPattern.Util;
using OpenQA.Selenium;
using NUnit.Framework;
using StrategyPattern.Util;

namespace StrategyPattern.ActionsLayer
{
    class MouseKeywordAction : IKeywordAction
    {
        IWebElement _elementselected;

        public enum MouseAction
        {
            MouseOver
        }

        public void DoAction()
        {
            switch (Enum.Parse(typeof(MouseAction), (string)SValues.valuesList[2]))
            {
                case MouseAction.MouseOver:
                    MouseOver((string)SValues.valuesList[3], (IWebDriver)SValues.valuesList[0]);

                    break;
                default:

                    Assert.IsFalse(false);
                    break;
            }
        }

        public bool MouseOver(string locator, IWebDriver driver)
        {
            Helper.HighlightElement(driver, driver.FindElement(By.XPath(locator)));
            var action = new OpenQA.Selenium.Interactions.Actions(driver);
            _elementselected = driver.FindElement(By.XPath(locator));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", _elementselected);
            action.MoveToElement(_elementselected).Perform();
            return true;

        }
    }
}
