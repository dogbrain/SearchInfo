using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine
{
    public class WebPages
    {
        public long totalEstimatedMatches { get; set; }
    }

    public class BingResponse
    {
        public WebPages webPages { get; set; }
    }
}


