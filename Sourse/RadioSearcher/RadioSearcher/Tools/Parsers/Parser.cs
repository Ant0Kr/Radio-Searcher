using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RadioSearcher.Models.Domain;

namespace RadioSearcher.Tools.Parsers
{
    interface IParser
    {
        List<Product> Parse(string request, int offset, int size);
        int GetCount(string request);
    }
}
