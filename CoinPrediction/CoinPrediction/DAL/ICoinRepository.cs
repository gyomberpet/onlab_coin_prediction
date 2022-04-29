using CoinPrediction.Model;

namespace CoinPrediction.DAL
{
    public interface ICoinRepository: IDisposable
    {
        IEnumerable<Coin> GetCoins();
        Coin GetCoinByID(int id);
        Coin InsertCoin(Coin coin);
        bool DeleteCoin(int id);
        Coin UpdateCoin(Coin coin);
    }
}
