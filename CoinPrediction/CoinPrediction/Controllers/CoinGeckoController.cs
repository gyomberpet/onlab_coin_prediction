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
        const string apiUrl = "https://api.coingecko.com/api/v3";
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
        [Route("coinMarkets/{currency}")]
        public async Task<ActionResult<List<CoinMarkets>>> GetMarkets(string currency)
        {
            var result = await _client.CoinsClient.GetCoinMarkets(currency, new string[] { }, null, null, null, false,
                "1h,24h,7d,14d,30d,200d,1y", "");
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        
        // GET api/<CoinsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CoinsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CoinsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CoinsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
