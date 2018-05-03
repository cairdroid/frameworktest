using System;
using NUnit.Framework;
using OpenQA.Selenium;
using StrategyPattern.Util;

namespace StrategyPattern.ActionsLayer
{
    class ChangeKeywordAction : IKeywordAction
    {
        public enum ChangeAction
        {
            ChangeToIframeContext,
            ChangeToCurrentContext
        }
        
        public void DoAction()
        {
            switch (Enum.Parse(typeof(ChangeAction), (string)SValues.valuesList[2]))
            {
                case ChangeAction.ChangeToIframeContext:
                    ChangeToIframeContext((string)SValues.valuesList[3], (IWebDriver)SValues.valuesList[0]);
                    SValues.Logger.Info("Running Action : " + ChangeAction.ChangeToIframeContext + " For Locator : " + (string)SValues.valuesList[3]);
                    break;
                case ChangeAction.ChangeToCurrentContext:
                    ChangeToCurrentContext((string)SValues.valuesList[3], (IWebDriver)SValues.valuesList[0]);
                    SValues.Logger.Info("Running Action : " + ChangeAction.ChangeToCurrentContext + " For Locator : " + (string)SValues.valuesList[3]);
                    break;
                default:
                    SValues.Logger.Info("Assert Failed For Locator : " + (string)SValues.valuesList[3] + " |°_°| Wrong |°_°|");
                    Assert.IsFalse(false);
                    break;
            }
        }

        public static void ChangeToIframeContext(string locator, IWebDriver driver)
        {
            Helper.HighlightElement(driver, driver.FindElement(By.XPath(locator)));
            var frameContainer = driver.FindElement(By.XPath(locator));
            //Switching to Iframe Context
            driver.SwitchTo().Frame(frameContainer);
        }

        public static void ChangeToCurrentContext(string locator, IWebDriver driver)
        {
            //Switching to DefaultContent leaving IFrame
            driver.SwitchTo().DefaultContent();
        }
    }
}
