using System.Collections.Generic;
using RadioSearcher.Models.Domain;

namespace RadioSearcher.Tools.Parsers
{
    interface IParser
    {
        List<Product> Parse(string request, int offset, int size);
        int GetCount(string request);
    }
}
