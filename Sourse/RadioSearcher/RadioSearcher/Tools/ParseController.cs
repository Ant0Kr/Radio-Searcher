using System.Collections.Generic;
using RadioSearcher.Models.Domain;
using RadioSearcher.Tools.Parsers;

namespace RadioSearcher.Tools
{
    public class ParseController
    {
        private readonly BelChipParser _belChip = new BelChipParser();
        private readonly RadioShopParser _radioShop = new RadioShopParser();

        public List<Product> Parse(string request, int offset, int size, int belchip, int radioshop)
        {
            var products = new List<Product>();
            if (belchip != 0 && belchip > offset)
            {
                products.AddRange(_belChip.Parse(request, offset, size));
            }
            else if (radioshop != 0 && radioshop + belchip > offset)
            {
                products.AddRange(_radioShop.Parse(request, offset - belchip, size));
            }
            if (belchip - offset < size && belchip > offset && radioshop != 0)
            {
                products.AddRange(_radioShop.Parse(request, 0, size - belchip + offset));
            }
            return products;
        }

        public int GetBelChipCount(string request)
        {
            return _belChip.GetCount(request);
        }

        public int GetRadioShopCount(string request)
        {
            return _radioShop.GetCount(request);
        }
    }
}