using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using RadioSearcher.Models.Domain;
using RadioSearcher.Models.Presentation;
using RadioSearcher.Tools;
using RadioSearcher.Tools.Parsers;

namespace RadioSearcher.Controllers
{
    public class HomeController : Controller
    {
        private readonly ParseController _parseController = new ParseController();
        private const int PageSize = 20;

        public ActionResult Index()
        {
            return View(new IndexModel());
        }

        [HttpPost]
        public ActionResult Index(FormCollection form, string request, string belchip, string radioshop)
        {
            Session["offset"] = 0;
            Session["request"] = request;
            Session["belchipCount"] = belchip == "" ? _parseController.GetBelChipCount(request) : 0;
            Session["radioshopCount"] = radioshop == "" ? _parseController.GetRadioShopCount(request) : 0;
            Session["count"] = (int)Session["belchipCount"] + (int)Session["radioshopCount"];
            List<Product> products = new List<Product>();
            if ((int)Session["offset"] < (int)Session["count"])
            {
                products.AddRange(GetProductRange());
            }
            return View(new IndexModel
            {
                Products = products
            });

        }

        public ActionResult GetProductsJson()
        {
            if (Session["request"] != null)
            {
                List<Product> products = new List<Product>();
                if ((int)Session["offset"] < (int)Session["count"])
                {
                    products.AddRange(GetProductRange());
                }
                return Json(new IndexModel
                {
                    Products = products
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new IndexModel(), JsonRequestBehavior.AllowGet);
        }
 

        private List<Product> GetProductRange()
        {
            List<Product> products = _parseController.Parse((string)Session["request"], (int)Session["offset"], PageSize,
                (int)Session["belchipCount"], (int)Session["radioshopCount"]);
            Session["offset"] = (int)Session["offset"] + products.Count;
            return products;
        }
    }
}