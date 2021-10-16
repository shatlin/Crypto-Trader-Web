using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraderWeb.Server.Data;
using TraderWeb.Shared;

namespace TraderWeb.Server.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CoinPriceController : ControllerBase
    {

        private readonly ILogger<CoinPriceController> _logger;
        private readonly DB _db;

        public CoinPriceController(ILogger<CoinPriceController> logger, DB db)
        {
            _logger = logger;
            _db = db;
        }

        private async Task<List<CoinPrice>> AllCoinPrice()
        {
            List<string> allTradingPairs =
                await _db.MyCoins.AsNoTracking().Where(x => x.IsIncludedForTrading == true).OrderBy(x => x.Pair).Select(x => x.Pair).ToListAsync();
            List<SignalCandle> allSignals = await _db.SignalCandle.AsNoTracking().Where(x => allTradingPairs.Contains(x.Pair)).OrderByDescending(x => x.CloseTime).ToListAsync();
            List<CoinPrice> coinPrices = GetCoinPrices(allTradingPairs, allSignals);

            return coinPrices;

        }

        private List<CoinPrice> GetCoinPrices(List<string> allTradingPairs, List<SignalCandle> allSignals)
        {
            List<CoinPrice> coinPrices = new List<CoinPrice>();
            CoinPrice coinprice = new CoinPrice();

            foreach (string pair in allTradingPairs)
            {

                try
                {
                    coinprice = new CoinPrice();

                    var OneMin = allSignals.Where(x => x.Pair == pair && x.CandleType == "1min").OrderByDescending(x => x.CloseTime);
                    var FiveMin = allSignals.Where(x => x.Pair == pair && x.CandleType == "5min").OrderByDescending(x => x.CloseTime).Take(12);
                    var FifteenMin = allSignals.Where(x => x.Pair == pair && x.CandleType == "15min").OrderByDescending(x => x.CloseTime);
                    var ThirtyMin = allSignals.Where(x => x.Pair == pair && x.CandleType == "30min").OrderByDescending(x => x.CloseTime);
                    var OneHour = allSignals.Where(x => x.Pair == pair && x.CandleType == "1hour").OrderByDescending(x => x.CloseTime);
                    var FourHour = allSignals.Where(x => x.Pair == pair && x.CandleType == "4hour").OrderByDescending(x => x.CloseTime);
                    var OneDay = allSignals.Where(x => x.Pair == pair && x.CandleType == "day").Take(7).OrderByDescending(x => x.CloseTime);

                    var Last5OneMins = OneMin.Take(5);

                    coinprice.Pair = OneMin.First().Pair;
                    coinprice.ClosePrice= OneMin.First().ClosePrice;
                    coinprice.DayMin = OneHour.Min(x => x.ClosePrice);
                    coinprice.DayMax = OneHour.Max(x => x.ClosePrice);


                    coinprice.TotalOneMinUps = OneMin.Count(x => x.UpOrDown == "up");
                    coinprice.TotalOneMinDowns = OneMin.Count(x => x.UpOrDown == "down");
                    coinprice.OneMinPriceChange = ((OneMin.First().ClosePrice - OneMin.Last().ClosePrice) / OneMin.Last().ClosePrice) * 100;
                    coinprice.IsLast5OneMinOnDownTrend = Last5OneMins.First().ClosePrice < Last5OneMins.Last().ClosePrice;
                    coinprice.IsOneMinOnDownTrend = OneMin.First().ClosePrice < OneMin.Last().ClosePrice;

                    coinprice.TotalFiveMinUps = FiveMin.Count(x => x.UpOrDown == "up");
                    coinprice.TotalFiveMinDowns = FiveMin.Count(x => x.UpOrDown == "down");
                    coinprice.FiveMinPriceChange = ((FiveMin.First().ClosePrice - FiveMin.Last().ClosePrice) / FiveMin.Last().ClosePrice) * 100;
                    coinprice.IsLast5FiveMinOnDownTrend = FiveMin.Take(5).First().ClosePrice < FiveMin.Take(5).Last().ClosePrice;
                    coinprice.IsFiveMinOnDownTrend = FiveMin.First().ClosePrice < FiveMin.Last().ClosePrice;

                    coinprice.TotalFifteenMinUps = FifteenMin.Count(x => x.UpOrDown == "up");
                    coinprice.TotalFifteenMinDowns = FifteenMin.Count(x => x.UpOrDown == "down");
                    coinprice.FifteenMinPriceChange = ((FifteenMin.First().ClosePrice - FifteenMin.Last().ClosePrice) / FifteenMin.Last().ClosePrice) * 100;
                    coinprice.IsLast5FifteenMinOnDownTrend = FifteenMin.Take(5).First().ClosePrice < FifteenMin.Take(5).Last().ClosePrice;
                    coinprice.IsFifteenMinOnDownTrend = FifteenMin.First().ClosePrice < FifteenMin.Last().ClosePrice;

                    coinprice.TotalThirtyMinUps = ThirtyMin.Count(x => x.UpOrDown == "up");
                    coinprice.TotalThirtyMinDowns = ThirtyMin.Count(x => x.UpOrDown == "down");
                    coinprice.ThirtyMinPriceChange = ((ThirtyMin.First().ClosePrice - ThirtyMin.Last().ClosePrice) / ThirtyMin.Last().ClosePrice) * 100;
                    coinprice.IsLast5ThirtyMinOnDownTrend = ThirtyMin.Take(5).First().ClosePrice < ThirtyMin.Take(5).Last().ClosePrice;
                    coinprice.IsThirtyMinOnDownTrend = ThirtyMin.First().ClosePrice < ThirtyMin.Last().ClosePrice;

                    coinprice.TotalOneHourUps = OneHour.Count(x => x.UpOrDown == "up");
                    coinprice.TotalOneHourDowns = OneHour.Count(x => x.UpOrDown == "down");
                    coinprice.OneHourPriceChange = ((OneHour.First().ClosePrice - OneHour.Last().ClosePrice) / OneHour.Last().ClosePrice) * 100;
                    coinprice.IsLast5OneHourOnDownTrend = OneHour.Take(5).First().ClosePrice < OneHour.Take(5).Last().ClosePrice;
                    coinprice.IsOneHourOnDownTrend = OneHour.First().ClosePrice < OneHour.Last().ClosePrice;

                    coinprice.TotalFourHourUps = FourHour.Count(x => x.UpOrDown == "up");
                    coinprice.TotalFourHourDowns = FourHour.Count(x => x.UpOrDown == "down");
                    coinprice.FourHourPriceChange = ((FourHour.First().ClosePrice - FourHour.Last().ClosePrice) / FourHour.Last().ClosePrice) * 100;
                    coinprice.IsLast5FourHourOnDownTrend = FourHour.Take(5).First().ClosePrice < FourHour.Take(5).Last().ClosePrice;
                    coinprice.IsFourHourOnDownTrend = FourHour.First().ClosePrice < FourHour.Last().ClosePrice;

                    coinprice.TotalDayUps = OneDay.Count(x => x.UpOrDown == "up");
                    coinprice.TotalDayDowns = OneDay.Count(x => x.UpOrDown == "down");
                    coinprice.DayPriceChange = ((OneDay.First().ClosePrice - OneDay.Last().ClosePrice) / OneDay.Last().ClosePrice) * 100;
                    coinprice.IsLast5DayOnDownTrend = OneDay.Take(5).First().ClosePrice < OneDay.Take(5).Last().ClosePrice;
                    coinprice.IsDayOnDownTrend = OneDay.First().ClosePrice < OneDay.Last().ClosePrice;

                    coinprice.PriceChangeNumber =
                        coinprice.DayPriceChange +
                        coinprice.FourHourPriceChange +
                        coinprice.OneHourPriceChange +
                        coinprice.ThirtyMinPriceChange +
                        coinprice.FifteenMinPriceChange +
                        coinprice.FiveMinPriceChange +
                        coinprice.OneMinPriceChange;

                    coinPrices.Add(coinprice);
                }
                catch
                {
 
                }
            }
            return coinPrices.OrderBy(x=>x.PriceChangeNumber).ToList();
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await AllCoinPrice());
        }

    }
}
