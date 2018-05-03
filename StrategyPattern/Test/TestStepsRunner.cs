using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text.RegularExpressions;
using NUnit.Framework;
using OpenQA.Selenium;
using StrategyPattern.ActionsLayer;
using StrategyPattern.Util;

namespace StrategyPattern.Test
{
    class TestStepsRunner
    {
        protected static IWebDriver _driver;
        protected static string[] stepsByRow = {"","","","" };

        public TestStepsRunner()
        {
            
        }

        public TestStepsRunner(IWebDriver driver)
        {
            _driver = driver;
        }
        
        public enum EGlobalActions
        {
            Click,
            Set,
            Verify,
            Check,
            Change,
            Go,
            Wait,
            Mouse
        }

        private void RunningTestCasesSteps(string testCaseN, EGlobalActions action, string keyword, string locator, string data)
        {
            SValues.valuesList.Clear();
            SValues.valuesList.Add(_driver);
            SValues.valuesList.Add(testCaseN);
            SValues.valuesList.Add(keyword);
            SValues.valuesList.Add(locator);
            SValues.valuesList.Add(data);

            Context.DoAction(action);
        }

        public void SplittingTestCases(List<string> listStepToSplit)
        {
            var actualTestName = TestContext.CurrentContext.Test.Name;

            foreach (var t in listStepToSplit)
            {
                stepsByRow = t.Split('|');
                //starting process for read testCase, Checking for Test name running.
                if (actualTestName.Equals(stepsByRow[0]))
                {
                    //Splitting Action to Get Global type
                    var subtype = Regex.Split(stepsByRow[1], @"(?<!^)(?=[A-Z])");
                    
                    RunningTestCasesSteps(stepsByRow[0], (EGlobalActions)Enum.Parse(typeof(EGlobalActions), subtype[0]), stepsByRow[1], stepsByRow[2], stepsByRow[3]);
                }
                else if (stepsByRow[0] == null)
                {
                    break;
                }
            }
        }
    }
}
