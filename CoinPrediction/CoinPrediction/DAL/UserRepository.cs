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

        public bool DeleteUser(int id)
        {
            var dbUserToDelete = context.Users.SingleOrDefault(u => u.Id == id);

            if (dbUserToDelete == null)
                return false;

            context.Users.Remove(dbUserToDelete);
            context.SaveChanges();

            return true;
        }

        public User GetUserByID(int id)
        {
            var dbUser = context.Users
                .Include(u => u.UserAssets)
                    .ThenInclude(a => a.Coin)
                .SingleOrDefault(u => u.Id == id);  

            return mapper.Map<User>(dbUser);
        }
        
        public IEnumerable<User> GetUsers()
        {
            var users = context.Users
                .ProjectTo<User>(mapper.ConfigurationProvider)
                .AsEnumerable();

            return users;
        }

        public User InsertUser(User user)
        {
            var dbUser = mapper.Map<DbUser>(user);
            dbUser.UserAssets = null;

            context.Users.Add(dbUser);
            context.SaveChanges();

            return mapper.Map<User>(dbUser);
        }

        public User AddAssetToUser(int id, UserAsset asset) 
        {
            var dbUser = context.Users.SingleOrDefault(u => u.Id == id);

            if (dbUser == null) 
                return null;

            var dbCoin = context.Coins.SingleOrDefault(c => c.CoinId == asset.Coin.CoinId);

            if (dbCoin == null)
                return null;

            var dbAsset = context.UserAssets.SingleOrDefault(a => a.Coin.CoinId == asset.Coin.CoinId);

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

        //public User? UpdateUser(User user)
        //{
        //    var dbUserToUpdate = context.Users.SingleOrDefault(u => u.Id == user.Id);

        //    if (dbUserToUpdate == null)
        //        return null;

        //    dbUserToUpdate.UserAssets = CheckCoinsExisting(user);

        //    dbUserToUpdate.Username = user.Username;
        //    dbUserToUpdate.Password = user.Password;
        //    context.SaveChanges();

        //    return mapper.Map<User>(dbUserToUpdate);

        //}

        //private List<DbUserAsset> CheckCoinsExisting(User user)
        //{
        //    var coinIds = user.UserAssets.Select(x => x.Coin?.CoinId);
        //    var dbCoins = context.Coins.Where(c => coinIds.Contains(c.CoinId)).ToList();

        //    var userAssets = new List<DbUserAsset>();
        //    int i = 0;
        //    foreach (var coin in dbCoins)
        //    {
        //        userAssets.Add(new DbUserAsset()
        //        {
        //            Coin = coin,
        //            Amount = user.UserAssets.Select(x => x.Amount).ToArray()[i++],
        //        });
        //    }

        //    return userAssets;
        //}

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
