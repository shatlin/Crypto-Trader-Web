using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TraderWeb.Shared;

namespace TraderWeb.Server.Data
{
    public class DB: DbContext
    {

        public DB(DbContextOptions<DB> options ) : base(options)
        {
            Database.SetCommandTimeout(new TimeSpan(0,5,120));
            // Database.EnsureCreated();
        }

    
        public DbSet<Config> Config { get; set; }
        public DbSet<Player> Player { get; set; }
        public DbSet<PlayerTrades> PlayerTrades { get; set; }
        public DbSet<SignalCandle> SignalCandle { get; set; }
        public DbSet<MyCoins> MyCoins { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
             //   optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=TraderProdV2;Integrated Security=True;Connect Timeout=60");
                base.OnConfiguring(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ConfigConfiguration());
            modelBuilder.ApplyConfiguration(new PlayerConfiguration());
            modelBuilder.ApplyConfiguration(new PlayerHistConfiguration());
            modelBuilder.ApplyConfiguration(new SignalCandleConfiguration());
            modelBuilder.ApplyConfiguration(new CoinConfiguration());
        }
    }

   
}
