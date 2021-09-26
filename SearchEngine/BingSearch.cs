
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SearchEngine
{
    public class BingSearch : ISearchProvider
    {
        public string ProviderName => "Bing";

        const string _subscriptionKey = "";
        const string uriBase = "https://api.bing.microsoft.com";
        static string path = "/v7.0/search";

        public async Task<long> NumberOfHits(string query, CancellationToken cancellationToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _subscriptionKey);

            try
            {
                var result = await client.GetAsync($"{uriBase}{path}?q={query}&responseFilter=Webpages&answerCount=1&count=0", cancellationToken);
                var jsonString = await result.Content.ReadAsStringAsync(cancellationToken);
                var bingReponse =
                   JsonSerializer.Deserialize<BingResponse>(jsonString);
                return bingReponse.webPages.totalEstimatedMatches;
            }
            catch
            {
                //Implement proper retry or error handeling
                throw;
            }
            return -1;
        }
    }
}
