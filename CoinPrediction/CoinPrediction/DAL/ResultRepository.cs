using AutoMapper;
using CoinPrediction.DAL.EfDbContext;
using CoinPrediction.DAL.EfDbContext.Entities;
using CoinPrediction.Model;

namespace CoinPrediction.DAL
{
    public class ResultRepository: IResultRepository, IDisposable
    {
        private readonly CryptoMarketContext context;
        private readonly IMapper mapper;

        public ResultRepository(CryptoMarketContext context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Insert a new simulation result to the db.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public async Task<SimulationResult> InsertResult(SimulationResult result) 
        {
            var dbResult = mapper.Map<DbSimulationResult>(result);
            await context.SimulationResults.AddAsync(dbResult);
            context.SaveChanges();

            return mapper.Map<SimulationResult>(dbResult);
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
