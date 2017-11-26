using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RadioSearcher.Models.Domain;
using RadioSearcher.Models.Presentation;
using RadioSearcher.Repository;

namespace RadioSearcher.Controllers
{
    public class FavouritesController : Controller
    {
        private readonly ProductRepository _db = new ProductRepository();

        public ActionResult Index()
        {
            FavouriteModel model = new FavouriteModel
            {
                Products = _db.GetList()
            };
            return View(model);
        }

        public ActionResult Post(string name, string productLink, string imgUrl, string cost, string isAvailable)
        {
            var product = _db.Create(new Product
            {
                Name = name,
                ProductLink = productLink,
                ImgUrl = imgUrl,
                Cost = cost,
                IsAvailable = isAvailable
            });
            return Json(product);
        }

        public ActionResult Delete(int id)
        {
            return Json(_db.Delete(id));
        }
    }
}