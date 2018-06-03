using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoPaint.Data;
using GeoPaint.Engine;

namespace GeoWeb.Bootstrapper
{
    public static class MEFLoader
    {
        public static CompositionContainer Init()
        {
            return Init(null);
        }

        public static CompositionContainer Init(ICollection<ComposablePartCatalog> catalogParts)
        {
            AggregateCatalog catalogs = new AggregateCatalog();
            RegisterCatalogRepository(catalogs);
            RegisterCatalogEngine(catalogs);

            if (catalogParts != null)
                foreach (var catalog in catalogParts)
                {
                    catalogs.Catalogs.Add(catalog);
                }

            CompositionContainer container = new CompositionContainer(catalogs, false);

            return container;
        }

        //private static void RegisterCatalogRepositories(AggregateCatalog catalog)
        //{
        //    catalog.Catalogs.Add(new AssemblyCatalog(typeof().Assembly)); //Service.dll
        //}

        private static void RegisterCatalogEngine(AggregateCatalog catalog)
        {
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(ShapeEngine).Assembly)); //Service.dll
        }

        private static void RegisterCatalogRepository(AggregateCatalog catalog)
        {
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(RepositoryBase).Assembly)); //Service.dll
        }
    }
}
