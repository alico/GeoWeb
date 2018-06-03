using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GeoPaint.Common;
using System.Web.Http;
using GeoWeb.MEF;

namespace GeoWeb.Bootstrapper
{
    public static class MEFConfig
    {
        public static void RegisterMEF()
        {
            ICollection<ComposablePartCatalog> catalogPart = new List<ComposablePartCatalog>();
            catalogPart.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));

            ObjectBase.Container = MEFLoader.Init(catalogPart);
            GlobalConfiguration.Configuration.DependencyResolver = new MefAPIDependencyResolver(ObjectBase.Container);
        }
    }
}
