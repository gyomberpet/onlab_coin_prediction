using CoinPrediction.Model;

namespace CoinPrediction.DAL.EfDbContext.Entities
{
    public class DbUser
    {
        public Guid Id { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }

        public List<DbUserAsset> Wallet { get; set; } = new List<DbUserAsset>();
    }
}
