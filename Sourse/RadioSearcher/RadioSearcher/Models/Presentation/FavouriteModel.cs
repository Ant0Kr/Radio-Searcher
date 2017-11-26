using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RadioSearcher.Models.Domain;

namespace RadioSearcher.Models.Presentation
{
    public class FavouriteModel
    {
        public List<Product> Products { get; set; }

        public FavouriteModel()
        {
            Products = new List<Product>();
        }
    }
}