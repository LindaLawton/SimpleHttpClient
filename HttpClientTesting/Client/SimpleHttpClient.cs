using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientTesting.Client
{
    public class SimpleHttpClient : ISimpleHttpClient
    {
        private static HttpClient _client;

        private const string Path = "discovery/v1/apis";

        public SimpleHttpClient(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("discovery");;
        }
        
        public async Task<HttpResponseMessage> GetAsync()
        {
            return await _client.GetAsync(Path);
        }
    }
}