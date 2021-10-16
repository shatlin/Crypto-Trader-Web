using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TraderWeb.Shared;

namespace TraderWeb.Server.Data
{
  
    public partial class SignalCandleConfiguration : IEntityTypeConfiguration<SignalCandle>
    {

        public void Configure(EntityTypeBuilder<SignalCandle> builder)
        {
            builder.Property(e => e.ClosePrice).IsRequired().HasColumnType("decimal(30, 12)");
        }
    }
}
