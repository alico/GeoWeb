using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using GeoPaint.Common;

namespace GeoWeb.ServiceHost.Controllers
{
    public class BaseController : ApiController
    {
        public BaseController()
        {
            if (ObjectBase.Container != null)
            {
                ObjectBase.Container.SatisfyImportsOnce(this);
            }

        }
    }
}