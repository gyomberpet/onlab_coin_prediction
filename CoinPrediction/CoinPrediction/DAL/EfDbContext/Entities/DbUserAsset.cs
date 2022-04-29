namespace CoinPrediction.DAL.EfDbContext.Entities
{
    public class DbUserAsset 
    {
        public int Id { get; set; } 
        public DbCoin Coin { get; set; }
        public double Amount { get; set; }
        public  DbUser User { get; set; }
    }
}
