
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
        public decimal FiveMinChange { get; set; }
        public decimal TenMinChange { get; set; }
        public decimal FifteenMinChange { get; set; }
        public decimal ThirtyMinChange { get; set; }
        public decimal OneHourChange { get; set; }
        public decimal FourHourChange { get; set; }
        public decimal OneDayChange { get; set; }
        public decimal TwoDayChange { get; set; }
        public decimal OneWeekChange { get; set; }
        public decimal PrecisionDecimals { get; set; }
        public decimal MarketCap { get; set; }
        public string TradeSuggestion { get; set; }
    }


}
