using System;
using System.Collections.Immutable;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using HttpClientTesting.Client;
using Microsoft.VisualBasic;

namespace HttpClientTesting.Service
{
    public class SimpleServiceResponse
    {
        public DateTime ResponseDate { get; set; }
        public bool Success { get; set; }
        public HttpResponseMessage Message { get; set; }

        public static SimpleServiceResponse Build(HttpResponseMessage message)
        {
            return new SimpleServiceResponse()
            {
                ResponseDate = DateTime.UtcNow,
                Success = message.IsSuccessStatusCode,
                Message = message
            };
        }
    }
    
    public class SimpleService : ISimpleService
    {
        private readonly ISimpleHttpClient _client;

        public SimpleService(ISimpleHttpClient client)
        {
            _client = client;
        }

        public async Task<SimpleServiceResponse> GetDataAsync()
        {
            try
            {
                var response = await _client.GetAsync();

                return SimpleServiceResponse.Build(response);
                
                
                
            }
            catch (HttpRequestException e)
            {
                var response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                response.Content = new StringContent($"Internal error: {e.Message}");
                return SimpleServiceResponse.Build(response);
            }
        }
    }
}