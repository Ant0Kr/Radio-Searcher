using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;
using RadioSearcher.Models.Domain;

namespace RadioSearcher.Tools.Parsers
{
    public class BelChipParser : IParser
    {
        private const string BelchipUrl = "http://belchip.by/";


        public List<Product> Parse(string request, int offset)
        {
            var products = new List<Product>();
            var page = new HtmlWeb().Load($"{BelchipUrl}search/?query={request}").DocumentNode.SelectNodes("//div[@class='cat-item']");
            for (var i = offset; i < page.Count && i < offset + 10; i++)
            {
                products.Add(new Product
                {
                    Name = page[i].SelectSingleNode(".//h3//a").InnerText,
                    ImgUrl = string.Format(BelchipUrl + page[i].SelectSingleNode(".//div[@class='cat-pic']//a[@class='product-image']//img").Attributes["src"].Value)
                });
            }
            return products;
        }

        public int GetCount(string request)
        {
            var page = new HtmlWeb().Load($"{BelchipUrl}search/?query={request}").DocumentNode.SelectNodes("//div[@class='cat-item']");
            return page?.Count ?? 0;
        }
    }
}