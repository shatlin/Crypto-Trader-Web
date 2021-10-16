using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TraderWeb.Shared;

namespace TraderWeb.Client.Services
{
    public class ConfigService : IConfigService
    {

        public List<Config> configs { get; set; } = new List<Config>();

        public HttpClient _httpClient { get; }

        public event Action OnChange;

        public ConfigService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Config>> GetConfigs()
        {
            configs= await _httpClient.GetFromJsonAsync<List<Config>>("api/configr");
            return configs;
        }
        public async Task<Config> GetSingleConfig(int id)
        {
            var test= await _httpClient.GetFromJsonAsync<Config>($"api/configr/{id}");
            return test;
        }

        public async Task<List<Config>> UpdateConfig(Config config,int id)
        {
            var result = await _httpClient.PutAsJsonAsync<Config>($"api/configr/{id}",config);
            configs=await result.Content.ReadFromJsonAsync<List<Config>>();
            OnChange.Invoke();
            return configs;
        }

    }
}
