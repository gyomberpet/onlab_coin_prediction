using CoinPrediction.Model;

namespace CoinPrediction.DAL
{
    public interface IResultRepository: IDisposable
    {
        Task<SimulationResult> InsertResult(SimulationResult result);
    }
}
