using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoinPrediction.DAL.EfDbContext;
using CoinPrediction.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using System.Data.SqlClient;

namespace CoinPrediction.DAL
{
    public class CoinPairRepository : ICoinPairRepository, IDisposable
    {
        private readonly CryptoMarketContext context;
        private readonly IMapper mapper;

        public CoinPairRepository(CryptoMarketContext context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Return the top 1000 BTC/USDT pairs hourly.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<PairHourBTCUSDT>> GetBTCUSDTPairsHourly()
        {
            
            return await context.BinanceHourBTCUSDT
                .ProjectTo<PairHourBTCUSDT>(mapper.ConfigurationProvider)
                .Take(1000)
                .ToListAsync();
        }

        /// <summary>
        /// Return the top 1000 BTC/USDT pairs minutely.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<PairMinuteBTCUSDT>> GetBTCUSDTPairsMinutely()
        {
            return await context.BinanceMinuteBTCUSDT
                .ProjectTo<PairMinuteBTCUSDT>(mapper.ConfigurationProvider)
                .Take(1000)
                .ToListAsync();
        }

        
        #region "Dispose"

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
