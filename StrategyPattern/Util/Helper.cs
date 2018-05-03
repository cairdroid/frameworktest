using System;
using System.Threading;
using OpenQA.Selenium;

namespace StrategyPattern.Util
{
    class Helper
    {public static void HighlightElement(IWebDriver driver, IWebElement Element)
        {
            var jsExecutor = (IJavaScriptExecutor)driver;
            jsExecutor.ExecuteScript("arguments[0].setAttribute('style', 'background-color: Tomato; border: 4px solid Yellow;');", Element);

            try
            {
                Thread.Sleep(1000);
            }
            catch (ThreadInterruptedException te)
            {
                Console.WriteLine(te);
                throw;
            }

            jsExecutor.ExecuteScript("arguments[0].setAttribute('style','border: solid 2px white');", Element);
        }
    }
}
