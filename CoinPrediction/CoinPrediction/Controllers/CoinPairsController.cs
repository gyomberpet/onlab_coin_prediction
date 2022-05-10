using CoinPrediction.DAL;
using CoinPrediction.Model;
using CoinPrediction.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoinPrediction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoinPairsController : ControllerBase
    {
        private readonly ICoinPairRepository coinPairRepository;
        private readonly IPricePredictorService pricePredictorService;
        private readonly IResultRepository resultRepository;

        public CoinPairsController(ICoinPairRepository coinPairRepository,
            IPricePredictorService pricePredictorService,
            IResultRepository resultRepository)
        {
            this.coinPairRepository = coinPairRepository ?? throw new ArgumentNullException(nameof(coinPairRepository));
            this.pricePredictorService = pricePredictorService ?? throw new ArgumentNullException(nameof(pricePredictorService));  
            this.resultRepository = resultRepository ?? throw new ArgumentNullException(nameof(resultRepository));
        }

        [HttpGet]
        [Route("btc/hourly")]
        public async Task<ActionResult<IEnumerable<PairHourBTCUSDT>>> GetBTCUSDTPairsHourly()
        {
            var pairs = await coinPairRepository.GetBTCUSDTPairsHourly();
            return Ok(pairs);
        }

        [HttpGet]
        [Route("btc/minutely")]
        public async Task<ActionResult<IEnumerable<PairMinuteBTCUSDT>>> GetBTCUSDTPairsMinutely()
        {
            var pairs = await coinPairRepository.GetBTCUSDTPairsMinutely();
            return Ok(pairs);
        }

        [HttpGet]
        [Route("simulate")]
        public async Task<ActionResult<SimulationResult>> RunSimulation([FromQuery] SimulationParams parameters) 
        {
            var simulationResult = pricePredictorService.PredictBTCUSDT(parameters);
            await resultRepository.InsertResult(simulationResult);

            return Ok(simulationResult);
        } 
    }
}