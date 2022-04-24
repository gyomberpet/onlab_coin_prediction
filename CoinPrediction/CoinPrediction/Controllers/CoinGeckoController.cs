using CoinGecko.Clients;
using CoinGecko.Entities.Response;
using CoinGecko.Entities.Response.Coins;
using CoinGecko.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoinPrediction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoinGeckoController : ControllerBase
    {
        // const string apiUrl = "https://api.coingecko.com/api/v3";
        private ICoinGeckoClient _client;

        public CoinGeckoController() 
        {
            _client = CoinGeckoClient.Instance;
        }

        [HttpGet]
        [Route("ping")]
        public async Task<ActionResult<Ping>> Ping()
        {
            var result =  await _client.PingClient.GetPingAsync();
            if(result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet]
        [Route("coin/{id}/{currency}")]
        public async Task<ActionResult<CoinMarkets>> GetCoin(string id, string currency)
        {
            var response = await _client.CoinsClient.GetCoinMarkets(currency, new[] { id }, null, null, null, false, "1h", "");
            var result = response[0];
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet]
        [Route("coinmarkets/{currency}")]
        public async Task<ActionResult<List<CoinMarkets>>> GetMarkets(string currency)
        {
            var result = await _client.CoinsClient.GetCoinMarkets(currency, new string[] { }, null, null, null, false,
                "1h,24h,7d,14d,30d,200d,1y", "");
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet]
        [Route("marketchart/{id}/{currency}/{days}/{interval}")]
        public async Task<ActionResult<MarketChartById>> GetMarketChart(string id, string currency, string days, string interval)
        {
            var result = await _client.CoinsClient.GetMarketChartsByCoinId(id, currency, days, interval);
            if(result == null)
                return NotFound();
            return Ok(result);
        }

    }
}
