using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GeoPaint.DTO;
using GeoPaint.Engine.Contracts;

namespace GeoWeb.ServiceHost.Controllers
{
    //[Authorize]
    public class ValuesController : BaseController
    {

        [Import]
        IBusinessEngineFactory _EngineFactory;

        public ValuesController()
        {
        }


        public ValuesController(IBusinessEngineFactory engineFactory)
        {
            _EngineFactory = engineFactory;
        }
        [HttpPost]
        public int Create(CreateComplexShapeRequestDTO complexShapeDto)
        {
            var shapeEngine = _EngineFactory.GetBusinessEngine<IShapeEngine>();
            return shapeEngine.Create(complexShapeDto);
        }
    }
}
