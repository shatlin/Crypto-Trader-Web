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
    public class BuyController : ControllerBase
    {

        private readonly ILogger<BuyController> _logger;
        private readonly DB _db;
        public BuyController(ILogger<BuyController> logger, DB db)
        {
            _logger = logger;
            _db = db;
        }
        public List<MyCoins> allcoins=new List<MyCoins>();
        private async Task<List<CoinPrice>> AllCoinPrice()
        {

            allcoins = await _db.MyCoins.Where(x => x.IsIncludedForTrading == true).AsNoTracking().ToListAsync();

            List<string> allPairs = allcoins.Select(x => x.Pair).ToList();

            var watch = new Stopwatch();
            watch.Start();

            List<SignalCandle> allSignals = await _db.SignalCandle.Where(x => allPairs.Contains(x.Pair)).AsNoTracking().OrderByDescending(x => x.CloseTime).ToListAsync();

            watch.Stop();
            _logger.LogInformation("Total AllSignals load time " + watch.ElapsedMilliseconds);

            var watch2 = new Stopwatch();
            watch2.Start();
            List<CoinPrice> coinPrices = GetCoinPrices(allSignals);

            watch2.Stop();
            _logger.LogInformation("Total GetCoinPrices load time " + watch2.ElapsedMilliseconds);

            return coinPrices;

        }

        private List<CoinPrice> GetCoinPrices(List<SignalCandle> allSignals)
        {
            List<CoinPrice> coinPrices = new List<CoinPrice>();
            CoinPrice coinprice = new CoinPrice();

            foreach (var coin in allcoins)
            {
                try
                {
                    coinprice = new CoinPrice();

                    var AllOneMin = allSignals.Where(x => x.Pair == coin.Pair && x.CandleType == "1min");
                    var Fifteen_OneMin = AllOneMin.Take(15);
                    var Ten_OneMin = AllOneMin.Take(10);
                    var Five_OneMin = AllOneMin.Take(5);
                    var isOneOnUpTrend = Ten_OneMin.First().ClosePrice >= Ten_OneMin.Max(x => x.ClosePrice);

                    coinprice.Pair = Ten_OneMin.First().Pair;
                    coinprice.CoinRank=coin.Rank;
                    coinprice.ClosePrice = Ten_OneMin.First().ClosePrice;
                    coinprice.OneMinPriceChange = ((AllOneMin.First().ClosePrice - AllOneMin.Last().ClosePrice) / AllOneMin.Last().ClosePrice) * 100;

                    coinprice.Five_OneMinPriceChange = ((Five_OneMin.First().ClosePrice - Five_OneMin.Last().ClosePrice) / Five_OneMin.Last().ClosePrice) * 100;

                    coinprice.Ten_OneMinPriceChange = ((Ten_OneMin.First().ClosePrice - Ten_OneMin.Last().ClosePrice) / Ten_OneMin.Last().ClosePrice) * 100;

                    coinprice.Fifteen_OneMinPriceChange = ((Fifteen_OneMin.First().ClosePrice - Fifteen_OneMin.Last().ClosePrice) / Fifteen_OneMin.Last().ClosePrice) * 100;

                    var AllFiveMin = allSignals.Where(x => x.Pair == coin.Pair && x.CandleType == "5min");
                    var FiveMin = AllFiveMin.Take(5).ToList();
                    var isFiveOnUpTrend = FiveMin.First().ClosePrice >= FiveMin.Max(x => x.ClosePrice);
                    coinprice.DayMin = AllFiveMin.Min(x => x.ClosePrice);
                    coinprice.DayMax = AllFiveMin.Max(x => x.ClosePrice);

                    var FifteenMin = allSignals.Where(x => x.Pair == coin.Pair && x.CandleType == "15min").Take(5);
                    var isFifteenOnUpTrend = FifteenMin.First().ClosePrice >= FifteenMin.Max(x => x.ClosePrice);
                    coinprice.FifteenMinPriceChange = ((FifteenMin.First().ClosePrice - FifteenMin.Last().ClosePrice) / FifteenMin.Last().ClosePrice) * 100;

                    var ThirtyMin = allSignals.Where(x => x.Pair == coin.Pair && x.CandleType == "30min").Take(5);
                    var isThirtyOnUpTrend = ThirtyMin.First().ClosePrice >= ThirtyMin.Max(x => x.ClosePrice);

                    var OneHour = allSignals.Where(x => x.Pair == coin.Pair && x.CandleType == "1hour");
                    var FourOneHour = OneHour.Take(4);
                    bool isHourOnUpTrend=false;
                    if (OneHour.Any())
                    {
                        coinprice.OneHourPriceChange = ((OneHour.First().ClosePrice - OneHour.Last().ClosePrice) / OneHour.Last().ClosePrice) * 100;
                        isHourOnUpTrend = FourOneHour.First().ClosePrice >= FourOneHour.Max(x => x.ClosePrice);
                        coinprice.IsOneHourOnDownTrend = FourOneHour.First().ClosePrice < FourOneHour.Last().ClosePrice;
                    }
                    else
                    {
                        coinprice.OneHourPriceChange = 0;
                        coinprice.IsOneHourOnDownTrend =true;
                    }

                    var FourHour = allSignals.Where(x => x.Pair == coin.Pair && x.CandleType == "4hour");

                   
                    if (FourHour.Any())
                    {
                        coinprice.FourHourPriceChange = ((FourHour.First().ClosePrice - FourHour.Last().ClosePrice) / FourHour.Last().ClosePrice) * 100;
                        coinprice.IsFourHourOnDownTrend = FourHour.First().ClosePrice < FourHour.Last().ClosePrice;
                    }
                    else
                    {
                        coinprice.FourHourPriceChange = 0;
                        coinprice.IsFourHourOnDownTrend = true;
                    }


                    var OneDay = allSignals.Where(x => x.Pair == coin.Pair && x.CandleType == "day").Take(7);

                  
                    if (OneDay.Any())
                    {
                        coinprice.DayPriceChange = ((OneDay.First().ClosePrice - OneDay.Last().ClosePrice) / OneDay.Last().ClosePrice) * 100;
                        coinprice.IsDayOnDownTrend = OneDay.First().ClosePrice < OneDay.Last().ClosePrice;
                    }
                    else
                    {
                        coinprice.DayPriceChange = 0;
                        coinprice.IsDayOnDownTrend = true;
                    }


                    //coinprice.StartingToClimb = isOneOnUpTrend && isFiveOnUpTrend;

                    coinprice.ClimbingFast = isOneOnUpTrend && isFiveOnUpTrend && isFifteenOnUpTrend; //&& isThirtyOnUpTrend

                    coinprice.ClimbedHigh = isOneOnUpTrend && isFiveOnUpTrend && isFifteenOnUpTrend && isThirtyOnUpTrend;

                    coinprice.SuperHigh = isOneOnUpTrend && isFiveOnUpTrend && isFifteenOnUpTrend && isThirtyOnUpTrend && isHourOnUpTrend;

                    coinprice.DayTradeCount= coin.DayTradeCount;
                    coinprice.DayVloume = coin.DayVolume;
                    //coinprice.PriceChangeNumber =
                    //    coinprice.DayPriceChange +
                    //    coinprice.FourHourPriceChange +
                    //    coinprice.OneHourPriceChange +
                    //    coinprice.ThirtyMinPriceChange +
                    //    coinprice.FifteenMinPriceChange +
                    //    coinprice.FiveMinPriceChange +
                    //    coinprice.OneMinPriceChange;


                    //if (coinprice.StartingToClimb)
                    //{
                    //    coinprice.BuyDialog = "L1: up in five 1,5 candles<br>Might climb";
                    //}

                    if (coinprice.ClimbingFast)
                    {
                        coinprice.BuyDialog = "L1: up in five 1,5 and 15 min candles<br>Good to buy";
                    }

                    if (coinprice.ClimbedHigh)
                    {
                        coinprice.BuyDialog = "L2: up in five 1,5,15,30 min candles<br>Mostly will go high";
                    }

                    if (coinprice.SuperHigh)
                    {
                        coinprice.BuyDialog = "L3: up in five 1,5,15,30,60 min candles<br>Can go higher or will start to go down?";
                    }

                    coinPrices.Add(coinprice);
                }
                catch
                {
                  
                }
            }
       
            return coinPrices;
        }


        //  [Route("api/buy/MarkToBuy/{pair}")]
        [HttpPut]
        [Route("[action]/{pair}")]
        public async Task<IActionResult> MarkToBuy(string pair)
        {
            MyCoins myCoin = null;
            if (!string.IsNullOrEmpty(pair))
            {
                myCoin = await _db.MyCoins.FirstOrDefaultAsync(x => x.Pair == pair);
                if (myCoin != null)
                {
                    myCoin.ForceBuy = true;
                    _db.Update(myCoin);
                    await _db.SaveChangesAsync();
                }
            }
            return Ok(myCoin);
        }

        [HttpPut]
        [Route("[action]/{pair}")]
        public async Task<IActionResult> CancelBuy(string pair)
        {
            MyCoins myCoin = null;
            if (!string.IsNullOrEmpty(pair))
            {
                myCoin = await _db.MyCoins.FirstOrDefaultAsync(x => x.Pair == pair);
                if (myCoin != null)
                {
                    myCoin.ForceBuy = false;
                    _db.Update(myCoin);
                    await _db.SaveChangesAsync();
                }
            }
            return Ok(myCoin);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetBuyDecisions()
        {
            var res= await AllCoinPrice();
            return Ok(res.OrderByDescending(x => x.DayTradeCount).ToList());
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Surgers()
        {
            var res = await AllCoinPrice();
            res=res.Where(
                x=> x.ClimbingFast==true 
                ||x.ClimbedHigh==true
                ||x.SuperHigh==true
                ||(x.Five_OneMinPriceChange>0.2M && x.Ten_OneMinPriceChange > 0.3M && x.Fifteen_OneMinPriceChange>0.3M)
                ).OrderByDescending(x=>x.DayTradeCount).ToList();
            return Ok(res);
        }

    }
}
