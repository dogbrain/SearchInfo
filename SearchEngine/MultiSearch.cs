using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SearchEngine
{
    public class MultiSearch : IMultiSearch
    {
        private readonly IEnumerable<ISearchProvider> providers;

        public MultiSearch(IEnumerable<ISearchProvider> providers)
        {
            this.providers = providers;
        }

        public async Task<IEnumerable<SearchResults>> MultiProviderSearch(string query, CancellationToken cancellationToken, IEnumerable<string> providerNames=null)
        {
            var searchTerms = query.SplitOnSpaceAndTrimString();
            var providersToUse = providerNames == null ? providers : providers.Where(x => providerNames.Contains(x.ProviderName)).ToList();
            var work = CreateMultipleSearchWork(searchTerms, providersToUse, cancellationToken).ToList();

            var workResult = await Task.WhenAll(work.Select(x => x.task));


            var result = providersToUse.Select(p => new SearchResults
            {
                Provider = p.ProviderName,
                NumberOfResults = work.Where(x => x.ProviderName == p.ProviderName).Sum(x => x.task.Result)
            });
            return result;
        }

        public IEnumerable<string> GetProviderNames => providers.Select(x => x.ProviderName);

        private IEnumerable<(string ProviderName, string searchTerm, Task<long> task)> CreateMultipleSearchWork(IEnumerable<string> searchTerms, IEnumerable<ISearchProvider> providersToUse, CancellationToken cancellationToken)
        {
            return searchTerms.SelectMany(searchTerm => providersToUse.Select(provider => CreateSearchWork(searchTerm, provider, cancellationToken))); ;
        }

        private (string ProviderName, string searchTerm, Task<long> task) CreateSearchWork(string searchTerm, ISearchProvider provider, CancellationToken cancellationToken)
        {
            return (provider.ProviderName, searchTerm, provider.NumberOfHits(searchTerm, cancellationToken));
        }
    }
}
