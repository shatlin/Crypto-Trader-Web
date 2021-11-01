using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraderWeb.Shared;

namespace TraderWeb.Client.Services
{
    public interface ISignalService
    {
        List<SignalCandle> Signals { get; set; }
        List<string> Pairs { get; set; }
        Task<List<string>> GetPairs();
        Task<List<SignalCandle>> GetSignals(string pair);
        Task<List<MyCoins>> GetCoinPrices();
        Task<List<MyCoins>> GetBuyables();
        Task<List<MyCoins>> GetSurgers();
        Task<MyCoins> MarkCoinToBuy(string pair);
        Task<MyCoins> CancelBuy(string pair);
        Task<MyCoins> IncludeForTrading(string pair);
        Task<MyCoins> ExcludeFromTrading(string pair);
        Task<GlobalSignal> GetGlobalSignal();

    }
}
