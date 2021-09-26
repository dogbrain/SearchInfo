using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine
{
    public static class StringExtensions
    {
        public static IEnumerable<string> SplitOnSpace(this string text) => text.Split(" ");
        public static IEnumerable<string> SplitOnComma(this string text) => text.Split(",");
        public static IEnumerable<string> TrimList(this IEnumerable<string> strings) => strings.Select(x=>x.Trim());
        public static IEnumerable<string> SplitOnSpaceAndTrimString(this string text) => text.SplitOnSpace().TrimList();
        public static IEnumerable<string> SplitOnCommaAndTrimString(this string text) => text.SplitOnComma().TrimList();
    }
}
