
using System;
using System.ComponentModel.DataAnnotations;

namespace TraderWeb.Shared
{


    public class SignalCandle
    {
        public Guid Id { get; set; }
        public string Pair { get; set; }
        public string CandleType { get; set; } //5Min,15Min,30Min,1Hr,Day
        public string UpOrDown { get; set; }
        public DateTime CloseTime { get; set; }
        public decimal ClosePrice { get; set; }
        public DateTime AddedTime { get; set; }
    }

    public class GlobalSignal
    {
        public decimal AveragePriceChange { get; set; }
        public decimal BitCoinPriceChange { get; set; }
        public decimal AveragePriceChangeThirtyMins { get; set; }
        public decimal BitCoinPriceChangeThirtyMins { get; set; }
        public bool IsMarketOnDownTrendToday { get; set; }
        public bool IsMarketOnUpTrendToday { get; set; }
        public bool IsBitCoinGoingUpToday { get; set; }
        public bool IsBitCoinGoingDownToday { get; set; }

        public bool IsMarketOnDownTrendThisWeek { get; set; }
        public bool IsMarketOnUpTrendThisWeek { get; set; }
        public bool IsBitCoinGoingUpThisWeek { get; set; }
        public bool IsBitCoinGoingDownThisWeek { get; set; }

        public bool AreMostCoinsGoingDownNow { get; set; }
        public bool AreMostCoinsGoingUpNow { get; set; }
    }


}
