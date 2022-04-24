using CoinPrediction.Model;

namespace CoinPrediction.DAL
{
    public interface ICoinRepository: IDisposable
    {
        IEnumerable<Coin> GetCoins();
        Coin? GetCoinByID(Guid id);
        Coin InsertCoin(Coin coin);
        bool DeleteCoin(Guid id);
        Coin? UpdateCoin(Coin coin);
    }
}
