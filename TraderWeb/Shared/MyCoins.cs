
using System;
using System.ComponentModel.DataAnnotations;

namespace TraderWeb.Shared
{
    public class MyCoins
    {
        public int Id { get; set; }
        public string Pair { get; set; }
        public string CoinName { get; set; }
        public string CoinSymbol { get; set; }
        public int TradePrecision { get; set; }
        public bool IsIncludedForTrading { get; set; }
        public bool ClimbingFast { get; set; }
        public bool ClimbedHigh { get; set; }
        public bool SuperHigh { get; set; }
        public decimal PercBelowDayHighToBuy { get; set; }
        public decimal PercAboveDayLowToSell { get; set; }
        public bool ForceBuy { get; set; }
        public int Rank { get; set; }
        public decimal DayTradeCount { get; set; }
        public decimal DayVolume { get; set; }
        public decimal DayVolumeUSDT { get; set; }
        public decimal DayOpenPrice { get; set; }
        public decimal DayHighPrice { get; set; }
        public decimal DayLowPrice { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal DayPriceDiff { get; set; }
        public decimal FiveMinChange { get; set; }
        public decimal TenMinChange { get; set; }
        public decimal FifteenMinChange { get; set; }
        public decimal ThirtyMinChange { get; set; }
        public decimal FourtyFiveMinChange { get; set; }
        public decimal OneHourChange { get; set; }
        public decimal FourHourChange { get; set; }
        public decimal TwentyFourHourChange { get; set; } //24 hour change
        public decimal FortyEightHourChange { get; set; } //48 hour change
        public decimal OneWeekChange { get; set; } // 7 day change

        public decimal PrecisionDecimals { get; set; }
        public decimal MarketCap { get; set; }
        public string TradeSuggestion { get; set; }
    }


}
