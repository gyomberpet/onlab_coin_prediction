using CoinPrediction.Model;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoinPrediction.DAL.EfDbContext.Entities;
using CoinPrediction.DAL.EfDbContext;

namespace CoinPrediction.DAL
{
    public class CoinRepository : ICoinRepository, IDisposable
    {
        private readonly CryptoMarketContext context;
        private readonly IMapper mapper;

        public CoinRepository(CryptoMarketContext context, IMapper mapper) 
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public bool DeleteCoin(int id)
        {
            var dbCoinToDelete = context.Coins.SingleOrDefault(c => c.Id == id);

            if (dbCoinToDelete == null)
                return false;

            context.Coins.Remove(dbCoinToDelete);
            context.SaveChanges();

            return true;
        }

        public Coin GetCoinByID(int id)
        {
            var dbCoin = context.Coins .SingleOrDefault(c => c.Id == id);

            return mapper.Map<Coin>(dbCoin);
        }

        public IEnumerable<Coin> GetCoins()
        {
            var coins = context.Coins
                .Select(mapper.Map<Coin>)
                .AsEnumerable();

            return coins;
        }

        public Coin InsertCoin(Coin coin)
        {
            var coinIds = GetCoins().Select(c => c.CoinId);
            if (coinIds.Contains(coin.CoinId))
                return null;

            var dbCoin = mapper.Map<DbCoin>(coin);
            context.Coins.Add(dbCoin);
            context.SaveChanges();

            return mapper.Map<Coin>(dbCoin);
        }

        public Coin UpdateCoin(Coin coin)
        {   
            var dbCoinToUpdate = context.Coins.SingleOrDefault(c => c.Id == coin.Id);

            if(dbCoinToUpdate == null)
                return null;

            var coinIds = GetCoins().Select(c => c.CoinId);
            if (coinIds.Contains(coin.CoinId) && coin.CoinId != dbCoinToUpdate.CoinId)
                return coin;

            dbCoinToUpdate.CoinId = coin.CoinId;
            dbCoinToUpdate.Name = coin.Name;
            dbCoinToUpdate.Symbol = coin.Symbol;
            context.SaveChanges();

            return mapper.Map<Coin>(dbCoinToUpdate);

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
