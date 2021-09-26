using Google.Apis.Customsearch.v1;
using Google.Apis.Services;
using System.Threading;
using System.Threading.Tasks;

namespace SearchEngine
{
    public class GoogleSearch : ISearchProvider
    {
        private const string apiKey = "";
        private const string searchEngine = "";

        public string ProviderName => "Google";
        
        public async Task<long> NumberOfHits(string query, CancellationToken cancellationToken)
        {
            var customSearchService = new CustomsearchService(new BaseClientService.Initializer { ApiKey = apiKey });
            var listRequest = customSearchService.Cse.List();
            listRequest.Cx = searchEngine;
            listRequest.Q = query;
            try
            {
                var response = await listRequest.ExecuteAsync(cancellationToken);
                long result;
                if (long.TryParse(response.SearchInformation.TotalResults, out result)) return result;
            }
            
            catch
            {
                //Implement proper retry or error handeling
                
            }
            return -1;
        }
    }
}
