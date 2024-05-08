using CoinPrediction.Model;

namespace CoinPrediction.DAL
{
    public interface ICoinPairRepository: IDisposable
    {
        Task<IEnumerable<PairHourBTCUSDT>> GetBTCUSDTPairsHourly();
        Task<IEnumerable<PairMinuteBTCUSDT>> GetBTCUSDTPairsMinutely();

    }
}
