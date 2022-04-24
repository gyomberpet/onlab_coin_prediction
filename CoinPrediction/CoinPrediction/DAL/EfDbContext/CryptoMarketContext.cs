using CoinPrediction.DAL.EfDbContext.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoinPrediction.DAL.EfDbContext
{
    public class CryptoMarketContext: DbContext
    {
        public DbSet<DbCoin> Coins => Set<DbCoin>();
        public DbSet<DbUser> Users => Set<DbUser>();

        public CryptoMarketContext(DbContextOptions options)
           : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DbCoin>()
                .ToContainer("Coins")
                .HasPartitionKey(c => c.Id);

            modelBuilder.Entity<DbUser>()
                .ToContainer("Users")
                .HasPartitionKey(u => u.Id);
        }
    }
}
