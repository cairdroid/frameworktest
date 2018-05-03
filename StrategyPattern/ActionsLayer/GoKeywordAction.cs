using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using StrategyPattern.Util;

namespace StrategyPattern.ActionsLayer
{
    class GoKeywordAction : IKeywordAction
    {
        public enum GoAction
        {
            GoToUrl
        }

        public void DoAction()
        {
            switch (Enum.Parse(typeof(GoAction), (string)SValues.valuesList[2]))
            {
                case GoAction.GoToUrl:
                    GoTo((string)SValues.valuesList[3], (string)SValues.valuesList[4], (IWebDriver)SValues.valuesList[0]);

                    break;
                default:

                    Assert.IsFalse(false);
                    break;
            }
        }

        public static void GoTo(string locator, string testData, IWebDriver driver)
        {
            driver.Navigate().GoToUrl(testData);
        }
    }
}
