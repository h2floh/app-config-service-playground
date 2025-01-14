using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace MyDotnetCoreApp.Services
{
    public class FeatureFlagService
    {
        private readonly HttpClient _httpClient;
        private readonly string _configurationEndpoint;

        public FeatureFlagService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _configurationEndpoint = configuration["AzureConfiguration:Endpoint"];
        }

        public async Task<List<FeatureFlag>> GetFeatureFlagsAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _configurationEndpoint);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<FeatureFlag>>(jsonResponse);
        }
    }
}