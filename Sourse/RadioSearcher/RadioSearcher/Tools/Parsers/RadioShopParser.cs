using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;
using RadioSearcher.Models.Domain;

namespace RadioSearcher.Tools.Parsers
{
    public class RadioShopParser : IParser
    {
        private const string RadioshopUrl = "http://radioshop.by/";

        public List<Product> Parse(string request, int offset, int size)
        {
            var products = new List<Product>();
            var page = new HtmlWeb().Load($"{RadioshopUrl}search.php?searchid=2193926&text={request}&web=0").DocumentNode
                .SelectNodes("//div[@style='text-indent:6px;']");
            for (var i = offset; i < page.Count && i < offset + size; i++)
            {
                var imgUrl = page[i].SelectSingleNode(".//a//span//img");
                var img = imgUrl == null ? "https://mosaikweb.com/wp-content/plugins/lightbox/images/No-image-found.jpg" : imgUrl.Attributes["src"].Value;
                products.Add(new Product
                {
                    Name = page[i].SelectSingleNode(".//a").InnerText,
                    ImgUrl = img,
                    ProductLink = RadioshopUrl + page[i].SelectSingleNode(".//a").Attributes["href"].Value,
                    Cost = page[i].SelectSingleNode(".//i").InnerText.Substring(20),
                    IsAvailable = "Available"
                });
            }
            return products;
        }

        public int GetCount(string request)
        {
            HtmlNodeCollection page;
            try
            {
                page = new HtmlWeb().Load($"{RadioshopUrl}search.php?searchid=2193926&text={request}&web=0").DocumentNode
                    .SelectNodes("//div[@style='text-indent:6px;']");
            }
            catch (System.Net.WebException)
            {
                return -1;
            }
            return page?.Count ?? 0;
        }
    }
}