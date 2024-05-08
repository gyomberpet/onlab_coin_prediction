namespace CoinPrediction.DAL.EfDbContext.Entities
{
    public class DbUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<DbUserAsset> UserAssets { get; set; } = new List<DbUserAsset>();
    }
}
