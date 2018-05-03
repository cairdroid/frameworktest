using System;
using System.Collections.Generic;
using StrategyPattern.Util;
using OpenQA.Selenium;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;

namespace StrategyPattern.ActionsLayer
{
    class VerifyKeywordAction : IKeywordAction
    {
        private bool _trackingVerify;

        IWebElement _elementselected;
        public enum VerifyAction
        {
            VerifyCardTotals,
            VerifyCardDetail,
            VerifyCardIsEmpty,
            VerifyMessage,
            VerifyErrorMessage,
            VerifyElementDisplayed
        }

        public void DoAction()
        {
            var passYesNot = false;
            switch (Enum.Parse(typeof(VerifyAction), (string)SValues.valuesList[2]))
            {
                case VerifyAction.VerifyCardIsEmpty:
                    SValues.Logger.Info("Running Action : " + VerifyAction.VerifyCardIsEmpty + " For Locator : " + (string)SValues.valuesList[3]);
                    VerifyAssert(VerifyCardIsEmpty((string)SValues.valuesList[3], (string)SValues.valuesList[4], (IWebDriver)SValues.valuesList[0]), (string)SValues.valuesList[3], (string)SValues.valuesList[4]);
                    break;
                case VerifyAction.VerifyErrorMessage:
                    SValues.Logger.Info("Running Action : " + VerifyAction.VerifyErrorMessage + " For Locator : " + (string) SValues.valuesList[3]);
                    VerifyAssert(VerifyErrorMessage((string)SValues.valuesList[3], (string)SValues.valuesList[4], (IWebDriver)SValues.valuesList[0]), (string)SValues.valuesList[3], (string)SValues.valuesList[4]);
                    break;
                case VerifyAction.VerifyMessage:
                    SValues.Logger.Info("Running Action : " + VerifyAction.VerifyMessage + " For Locator : " + (string)SValues.valuesList[3]);
                    VerifyAssert(VerifyMessage((string)SValues.valuesList[3], (string)SValues.valuesList[4], (IWebDriver)SValues.valuesList[0]), (string)SValues.valuesList[3], (string)SValues.valuesList[4]);
                    break;
                case VerifyAction.VerifyElementDisplayed:
                    SValues.Logger.Info("Running Action : " + VerifyAction.VerifyElementDisplayed + " For Locator : " + (string)SValues.valuesList[3]);
                    VerifyAssert(VerifyElementDisplayed((string)SValues.valuesList[3], (string)SValues.valuesList[4], (IWebDriver)SValues.valuesList[0]), (string)SValues.valuesList[3], (string)SValues.valuesList[4]);
                    break;
                case VerifyAction.VerifyCardDetail:
                    SValues.Logger.Info("Running Action : " + VerifyAction.VerifyCardDetail + " For Locator : " + (string)SValues.valuesList[3]);
                    VerifyAssert(VerifyCardDetail((string)SValues.valuesList[3], (string)SValues.valuesList[4], (IWebDriver)SValues.valuesList[0]), (string)SValues.valuesList[3], (string)SValues.valuesList[4]);
                    break;
                case VerifyAction.VerifyCardTotals:
                    SValues.Logger.Info("Running Action : " + VerifyAction.VerifyCardTotals + " For Locator : " + (string)SValues.valuesList[3]);
                    VerifyAssert(VerifyCardTotals((string)SValues.valuesList[3], (string)SValues.valuesList[4], (IWebDriver)SValues.valuesList[0]), (string)SValues.valuesList[3], (string)SValues.valuesList[4]);
                    break;
                default:
                    break;
            }
        }

        public bool VerifyErrorMessage(string locator, string dataTest, IWebDriver driver)
        {
            try
            {
                WaitKeywordAction.WaitForElement(locator,driver);
                Helper.HighlightElement(driver, driver.FindElement(By.XPath(locator)));
                _elementselected = driver.FindElement(By.XPath(locator));
                return _elementselected.Text.Equals(dataTest);
            }
            catch (NoSuchElementException elementNotVisibleException)
            {
                SValues.Logger.Error(
                    "Element : " + locator + "  Was Not Visible, * Error Message *" + elementNotVisibleException);
                return false;
            }
        }

        public bool VerifyMessage(string locator, string dataTest, IWebDriver driver)
        {
            try
            {
                Helper.HighlightElement(driver, driver.FindElement(By.XPath(locator)));
                _elementselected = driver.FindElement(By.XPath(locator));
                return _elementselected.Text.Equals(dataTest);
            }
            catch (NoSuchElementException elementNotVisibleException)
            {
                SValues.Logger.Error(
                    "Element : " + locator + "  Was Not Visible, * Error Message *" + elementNotVisibleException);
                return false;
            }
        }

        public bool VerifyElementDisplayed(string locator, string dataTest, IWebDriver driver)
        {
            try
            {
                Helper.HighlightElement(driver, driver.FindElement(By.XPath(locator)));
                _elementselected = driver.FindElement(By.XPath(locator));
            }
            catch (NoSuchElementException elementNotVisibleException)
            {
                SValues.Logger.Error(
                    "Element : " + locator + "  Was Not Visible, * Error Message *" + elementNotVisibleException);
                return false;
            }

            return _elementselected.Displayed;
        }

        public bool VerifyCardIsEmpty(string locator, string dataTest, IWebDriver driver)
        {
            Helper.HighlightElement(driver, driver.FindElement(By.XPath(locator)));
            _elementselected = driver.FindElement(By.XPath(locator));

            return _elementselected.Text.Equals(dataTest);
        }

        public bool VerifyCardTotals(string locator, string dataTest, IWebDriver driver)
        {
            _trackingVerify = false;
            // Reading Foot Sumary
            var count = 0;
            Helper.HighlightElement(driver, driver.FindElement(By.XPath(locator)));
            _elementselected = driver.FindElement(By.XPath(locator));
            IList<IWebElement> tableFootElements = _elementselected.FindElements(By.TagName("tr"));

            foreach (var row in tableFootElements)
            {
                IList<IWebElement> rowTd = row.FindElements(By.TagName("td"));
                if (count.Equals(2) || count.Equals(4))
                    _trackingVerify = CheckingTotals(rowTd[0].Text, rowTd[1].Text, locator).Equals(1);
                count++;
            }
            return _trackingVerify;
        }

        public bool VerifyCardDetail(string locator, string dataTest, IWebDriver driver)
        {
            // Reading Body Summary
            Helper.HighlightElement(driver, driver.FindElement(By.XPath(locator)));
            _elementselected = driver.FindElement(By.XPath(locator));
            IList<IWebElement> tableBodyElements = _elementselected.FindElements(By.TagName("tr"));

            //Going through all rows
            foreach (var row in tableBodyElements)
            {
                IList<IWebElement> rowTd = row.FindElements(By.TagName("td"));

                //Getting Values to verify
                _trackingVerify = CheckingImg(rowTd[0].FindElement(By.XPath("//td[@class='cart_product']/a/img")).GetAttribute("src"), locator).Equals(1);
                _trackingVerify = CheckingColorSize(rowTd[1].Text, locator).Equals(1);
                _trackingVerify = CheckingTotals("Unit Price", rowTd[3].Text, locator).Equals(1);
                _trackingVerify = CheckingQty(rowTd[4].FindElement(By.XPath("//input[@class='cart_quantity_input form-control grey']")).GetAttribute("value"), locator).Equals(1);
            }
            return _trackingVerify;
        }

        public int CheckingColorSize(string colorSizeString, string locator)
        {
            var colorSize = colorSizeString.Split('\n');
            var splitColorSize = colorSize[2].Split(',');
            //Verify Color & Size
            if (splitColorSize[0].Length > 7 && splitColorSize[1].Length > 7) return 1;
            SValues.Logger.Info(
                "Error Trying to Verify Color & Size : " + locator);
            return 0;
        }

        public int CheckingImg(string imgContent, string locator)
        {
            if (imgContent.Length > 0) return 1;
            SValues.Logger.Info(
                "Error Trying to Verify Image : " + locator);
            return 0;
        }

        public int CheckingQty(string qty, string locator)
        {
            if (Regex.IsMatch(qty, @"\d")) return 1;
            SValues.Logger.Info(
                "Error Trying to Verify Quantity : " + locator);
            return 0;
        }

        public int CheckingTotals(string rowName, string total, string locator)
        {
            if (Regex.IsMatch(total, @"\$[0-9]*.[0-9]*")) return 1;
            SValues.Logger.Info(
                "Error Trying to Verify " + rowName + " locator : " + locator);
            return 0;
        }

        public void VerifyAssert(bool passYesNot, string locaterTest, string dataTest)
        {
            try
            {
                Assert.IsTrue(passYesNot, " Was Expecting : " + dataTest);
                SValues.Logger.Info("Assert Passed For Locator : " + locaterTest + " |°-°| Successfully |°-°|");
            }
            catch (AssertionException ae)
            {
                SValues.Logger.Error("Assert Failed For Locator : " + locaterTest + " * Error Message * : " + ae);
                Assert.Fail();
            }
        }
    }
}
