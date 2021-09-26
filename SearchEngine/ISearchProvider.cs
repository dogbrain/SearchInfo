using System.Threading;
using System.Threading.Tasks;

namespace SearchEngine
{
    public interface ISearchProvider
    {
        public string ProviderName { get; }
        Task<long> NumberOfHits(string query, CancellationToken cancellationToken);
    }
}