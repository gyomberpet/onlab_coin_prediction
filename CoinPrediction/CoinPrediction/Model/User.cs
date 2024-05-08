namespace CoinPrediction.Model
{
    public class User
    {
        public int Id { get; init; }
        public string Username { get; init; }
        public string Password { get; init; }
        public List<UserAsset> UserAssets { get; init; } = new List<UserAsset>();
    }
}