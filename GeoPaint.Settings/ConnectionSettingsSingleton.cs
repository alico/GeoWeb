using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoPaint.Settings
{
    public class ConnectionSettingsSingleton
    {
        private static readonly ConnectionSettingsSingleton _instance =
          new ConnectionSettingsSingleton();

        private ConnectionSettings _settings;

        private string dbProvider = ConfigurationManager.AppSettings["DbProvider"];

        private ConnectionSettingsSingleton()
        {
            switch (dbProvider)
            {
                case "XML":
                    {
                        _settings = new ConnectionSettings
                        {
                            DataConnectionString = ConfigurationManager.AppSettings["XMLFilePath"],
                            DataProvider = DataProviderType.XmlFileStore
                        };
                        break;
                    }
                case "SQL":
                    {
                        _settings = new ConnectionSettings
                        {
                            DataConnectionString = ConfigurationManager.ConnectionStrings["Test"].ConnectionString,
                            DataProvider = DataProviderType.SqlServer
                        };
                        break;
                    }

                default:
                    break;
            }



        }

        public static ConnectionSettingsSingleton GetConnectionSettings()
        {
            return _instance;
        }

        public ConnectionSettings Setting
        {
            get
            {
                return _settings;
            }
        }
    }
}
