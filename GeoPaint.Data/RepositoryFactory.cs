using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoPaint.Data.Contexts;
using GeoPaint.Data.Contracts;
using GeoPaint.Data.Repositories;
using GeoPaint.Settings;
using Microsoft.Practices.Unity;

namespace GeoPaint.Data
{
    [Export(typeof(IRepositoryFactory))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class RepositoryFactory : IRepositoryFactory
    {
        public IRepository<T> GetRepository<T>() where T : Entities.EntityBase
        {
            UnityContainer _container = new UnityContainer();

            var _settings = ConnectionSettingsSingleton.GetConnectionSettings().Setting;

            _container.RegisterInstance(_settings);

            switch (_settings.DataProvider)
            {
                case DataProviderType.SqlServer:
                    _container.RegisterType<DbContext, GeoPaintSQLDbContext>();
                    _container.RegisterType(typeof(IRepository<>), typeof(GeoPaintSQLRepository<>));

                    break;

                case DataProviderType.XmlFileStore:
                    _container.RegisterType<GeoPaintXMLContext>();
                    _container.RegisterType(typeof(IRepository<>), typeof(GeoPaintXMLRepository<>));
                    break;

                default:
                    throw new Exception($"The DataProvider Type {_settings.DataProvider} is unknown");
            }
            return _container.Resolve<IRepository<T>>();
        }
    }
}
