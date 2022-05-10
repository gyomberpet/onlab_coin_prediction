﻿using CoinPrediction.DAL;
using CoinPrediction.Model;
using Microsoft.AspNetCore.Mvc;

namespace CoinPrediction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoinsController : ControllerBase
    {
        private readonly ICoinRepository coinRepository;

        public CoinsController(ICoinRepository coinRepository) 
        {
            this.coinRepository = coinRepository ?? throw new ArgumentNullException(nameof(coinRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Coin>>> Get() 
        {
            var coins = await coinRepository.GetCoins();
            return Ok(coins);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Coin>> Get(int id)
        {
            var coins = await coinRepository.GetCoinByID(id);
            return Ok(coins);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] Coin coin)
        {
            try
            {
                Coin created = await coinRepository.InsertCoin(coin);
                if (created == null)
                    return BadRequest();

                return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut]
        public async Task<ActionResult<Coin>> Put([FromBody] Coin coin) 
        {
            var updated = await coinRepository.UpdateCoin(coin);
            
            if(updated == null)
                return NotFound();

            if (updated == coin)
                return BadRequest();

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id) 
        {
            var isSuccess = await coinRepository.DeleteCoin(id);

            if (!isSuccess)
                return NotFound();

            return NoContent();
        }

    }
}
