using System.Collections.Generic;
using StrategyPattern.Util;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using Dapper;

namespace StrategyPattern.DataAccess
{
    class ExcelDataAccess
    {
        private static string fileName;

        public static string TestDataFileConnection()
        {
            fileName = ConfigurationManager.AppSettings["TestDataSheetPath"];
            var con = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = {fileName}; Extended Properties=Excel 12.0;";
            return con;
        }

        public static List<dynamic> GetTestsData(string keyName)
        {
            using (var connection = new OleDbConnection(TestDataFileConnection()))
            {
                try
                {
                    connection.Open();
                }
                catch (OleDbException e)
                {
                    SValues.Logger.Error("Troubles trying to open file : "+ fileName + " |*| Error |*| " + e);
                }
                var query = $"select * from [{keyName}$]";
                var anonymousList = connection.Query(query).ToList();
                connection.Close();
                return anonymousList;
            }
        }
    }
}
