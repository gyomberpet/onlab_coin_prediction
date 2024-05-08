using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoinPrediction.DAL.EfDbContext;
using CoinPrediction.DAL.EfDbContext.Entities;
using CoinPrediction.Model;
using Microsoft.EntityFrameworkCore;

namespace CoinPrediction.DAL
{
    public class UserRepository: IUserRepository, IDisposable
    {
        private readonly CryptoMarketContext context;
        private readonly IMapper mapper;

        public UserRepository(CryptoMarketContext context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> DeleteUser(int id)
        {
            var dbUserToDelete = await context.Users.SingleOrDefaultAsync(u => u.Id == id);

            if (dbUserToDelete == null)
                return false;

            context.Users.Remove(dbUserToDelete);
            context.SaveChanges();

            return true;
        }

        public async Task<User> GetUserByID(int id)
        {
            var dbUser = await context.Users
                .Include(u => u.UserAssets)
                    .ThenInclude(a => a.Coin)
                .SingleOrDefaultAsync(u => u.Id == id);  

            return mapper.Map<User>(dbUser);
        }
        
        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await context.Users
                .ProjectTo<User>(mapper.ConfigurationProvider)
                .ToListAsync();

            return users;
        }

        public async Task<User> InsertUser(User user)
        {
            var dbUser = mapper.Map<DbUser>(user);
            dbUser.UserAssets = null;

            await context.Users.AddAsync(dbUser);
            context.SaveChanges();

            return mapper.Map<User>(dbUser);
        }

        public async Task<User> AddAssetToUser(int id, UserAsset asset) 
        {
            var dbUser = await context.Users.SingleOrDefaultAsync(u => u.Id == id);

            if (dbUser == null) 
                return null;

            var dbCoin = await context.Coins.SingleOrDefaultAsync(c => c.CoinId == asset.Coin.CoinId);

            if (dbCoin == null)
                return null;

            var dbAsset = await context.UserAssets.SingleOrDefaultAsync(a => a.Coin.CoinId == asset.Coin.CoinId);

            if (dbAsset == null)
            {
                dbAsset = new DbUserAsset()
                {
                    Coin = dbCoin,
                    Amount = asset.Amount
                };

                dbUser.UserAssets.Add(dbAsset);
                context.SaveChanges();

                return mapper.Map<User>(dbUser);
            }
                    
            dbAsset.Amount += asset.Amount;
            context.SaveChanges();

            return mapper.Map<User>(dbUser);
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
