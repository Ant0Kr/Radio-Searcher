using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RadioSearcher.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["Request"] == null)
            {
                return View();
            }
            else
            {
                return View();
            }
            
        }

        [HttpPost]
        public ActionResult Index(String request)
        {
            Session["Request"] = request;
            return View();
        }
    }
}