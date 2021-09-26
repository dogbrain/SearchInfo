using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SearchEngine.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly ILogger<SearchController> _logger;
        private readonly IMultiSearch multiSearch;

        public SearchController(ILogger<SearchController> logger, IMultiSearch multiSearch)
        {
            _logger = logger;
            this.multiSearch = multiSearch;
        }

        [HttpGet]
        public async Task<IEnumerable<SearchResults>> Get(string query, CancellationToken cancellationToken, string providers = null)
        {
            return await multiSearch.MultiProviderSearch(query, cancellationToken, providers?.SplitOnCommaAndTrimString());
        }

        [HttpGet]
        [Route("providers")]
        public async Task<IEnumerable<string>> GetProviders(CancellationToken cancellationToken)
        {
            return multiSearch.GetProviderNames;
        }
    }
}
