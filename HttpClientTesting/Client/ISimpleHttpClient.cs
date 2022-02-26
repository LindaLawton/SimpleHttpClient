using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientTesting.Client
{
    public interface ISimpleHttpClient
    {
        Task<HttpResponseMessage> GetAsync();
    }
}