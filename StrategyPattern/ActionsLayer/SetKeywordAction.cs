using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using StrategyPattern.Util;

namespace StrategyPattern.ActionsLayer
{
    class SetKeywordAction : IKeywordAction
    {
        static SelectElement dropdown;

        public enum SetAction
        {
            SetText,
            SetTextDropDown,
            SetValueDropDown
        }        

        public void DoAction()
        {
            switch (Enum.Parse(typeof(SetAction), (string)SValues.valuesList[2] ))
            {
                case SetAction.SetText:
                    SetText((string)SValues.valuesList[3], (string)SValues.valuesList[4], (IWebDriver)SValues.valuesList[0]);

                    break;
                case SetAction.SetValueDropDown:
                    SetByValueDropDown((string)SValues.valuesList[3], (string)SValues.valuesList[4], (IWebDriver)SValues.valuesList[0]);

                    break;
                case SetAction.SetTextDropDown:
                    SetByTextDropDown((string)SValues.valuesList[3], (string)SValues.valuesList[4], (IWebDriver)SValues.valuesList[0]);

                    break;
                default:

                    Assert.IsFalse(false);
                    break;
            }
        }

        public static void SetText(string locator, string testData, IWebDriver driver)
        {
            try
            {
                WaitKeywordAction.WaitForElement(locator, driver);
                Helper.HighlightElement(driver, driver.FindElement(By.XPath(locator)));
                driver.FindElement(By.XPath(locator)).Clear();
                driver.FindElement(By.XPath(locator)).SendKeys(testData);
                //Doing click on blank area in page
                driver.FindElement(By.XPath("//body")).Click();
            }
            catch (ElementNotVisibleException ve)
            {
                SValues.Logger.Error("Trying to locate element : " + locator );
                Assert.Fail();
            }
        }

        public static void SetByValueDropDown(string locator, string testData, IWebDriver driver)
        {
            WaitKeywordAction.WaitForElement(locator, driver);
            Helper.HighlightElement(driver, driver.FindElement(By.XPath(locator)));
            dropdown = new SelectElement(driver.FindElement(By.XPath(locator)));
            dropdown.SelectByValue(testData);
        }

        public static void SetByTextDropDown(string locator, string testData, IWebDriver driver)
        {
            WaitKeywordAction.WaitForElement(locator, driver);
            Helper.HighlightElement(driver, driver.FindElement(By.XPath(locator)));
            dropdown = new SelectElement(driver.FindElement(By.XPath(locator)));
            dropdown.SelectByText(testData);
        }

    }
}
