using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GeoPaint.Settings
{
    public class ConnectionSettings
    {
        [JsonProperty("dataProvider")]
        public DataProviderType DataProvider { get; set; }

        [JsonProperty("dataConnectionString")]
        public string DataConnectionString { get; set; }
    }
}
