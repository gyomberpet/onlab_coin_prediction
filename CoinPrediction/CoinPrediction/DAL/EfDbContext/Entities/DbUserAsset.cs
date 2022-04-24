namespace CoinPrediction.DAL.EfDbContext.Entities
{
    public class DbUserAsset 
    {
        public DbCoin? Coin { get; set; }
        public double Amount { get; set; }
    }
}
