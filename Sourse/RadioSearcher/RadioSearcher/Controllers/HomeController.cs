using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RadioSearcher.Models.Domain;
using RadioSearcher.Models.Presentation;
using RadioSearcher.Tools;
using RadioSearcher.Tools.Parsers;

namespace RadioSearcher.Controllers
{
    public class HomeController : Controller
    {
        private readonly ParseController _parseController = new ParseController();
        public ActionResult Index()
        {
            Session["offset"] = 0;
            return View(new IndexModel());
        }

        [HttpPost]
        public ActionResult Index(FormCollection form, string request, string belchip, string chipdip)
        {
            if (Session["request"] != null || request!=null)
            {
                if (form.Count != 0)
                {
                    Session["count"] = _parseController.GetCount(request, belchip, chipdip);
                    Session["request"] = request;
                    Session["offset"] = 0;
                    Session["belchipCount"] = belchip == "" ? _parseController.BelChip.GetCount(request) : 0;
                    Session["chipdipCount"] = chipdip == "" ? _parseController.ChipDip.GetCount(request) : 0;
                }
                List<Product> products = new List<Product>();
                if ((int)Session["offset"] < (int)Session["count"])
                {
                    products.AddRange(_parseController.Parse((string)Session["request"], (int)Session["offset"],
                        (int)Session["belchipCount"], (int)Session["chipdipCount"]));
                    Session["offset"] = (int)Session["offset"] + 10;
                }
                if (form.Count != 0)
                {
                    return View(new IndexModel
                    {
                        Products = products
                    });
                }
                return Json(new IndexModel
                {
                    Products = products
                });
            }
            return Json(new IndexModel());
        }
    }
}