using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using Dapper;

namespace FrameworkDresses.DataAccess
{
    class ExcelDataAccess
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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
                    Logger.Info("Troubles trying to open file : "+ fileName + " |*| Error |*| " + e);
                }
                var query = $"select * from [{keyName}$]";
                var anonymousList = connection.Query(query).ToList();
                connection.Close();
                return anonymousList;
            }
        }
    }
}
