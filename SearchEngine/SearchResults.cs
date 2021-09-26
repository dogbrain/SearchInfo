using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine
{
    public record SearchResults
    {
        public string Provider { get; internal set; }
        public long NumberOfResults { get; internal set; }
    }
}