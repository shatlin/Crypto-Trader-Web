using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TraderWeb.Shared;

namespace TraderWeb.Client.Services
{
    public class PlayerService : IPlayerService
    {

        public List<Player> Players { get; set; } = new List<Player>();

        public HttpClient _httpClient { get; }

        public event Action OnChange;

        public PlayerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

       

        public async Task<List<Player>> GetPlayers()
        {
            Players= await _httpClient.GetFromJsonAsync<List<Player>>("api/Player");
            return Players;
        }
        public async Task<Player> GetSinglePlayer(int id)
        {
            var test= await _httpClient.GetFromJsonAsync<Player>($"api/Player/{id}");
            return test;
        }

        public async Task<List<Player>> UpdatePlayer(Player Player,int id)
        {
            var result = await _httpClient.PutAsJsonAsync<Player>($"api/Player/{id}",Player);
            Players=await result.Content.ReadFromJsonAsync<List<Player>>();
            OnChange.Invoke();
            return Players;
        }

        public async Task<Player> SellPlayer(string playername)
        {
            var result = await _httpClient.PutAsJsonAsync<string>($"api/Player/SellPlayer/{playername}", playername);
            var player = await result.Content.ReadFromJsonAsync<Player>();
            return player;
        }


    }
}
