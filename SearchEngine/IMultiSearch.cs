using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SearchEngine
{
    public interface IMultiSearch
    {
        Task<IEnumerable<SearchResults>> MultiProviderSearch(string query, CancellationToken cancellationToken, IEnumerable<string> providerNames = null);
        IEnumerable<string> GetProviderNames { get; }
    }
}