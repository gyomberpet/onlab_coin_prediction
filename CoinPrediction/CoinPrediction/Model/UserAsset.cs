namespace CoinPrediction.Model
{
    public class UserAsset
    {
        public UserAsset(Coin coin, double amount)
        {
            Coin = coin;
            Amount = amount;
        }

        public Coin Coin { get; }
        public double Amount { get; }

    }
}
