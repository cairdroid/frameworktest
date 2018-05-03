using System;
using NUnit.Framework;
using OpenQA.Selenium;
using StrategyPattern.Util;

namespace StrategyPattern.ActionsLayer
{
    class CheckKeywordAction : IKeywordAction
    {
        public enum CheckAction
        {
            CheckText
        }

        public void DoAction()
        {
            switch (Enum.Parse(typeof(CheckAction), (string)SValues.valuesList[2]))
            {
                case CheckAction.CheckText:
                    CheckText((string)SValues.valuesList[3], (string)SValues.valuesList[4], (IWebDriver)SValues.valuesList[0]);
                    break;
                default:

                    Assert.IsFalse(false);
                    break;
            }
        }

        public static bool CheckText(string locator, string testData, IWebDriver driver)
        {
            Helper.HighlightElement(driver, driver.FindElement(By.XPath(locator)));
            return driver.FindElement(By.XPath(locator)).Text.Equals(testData);
        }
    }
}
