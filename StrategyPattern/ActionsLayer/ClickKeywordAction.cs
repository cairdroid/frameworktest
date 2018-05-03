using System;
using NUnit.Framework;
using OpenQA.Selenium;
using StrategyPattern.Util;

namespace StrategyPattern.ActionsLayer
{
    class ClickKeywordAction :  IKeywordAction
    {

        public enum ClickAction
        {
            ClickIcon,
            ClickImage,
            ClickButton
        }

        public void DoAction()
        {
            switch (Enum.Parse(typeof(ClickAction), (string)SValues.valuesList[2]))
            {
                case ClickAction.ClickButton:
                    Click((string)SValues.valuesList[3], (IWebDriver)SValues.valuesList[0]);
                    SValues.Logger.Info("Running Action : " + ClickAction.ClickButton + " For Locator : " + (string)SValues.valuesList[3]);
                    break;
                case ClickAction.ClickIcon:
                    Click((string)SValues.valuesList[3], (IWebDriver)SValues.valuesList[0]);
                    SValues.Logger.Info("Running Action : " + ClickAction.ClickIcon + " For Locator : " + (string)SValues.valuesList[3]);
                    break;
                case ClickAction.ClickImage:
                    Click((string)SValues.valuesList[3], (IWebDriver)SValues.valuesList[0]);
                    SValues.Logger.Info("Running Action : " + ClickAction.ClickImage + " For Locator : " + (string)SValues.valuesList[3]);
                    break;
                default:
                    Assert.IsFalse(false);
                    break;
            }
        }

        public static void Click(string locator, IWebDriver driver)
        {
            Click(locator, driver);
        }

    }
}
