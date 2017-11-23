using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;
using RadioSearcher.Models.Domain;

namespace RadioSearcher.Tools.Parsers
{
    public class ChipDipParser : IParser
    {
        private const string ChipdipUrl = "https://www.chipdip.ru/search?searchtext=";
        public int Count { get; set; }

        public List<Product> Parse(string request, int offset)
        {
            return new List<Product>();
        }

        public int GetCount(string request)
        {
            var page = new HtmlWeb().Load(ChipdipUrl + request).DocumentNode
                .SelectSingleNode("//div[@id='search_results']//div[@class='serp__group-section']");
            return page == null ? 0 : int.Parse(page.SelectSingleNode("sub").InnerText);
        }
    }
}