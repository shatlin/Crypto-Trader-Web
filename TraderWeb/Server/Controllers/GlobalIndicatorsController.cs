using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TraderWeb.Server.Data;
using TraderWeb.Shared;

namespace TraderWeb.Server.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class GlobalIndicatorController : ControllerBase
    {

        private readonly ILogger<GlobalIndicatorController> _logger;
        private readonly DB _db;
        public GlobalIndicatorController(ILogger<GlobalIndicatorController> logger, DB db)
        {
            _logger = logger;
            _db = db;
        }
        public List<MyCoins> allcoins=new List<MyCoins>();

        public GlobalSignal globalSignal = new GlobalSignal();

        private async Task<GlobalSignal> GetGlobalIndicators()
        {
            allcoins=await _db.MyCoins.Where(x=>x.IsIncludedForTrading == true && x.Rank >0 && x.Rank<50).AsNoTracking().ToListAsync();

            List<string> allPairs = allcoins.Select(x => x.Pair).ToList();

            List<SignalCandle> allSignals = await _db.SignalCandle.Where(x => allPairs.Contains(x.Pair)).AsNoTracking().OrderByDescending(x => x.CloseTime).ToListAsync();

            CalculateGlobalIndicators(allSignals);
         
            return globalSignal;

        }

        private GlobalSignal CalculateGlobalIndicators(List<SignalCandle> allSignals)
        {

            decimal avgPriceChange=0;
            decimal avgPriceChangethirtyMins = 0;
            decimal GoingUpCount=0;
            foreach (var coin in allcoins)
            {
                try
                {

                    var AllOneMin = allSignals.Where(x => x.Pair == coin.Pair && x.CandleType == "1min");
                    var currentPrice = AllOneMin.First().ClosePrice;

                    var isOneOnUpTrend = currentPrice >= AllOneMin.Max(x => x.ClosePrice);

                    if(isOneOnUpTrend) GoingUpCount++;

                    var AllFiveMin = allSignals.Where(x => x.Pair == coin.Pair && x.CandleType == "5min");
                    avgPriceChange += ((currentPrice - AllFiveMin.Last().ClosePrice) / AllFiveMin.Last().ClosePrice) * 100;

                    avgPriceChangethirtyMins += ((currentPrice - AllOneMin.Last().ClosePrice) / AllOneMin.Last().ClosePrice) * 100;

                    if (coin.Pair == "BTCUSDT")
                    { 
                        globalSignal.BitCoinPriceChange= ((currentPrice - AllFiveMin.Last().ClosePrice) / AllFiveMin.Last().ClosePrice) * 100;
                        globalSignal.BitCoinPriceChangeThirtyMins = ((currentPrice - AllOneMin.Last().ClosePrice) / AllOneMin.Last().ClosePrice) * 100;
                    }
                }
                catch
                {
                  
                }
            }

            avgPriceChange= avgPriceChange/allcoins.Count;
            avgPriceChangethirtyMins=avgPriceChangethirtyMins/allcoins.Count;
            globalSignal.AveragePriceChangeThirtyMins=avgPriceChangethirtyMins;
            globalSignal.AveragePriceChange=avgPriceChange;
            globalSignal.IsMarketOnUpTrendToday= avgPriceChange > 1;
            globalSignal.IsMarketOnDownTrendToday = avgPriceChange <= 0;

            globalSignal.IsBitCoinGoingUpToday = globalSignal.BitCoinPriceChange > 1;
            globalSignal.IsBitCoinGoingDownToday = globalSignal.BitCoinPriceChange <= 0;

            globalSignal.AreMostCoinsGoingUpNow   =    GoingUpCount > (allcoins.Count/2);
            globalSignal.AreMostCoinsGoingDownNow = GoingUpCount < (allcoins.Count / 2);
            return globalSignal;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var res= await GetGlobalIndicators();
            return Ok(res);
        }

    }
}
