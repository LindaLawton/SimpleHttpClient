using System.Threading.Tasks;

namespace HttpClientTesting.Service
{
    public interface ISimpleService
    {
        Task<string> GetData();
    }
}