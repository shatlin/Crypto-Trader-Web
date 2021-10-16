using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TraderWeb.Shared;

namespace TraderWeb.Server.Data
{

    public partial class ConfigConfiguration : IEntityTypeConfiguration<Config>
    {
        public void Configure(EntityTypeBuilder<Config> builder)
        {
            builder.Property(e => e.MinimumAmountToTradeWith).IsRequired().HasColumnType("decimal(18, 2)");
            builder.Property(e => e.MaximumAmountForaBot).IsRequired().HasColumnType("decimal(18, 2)");
            builder.Property(e => e.BufferPriceForBuyAndSell).IsRequired().HasColumnType("decimal(18, 12)");
            builder.Property(e => e.CommisionAmount).IsRequired().HasColumnType("decimal(18, 12)");
            builder.Property(e => e.DivideHighAndAverageBy).IsRequired().HasColumnType("decimal(18, 12)");
            builder.Property(e => e.DayLowGreaterthanTobuy).IsRequired().HasColumnType("decimal(18, 2)");
            builder.Property(e => e.DayLowLessthanTobuy).IsRequired().HasColumnType("decimal(18, 2)");
            builder.Property(e => e.DayHighLessthanToSell).IsRequired().HasColumnType("decimal(18, 12)");
            builder.Property(e => e.DayHighGreaterthanToSell).IsRequired().HasColumnType("decimal(18, 12)");
            builder.Property(e => e.ReduceSellAboveBy).IsRequired().HasColumnType("decimal(6, 4)");
            builder.Property(e => e.MinSellAbovePerc).IsRequired().HasColumnType("decimal(6, 4)");
            builder.Property(e => e.ReducePriceDiffPercBy).IsRequired().HasColumnType("decimal(6, 4)");
            builder.Property(e => e.DefaultSellAbovePerc).IsRequired().HasColumnType("decimal(6, 4)");
            builder.Property(e => e.MinAllowedTradeCount).IsRequired().HasColumnType("decimal(18, 12)");
            builder.Property(e => e.ScalpFourHourDiffLessThan).IsRequired().HasColumnType("decimal(6, 4)");
            builder.Property(e => e.ScalpOneHourDiffLessThan).IsRequired().HasColumnType("decimal(6, 4)");
            builder.Property(e => e.ScalpThirtyMinDiffLessThan).IsRequired().HasColumnType("decimal(6, 4)");
            builder.Property(e => e.ScalpFifteenMinDiffLessThan).IsRequired().HasColumnType("decimal(6, 4)");
            builder.Property(e => e.ScalpFiveMinDiffLessThan).IsRequired().HasColumnType("decimal(6, 4)");
            builder.Property(e => e.SellWhenAllBotsAtLossBelow).IsRequired().HasColumnType("decimal(4, 2)");
        }
    }

    

}
