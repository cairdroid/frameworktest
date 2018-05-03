using System;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Configuration;
using StrategyPattern.DataAccess;
using StrategyPattern.DriverManager;

namespace StrategyPattern.Test
{
    [TestFixture]
    public class Test
    {
        enum Sheet
        {
            Sheet1
        }

        private IWebDriver _driver;
        private TestStepsRunner _testCaseRunnerStepsObj;
        static List<dynamic> _fullDataFromExcel;
        private StrategyPattern.DriverManager.DriverManager _driverManager;
        //private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [SetUp]
        public void SetUpAllTestCases()
        {
            var setBrowser = ConfigurationManager.AppSettings["Browser"];
            _driverManager = DriverManagerFactory.getManager((DriverManagerFactory.DriverType)Enum.Parse(typeof(DriverManagerFactory.DriverType), setBrowser));
            _driver = _driverManager.GetDriver();
        }

        public static IEnumerable<dynamic> GetNamesOfTestCases()
        {
            var stepsForTestCase = new List<string>();
            _fullDataFromExcel = ExcelDataAccess.GetTestsData(Sheet.Sheet1.ToString());
            var tmpTName = "";
            var tmpTNameOld = "";// = _fullDataFromExcel[0].TestCase;
            var i = 1;

           // SValues.Logger.Info("|-| Getting list full list of textcases |-|");
            //Get TestCase Name
            foreach (var t in _fullDataFromExcel)
            {
                //Chaning Test Case Name
                if (!string.IsNullOrEmpty(t.TestCase))
                {
                    tmpTName = (string)t.TestCase;
                }

                //Get Steps for TestCase
                stepsForTestCase.Add(tmpTName + "|" + t.Keyword + "|" + t.Locator + "|" + t.Data + "|" + i);
                
                if (tmpTName != tmpTNameOld && i >1)
                {
                    yield return new TestCaseData(new[] { stepsForTestCase }).SetName(tmpTName);
                    i = 0;
                }
                tmpTNameOld = tmpTName;
                i++;
               
            }
        }

        [TestCaseSource(nameof(GetNamesOfTestCases))]
        public void TestCaseExecuter(List<string> stepsForTestCase)
        {
            _testCaseRunnerStepsObj = new TestStepsRunner(_driver);
            _testCaseRunnerStepsObj.SplittingTestCases(stepsForTestCase);
           
        }

        [TearDown]
        public void AtTheEnd()
        {
            _driverManager.QuitDriver();
        }
    }
}
