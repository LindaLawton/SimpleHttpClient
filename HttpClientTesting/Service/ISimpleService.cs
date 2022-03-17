using System.Threading.Tasks;

namespace HttpClientTesting.Service
{
    public interface ISimpleService
    {
        Task<SimpleServiceResponse> GetDataAsync();
    }
}