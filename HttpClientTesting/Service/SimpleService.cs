using System.Net.Http;
using System.Threading.Tasks;
using HttpClientTesting.Client;

namespace HttpClientTesting.Service
{
    public class SimpleService : ISimpleService
    {
        private readonly ISimpleHttpClient _client;

        public SimpleService(ISimpleHttpClient client)
        {
            _client = client;
        }

        public async Task<string> GetData()
        {
            try
            {
                var response = await _client.GetAsync();
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();

                return responseBody;
            }
            catch (HttpRequestException e)
            {
                return e.Message;
            }
        }
    }
}