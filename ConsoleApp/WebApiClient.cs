using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class WebApiClient : IDisposable
    {
        private HttpClient _client;
        private JsonSerializerOptions JsonSerializerSettings { get; set; } = new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.Preserve, PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        public WebApiClient(string baseAddress)
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri(baseAddress)
            };
            _client.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
        }

        public void Dispose()
        {
            _client.Dispose();
        }
        public async Task<T?> GetAsync<T>(string request)
        {
            var response = await _client.GetAsync(request);
            //response.EnsureSuccessStatusCode();
            if (!response.IsSuccessStatusCode)
                return default;

            return await response.Content.ReadFromJsonAsync<T?>();
        }
        public async Task<T?> PostAsync<T>(string request, T payload)
        {
            var response = await _client.PostAsJsonAsync(request, payload);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<T?>();
        }


    }
}
