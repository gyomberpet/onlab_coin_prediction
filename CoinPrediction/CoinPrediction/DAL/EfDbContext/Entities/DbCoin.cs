namespace CoinPrediction.DAL.EfDbContext.Entities
{
    public class DbCoin
    {
        public int Id { get; set; }
        public string CoinId { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public List<DbUserAsset> UserAssets { get; set; } = new List<DbUserAsset>();
    }
}
