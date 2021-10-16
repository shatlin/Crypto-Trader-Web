using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TraderWeb.Shared;

namespace TraderWeb.Client.Services
{
    public class SignalService : ISignalService
    {

        public List<SignalCandle> Signals { get; set; } = new List<SignalCandle>();
        public List<string> Pairs { get; set; } = new List<string>();
        public HttpClient _httpClient { get; }


        public SignalService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<String>> GetPairs()
        {
            Pairs = await _httpClient.GetFromJsonAsync<List<string>>("api/Signal");
            return Pairs;
        }

        public async Task<List<SignalCandle>> GetSignals(string pair)
        {
            var sigs = await _httpClient.GetFromJsonAsync<List<SignalCandle>>($"api/Signal/{pair}");
            return sigs;
        }

        public async Task<List<CoinPrice>> GetCoinPrices()
        {
            var coinprices = await _httpClient.GetFromJsonAsync<List<CoinPrice>>($"api/CoinPrice");
            return coinprices;
        }

        public async Task<List<CoinPrice>> GetBuyables()
        {
            var coinprices = await _httpClient.GetFromJsonAsync<List<CoinPrice>>($"api/Buy/GetBuyDecisions");
            return coinprices;
        }

        public async Task<List<CoinPrice>> GetSurgers()
        {
            var coinprices = await _httpClient.GetFromJsonAsync<List<CoinPrice>>($"api/Buy/Surgers");
            return coinprices;
        }

        public async Task<GlobalSignal> GetGlobalSignal()
        {
            var sigs = await _httpClient.GetFromJsonAsync<GlobalSignal>($"api/GlobalIndicator");
            return sigs;
        }

        public async Task<MyCoins> MarkCoinToBuy(string pair)
        {
            var result = await _httpClient.PutAsJsonAsync<string>($"api/Buy/MarkToBuy/{pair}", pair);
            var mycoin = await result.Content.ReadFromJsonAsync<MyCoins>();
            return mycoin;
        }

        public async Task<MyCoins> CancelBuy(string pair)
        {
            var result = await _httpClient.PutAsJsonAsync<string>($"api/Buy/CancelBuy/{pair}", pair);
            var mycoin = await result.Content.ReadFromJsonAsync<MyCoins>();
            return mycoin;
        }

    }
}
