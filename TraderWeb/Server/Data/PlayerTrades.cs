using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TraderWeb.Shared;

namespace TraderWeb.Server.Data
{
   
    public partial class PlayerHistConfiguration : IEntityTypeConfiguration<PlayerTrades>
    {
        public void Configure(EntityTypeBuilder<PlayerTrades> builder)
        {
            builder.Property(e => e.BuyOrSell).IsRequired(false);
            builder.Property(e => e.ProfitLossChanges).IsRequired(false);
            builder.Property(e => e.DayHigh).IsRequired().HasColumnType("decimal(30, 12)");
            builder.Property(e => e.DayLow).IsRequired().HasColumnType("decimal(30, 12)");
            builder.Property(e => e.BuyBelowPerc).IsRequired().HasColumnType("decimal(30, 12)");
            builder.Property(e => e.SellBelowPerc).IsRequired().HasColumnType("decimal(30, 12)");
            builder.Property(e => e.DontSellBelowPerc).IsRequired().HasColumnType("decimal(30, 12)");
            builder.Property(e => e.BuyCoinPrice).IsRequired().HasColumnType("decimal(30, 12)");
            builder.Property(e => e.CurrentCoinPrice).IsRequired().HasColumnType("decimal(30, 12)");
            builder.Property(e => e.Quantity).IsRequired().HasColumnType("decimal(30, 12)");
            builder.Property(e => e.TotalBuyCost).IsRequired().HasColumnType("decimal(30, 12)");
            builder.Property(e => e.TotalCurrentValue).IsRequired().HasColumnType("decimal(30, 12)");
           
            builder.Property(e => e.AvailableAmountToBuy).IsRequired().HasColumnType("decimal(30, 12)");
            builder.Property(e => e.BuyCommision).IsRequired().HasColumnType("decimal(30, 12)");
            builder.Property(e => e.SellCoinPrice).IsRequired().HasColumnType("decimal(30, 12)");
           
            builder.Property(e => e.SellCommision).IsRequired().HasColumnType("decimal(30, 12)");
            builder.Property(e => e.TotalSellAmount).IsRequired().HasColumnType("decimal(30, 12)");
           
            builder.Property(e => e.SellAbovePerc).IsRequired().HasColumnType("decimal(30, 12)");
            builder.Property(e => e.ProfitLossAmt).IsRequired().HasColumnType("decimal(30, 12)");
            builder.Property(e => e.LastRoundProfitPerc).IsRequired().HasColumnType("decimal(30, 12)");
            builder.Property(e => e.HardSellPerc).IsRequired().HasColumnType("decimal(30, 12)");

        }
    }

}
