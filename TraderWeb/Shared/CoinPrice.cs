
using System;
using System.ComponentModel.DataAnnotations;

namespace TraderWeb.Shared
{


    public class CoinPrice: SignalCandle
    {
     
        public int CoinRank { get; set; }
        public decimal DayMin { get; set; }
        public decimal DayMax { get; set; }
        public int TotalOneMinUps { get; set; } // last 30 mins
        public int TotalOneMinDowns { get; set; }
        public decimal OneMinPriceChange { get; set; }
        public decimal Ten_OneMinPriceChange { get; set; }
        public decimal Five_OneMinPriceChange { get; set; }
        public decimal Fifteen_OneMinPriceChange { get; set; }
        public bool IsLast5OneMinOnDownTrend { get; set; }
        public bool IsOneMinOnDownTrend { get; set; }

        public int TotalFiveMinUps { get; set; } // last 30 mins
        public int TotalFiveMinDowns { get; set; }
        public decimal FiveMinPriceChange { get; set; }
        public bool IsLast5FiveMinOnDownTrend { get; set; }
        public bool IsFiveMinOnDownTrend { get; set; }

        public int TotalFifteenMinUps { get; set; } // last 30 mins
        public int TotalFifteenMinDowns { get; set; }
        public decimal FifteenMinPriceChange { get; set; }
        public bool IsLast5FifteenMinOnDownTrend { get; set; }
        public bool IsFifteenMinOnDownTrend { get; set; }

        public int TotalThirtyMinUps { get; set; } // last 30 mins
        public int TotalThirtyMinDowns { get; set; }
        public decimal ThirtyMinPriceChange { get; set; }
        public bool IsLast5ThirtyMinOnDownTrend { get; set; }
        public bool IsThirtyMinOnDownTrend { get; set; }

        public int TotalOneHourUps { get; set; } // last 30 mins
        public int TotalOneHourDowns { get; set; }
        public decimal OneHourPriceChange { get; set; }
        public bool IsLast5OneHourOnDownTrend { get; set; }
        public bool IsOneHourOnDownTrend { get; set; }

        public int TotalFourHourUps { get; set; } // last 30 mins
        public int TotalFourHourDowns { get; set; }
        public decimal FourHourPriceChange { get; set; }
        public bool IsLast5FourHourOnDownTrend { get; set; }
        public bool IsFourHourOnDownTrend { get; set; }

        public int TotalDayUps { get; set; } // last 30 mins
        public int TotalDayDowns { get; set; }
        public decimal DayPriceChange { get; set; }
        public bool IsLast5DayOnDownTrend { get; set; }
        public bool IsDayOnDownTrend { get; set; }
        public string BuyDialog { get; set; }

        public decimal PriceChangeNumber { get; set; }
        public bool StartingToClimb { get; set; }
        public bool ClimbingFast { get; set; }
        public bool ClimbedHigh { get; set; }
        public bool SuperHigh { get; set; }

        public decimal DayTradeCount { get; set; }
        public decimal DayVloume { get; set; }
    }




}
