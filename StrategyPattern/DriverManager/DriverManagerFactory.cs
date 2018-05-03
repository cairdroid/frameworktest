using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyPattern.DriverManager
{
    class DriverManagerFactory
    {
        public enum DriverType
        {
            Chrome,
            Firefox,
            Ie
        }

        public static DriverManager getManager(DriverType type)
        {
            DriverManager driverManager;

            switch (type)
            {
                case DriverType.Chrome:
                    driverManager = new ChromeDriverManager();
                    break;
                case DriverType.Firefox:
                    driverManager = new FirefoxDriverManager();
                    break;
                case DriverType.Ie:
                    driverManager = new IEDriverManager();
                    break;
                default:
                    driverManager = null;
                    break;

            }
            return driverManager;
        }
    }
}
