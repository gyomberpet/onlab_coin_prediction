using CoinPrediction.Model;

namespace CoinPrediction.Services
{
    public interface IPricePredictorService
    {
       SimulationResult PredictBTCUSDT(SimulationParams parameters);
    }
}
