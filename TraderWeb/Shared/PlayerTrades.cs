using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TraderWeb.Shared
{
    public class PlayerTrades
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Pair { get; set; }
        public bool IsTrading { get; set; }
        public bool isSellAllowed { get; set; }
        public bool ForceSell { get; set; }

        public bool IsTracked { get; set; }
        public decimal? DayHigh { get; set; }
        public decimal? DayLow { get; set; }

        public decimal? HardSellPerc { get; set; }
        public bool isBuyOrderCompleted { get; set; }

        public decimal? BuyBelowPerc { get; set; }
        public decimal? SellBelowPerc { get; set; }
        public decimal? SellAbovePerc { get; set; }
        public decimal? LossSellBelow { get; set; }

        public decimal? BuyCoinPrice { get; set; }
       
        public decimal CurrentCoinPrice { get; set; }
        public decimal SellCoinPrice { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? BuyCommision { get; set; }
        public decimal? SellCommision { get; set; }
        public bool isBuyAllowed { get; set; }
        public decimal? LastRoundProfitPerc { get; set; }
        public int RepsTillCancelOrder { get; set; } // 10
        public decimal? TotalBuyCost { get; set; }
        public decimal? TotalCurrentValue { get; set; }
        public decimal? TotalSellAmount { get; set; }
        public decimal? AvailableAmountToBuy { get; set; }
        
        public DateTime? BuyTime { get; set; }
        public DateTime? SellTime { get; set; }
        public long BuyOrderId { get; set; }
        public long SellOrderId { get; set; }

        public DateTime? UpdatedTime { get; set; }

        public string ProfitLossChanges { get; set; }
        public string BuyOrSell { get; set; }
        public decimal ProfitLossAmt { get; set; }
    }

   

}
