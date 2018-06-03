using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using GeoPaint.Entities;
using GeoPaint.Settings;
namespace GeoPaint.Data.Contexts
{
    public partial class GeoPaintXMLContext
    {
        public GeoPaintXMLContext(ConnectionSettings appSettings)
        {
            _xmlFileLocation = appSettings.DataConnectionString;
        }

        readonly string _xmlFileLocation;

        public void SaveChanges<T>(IList<T> entities)
            where T : class
        {
            if (entities == null)
                return;

            bool exists = Directory.Exists(_xmlFileLocation);

            if (!exists)
                System.IO.Directory.CreateDirectory(_xmlFileLocation);

            string filePath = Path.Combine(_xmlFileLocation, $"{typeof(T).Name}.xml");
            using (var writer = new StreamWriter(filePath))
            {
                var serializer = new XmlSerializer(typeof(List<T>));
                serializer.Serialize(writer, entities);
                writer.Flush();
            }
        }

        public IList<T> Set<T>()
            where T : class
        {
            string filePath = Path.Combine(_xmlFileLocation, $"{typeof(T).Name}.xml");
            IList<T> entities = new List<T>();
            if (File.Exists(filePath))
            {
                //XDocument doc = XDocument.Load(filePath);
                using (var stream = File.OpenRead(filePath))
                {
                    var serializer = new XmlSerializer(typeof(List<T>));
                    entities = serializer.Deserialize(stream) as List<T>;
                }
            }
            return entities.ToList();
        }
    }
}
