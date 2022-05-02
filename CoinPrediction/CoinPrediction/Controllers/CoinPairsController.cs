using CoinPrediction.DAL;
using CoinPrediction.Model;
using Microsoft.AspNetCore.Mvc;

namespace CoinPrediction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoinPairsController : ControllerBase
    {
        private readonly ICoinPairRepository coinPairRepository;

        public CoinPairsController(ICoinPairRepository coinPairRepository)
        {
            this.coinPairRepository = coinPairRepository ?? throw new ArgumentNullException(nameof(coinPairRepository));
        }

        [HttpGet]
        [Route("btc/hourly")]
        public ActionResult<IEnumerable<PairHourBTCUSDT>> GetBTCUSDTPairsHourly()
        {
            var pairs = coinPairRepository.GetBTCUSDTPairsHourly();
            return Ok(pairs);
        }

        [HttpGet]
        [Route("btc/minutely")]
        public ActionResult<IEnumerable<PairMinuteBTCUSDT>> GetBTCUSDTPairsMinutely()
        {
            var pairs = coinPairRepository.GetBTCUSDTPairsMinutely();
            return Ok(pairs);
        }
   
    }
}