using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RadioSearcher.Models.Domain;
using RadioSearcher.Tools.Parsers;

namespace RadioSearcher.Tools
{
    public class ParseController
    {
        public BelChipParser BelChip = new BelChipParser();
        public ChipDipParser ChipDip = new ChipDipParser();
        private int _entryCounter;

        public int GetCount(string request, string belchip, string chipdip)
        {
            _entryCounter += belchip == "" ? BelChip.GetCount(request) : 0;
            _entryCounter += chipdip == "" ? ChipDip.GetCount(request) : 0;
            return _entryCounter;
        }

        public List<Product> Parse(string request, int offset,int belchip, int chipdip)
        {
            var products = new List<Product>();
            if (belchip != 0 && belchip > offset)
            {
                products.AddRange(BelChip.Parse(request, offset));
            }
            else if (chipdip != 0 && chipdip + belchip > offset)
            {
                products.AddRange(ChipDip.Parse(request, offset - belchip));
            }
            return products;
        }
    }
}