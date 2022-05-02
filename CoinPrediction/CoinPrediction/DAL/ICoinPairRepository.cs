using CoinPrediction.Model;

namespace CoinPrediction.DAL
{
    public interface ICoinPairRepository: IDisposable
    {
        IEnumerable<PairHourBTCUSDT> GetBTCUSDTPairsHourly();
        IEnumerable<PairMinuteBTCUSDT> GetBTCUSDTPairsMinutely();

    }
}
