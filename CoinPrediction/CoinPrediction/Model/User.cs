namespace CoinPrediction.Model
{
    public class User
    {
        public User(Guid id, string username, string password, List<UserAsset> wallet)
        {
            Id = id;
            Username = username;
            Password = password;
            Wallet = wallet;
        }

        public Guid Id { get; }
        public string Username { get; }
        public string Password { get; }
        public List<UserAsset> Wallet { get; }
    }

   
}