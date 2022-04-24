namespace CoinPrediction.Model
{
    public class Coin
    {
        public Coin(Guid id, string coinId, string name, string symbol)
        {
            Id = id;
            CoinId = coinId;
            Name = name;
            Symbol = symbol;
        }

        public Guid Id { get; }

        public string CoinId { get; }
        public string Name { get; }
        public string Symbol { get; }
    }
}
