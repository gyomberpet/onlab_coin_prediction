using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoinPrediction.DAL.EfDbContext;
using CoinPrediction.Model;

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
        public IEnumerable<PairHourBTCUSDT> GetBTCUSDTPairsHourly()
        {
            
            return context.BinanceHourBTCUSDT
                .ProjectTo<PairHourBTCUSDT>(mapper.ConfigurationProvider)
                .Take(100)
                .AsEnumerable();
        }

        public IEnumerable<PairMinuteBTCUSDT> GetBTCUSDTPairsMinutely()
        {
            return context.BinanceMinuteBTCUSDT
                .ProjectTo<PairMinuteBTCUSDT>(mapper.ConfigurationProvider)
                .Take(100)
                .AsEnumerable();
        }

        /*public IEnumerable<PriceStory> InsertPriceStories(IEnumerable<PriceStory> priceStories)
        {
            var dbExistingCoins = context.Coins
                .ToList();

            // Only existing coins
            var filteredPriceStories = priceStories
                .Where(ps => dbExistingCoins
                    .Select(c => c.CoinId)
                    .Contains(ps.Coin.CoinId));

            var existingPriceStories = GetPriceStories();

            var newPriceStories = filteredPriceStories
                .Where(priceStory => !existingPriceStories
                    .Any(eps => eps.Date == priceStory.Date && eps.Coin.CoinId == priceStory.Coin.CoinId));

            var dbNewPriceStories = newPriceStories.Select(nps => new DbPairInUSDT() {
                Coin = dbExistingCoins.SingleOrDefault(c => c.CoinId == nps.Coin.CoinId),
                Date = nps.Date,
                Price = nps.Price,
            });

            context.AddRange(dbNewPriceStories);
            context.SaveChanges();

            var result = newPriceStories.Select(mapper.Map<PriceStory>);

            return result;
        }*/

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
