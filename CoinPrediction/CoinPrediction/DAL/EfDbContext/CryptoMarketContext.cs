using CoinPrediction.DAL.EfDbContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CoinPrediction.DAL.EfDbContext
{
    public class CryptoMarketContext: DbContext
    {
        public DbSet<DbCoin> Coins => Set<DbCoin>();
        public DbSet<DbUser> Users => Set<DbUser>();
        public DbSet<DbUserAsset> UserAssets => Set<DbUserAsset>();
        public DbSet<DbPairHourBTCUSDT> BinanceHourBTCUSDT => Set<DbPairHourBTCUSDT>();
        public DbSet<DbPairMinuteBTCUSDT> BinanceMinuteBTCUSDT => Set<DbPairMinuteBTCUSDT>();
        public DbSet<DbSimulationResult> SimulationResults => Set<DbSimulationResult>();


        public CryptoMarketContext(DbContextOptions options)
           : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DbCoin>()
                .HasIndex(e => e.CoinId)
                .IsUnique();
        }
    }

    /// <summary>
    /// Help to create the connection to the db.
    /// </summary>
    public class AppDbContextFactory : IDesignTimeDbContextFactory<CryptoMarketContext>
    {
        public CryptoMarketContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<CryptoMarketContext>();
            builder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CryptoMarket;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            return new CryptoMarketContext(builder.Options);
        }
    }
}
