using System.Collections.Generic;
using System.Web.Mvc;
using RadioSearcher.Models.Domain;
using RadioSearcher.Models.Presentation;
using RadioSearcher.Tools;

namespace RadioSearcher.Controllers
{
    public class HomeController : Controller
    {
        private readonly ParseController _parseController = new ParseController();
        private const int PageSize = 20;
        private const string ConnectionError = "Bad internet Connection!";

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
            var isBadConnection = (int) Session["belchipCount"] == -1 || (int) Session["radioshopCount"] == -1;
            Session["badConnection"] = isBadConnection ? ConnectionError : null;
            if (isBadConnection)
            {
                return Index();
            }
            Session["count"] = (int)Session["belchipCount"] + (int)Session["radioshopCount"];
            var products = new List<Product>();
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