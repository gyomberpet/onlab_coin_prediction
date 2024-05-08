using CoinPrediction.Model;

namespace CoinPrediction.DAL
{
    public interface ICoinRepository: IDisposable
    {
        Task<IEnumerable<Coin>> GetCoins();
        Task<Coin> GetCoinByID(int id);
        Task<Coin> InsertCoin(Coin coin);
        Task<bool> DeleteCoin(int id);
        Task<Coin> UpdateCoin(Coin coin);
    }
}
