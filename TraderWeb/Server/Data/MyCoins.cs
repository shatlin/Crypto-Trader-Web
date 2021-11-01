using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TraderWeb.Shared;

namespace TraderWeb.Server.Data
{
    public partial class CoinConfiguration : IEntityTypeConfiguration<MyCoins>
    {
        public void Configure(EntityTypeBuilder<MyCoins> builder)
        {
            builder.Property(e => e.PercBelowDayHighToBuy).IsRequired().HasColumnType("decimal(30, 12)");
            builder.Property(e => e.PercAboveDayLowToSell).IsRequired().HasColumnType("decimal(30, 12)");
            builder.Property(e => e.DayTradeCount).IsRequired().HasColumnType("decimal(30, 12)");

            builder.Property(e => e.DayVolume).IsRequired().HasColumnType("decimal(30, 12)");
            builder.Property(e => e.DayVolumeUSDT).IsRequired().HasColumnType("decimal(30, 12)");

            builder.Property(e => e.DayOpenPrice).IsRequired().HasColumnType("decimal(30, 12)");
            builder.Property(e => e.DayHighPrice).IsRequired().HasColumnType("decimal(30, 12)");
            builder.Property(e => e.DayLowPrice).IsRequired().HasColumnType("decimal(30, 12)");
            builder.Property(e => e.CurrentPrice).IsRequired().HasColumnType("decimal(30, 12)");
            builder.Property(e => e.FiveMinChange).IsRequired().HasColumnType("decimal(30, 12)");
            builder.Property(e => e.TenMinChange).IsRequired().HasColumnType("decimal(30, 12)");
            builder.Property(e => e.FifteenMinChange).IsRequired().HasColumnType("decimal(30, 12)");
            builder.Property(e => e.ThirtyMinChange).IsRequired().HasColumnType("decimal(30, 12)");
            builder.Property(e => e.FourtyFiveMinChange).IsRequired().HasColumnType("decimal(30, 12)");
            builder.Property(e => e.OneHourChange).IsRequired().HasColumnType("decimal(30, 12)");
            builder.Property(e => e.FourHourChange).IsRequired().HasColumnType("decimal(30, 12)");
            builder.Property(e => e.TwentyFourHourChange).IsRequired().HasColumnType("decimal(30, 12)");
            builder.Property(e => e.FortyEightHourChange).IsRequired().HasColumnType("decimal(30, 12)");
            builder.Property(e => e.OneWeekChange).IsRequired().HasColumnType("decimal(30, 12)");
            builder.Property(e => e.PrecisionDecimals).IsRequired().HasColumnType("decimal(30, 12)");
            builder.Property(e => e.MarketCap).IsRequired().HasColumnType("decimal(30, 12)");
            builder.Property(e => e.DayPriceDiff).IsRequired().HasColumnType("decimal(30, 12)");
        }

    }
}
