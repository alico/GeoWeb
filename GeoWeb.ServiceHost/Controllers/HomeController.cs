using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GeoPaint.Component;
using GeoPaint.DTO;
using GeoPaint.Engine.Contracts;

namespace GeoWeb.ServiceHost.Controllers
{
    public class HomeController : BaseController2
    {

        [Import]
        IBusinessEngineFactory _EngineFactory;

        public HomeController()
        {
        }

        public HomeController(IBusinessEngineFactory engineFactory)
        {
            _EngineFactory = engineFactory;
        }


        /// <summary>
        /// Loads images and lists images
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Image Id</returns>
        public ActionResult Index(int id = 0)
        {
            var shapeEngine = _EngineFactory.GetBusinessEngine<IShapeEngine>();
            var html = string.Empty;

            if (id > 0)
            {
                var data = shapeEngine.Get(id);
                html = data.Render();
            }

            var dto = shapeEngine.GetList();

            ViewBag.RenderHtmlContent = html;
            ViewBag.Title = "Load Image";

            return View(dto);
        }

        /// <summary>
        /// Prepares view for creating image
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.RenderHtmlContent = "";
            return View();
        }

        /// <summary>
        /// Creates image 
        /// </summary>
        /// <param name="complexShapeDto">Image data</param>
        /// <returns>Image Id</returns>
        [HttpPost]
        public JsonResult Create(CreateComplexShapeRequestDTO complexShapeDto)
        {
            var shapeEngine = _EngineFactory.GetBusinessEngine<IShapeEngine>();
            var id = shapeEngine.Create(complexShapeDto);

            return Json(id, JsonRequestBehavior.AllowGet);
        }
    }
}
