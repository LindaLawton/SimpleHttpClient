using System.Threading.Tasks;
using HttpClientTesting.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HttpClientTesting.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SimpleController : ControllerBase
    {
        private readonly ILogger<SimpleController> _logger;
        private readonly ISimpleService _simpleService;

        public SimpleController(ILogger<SimpleController> logger, ISimpleService simpleService)
        {
            _logger = logger;
            _simpleService = simpleService;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            var response = await _simpleService.GetDataAsync();

            return response.ResponseDate.ToString();
        }
    }
}