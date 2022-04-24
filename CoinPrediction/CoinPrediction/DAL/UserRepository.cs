using AutoMapper;
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

            this.context.Database.EnsureCreated();          
        }

        public bool DeleteUser(Guid id)
        {
            var dbUserToDelete = context.Users.SingleOrDefault(u => u.Id == id);

            if (dbUserToDelete == null)
                return false;

            context.Users.Remove(dbUserToDelete);
            context.SaveChanges();

            return true;
        }

        public User? GetUserByID(Guid id)
        {
            var dbUser = context.Users.SingleOrDefault(u => u.Id == id);                            

            return mapper.Map<User>(dbUser);
        }
        
        public IEnumerable<User> GetUsers()
        {
            var users = context.Users
                .Select(mapper.Map<User>)
                .AsEnumerable();

            return users;
        }

        public User InsertUser(User user)
        {
            var dbUser = mapper.Map<DbUser>(user);
            dbUser.Wallet = CheckCoinsExisting(user);

            context.Users.Add(dbUser);
            context.SaveChanges();

            return mapper.Map<User>(dbUser);
        }

        public User? UpdateUser(User user)
        {
            var dbUserToUpdate = context.Users.SingleOrDefault(u => u.Id == user.Id);

            if (dbUserToUpdate == null)
                return null;

            dbUserToUpdate.Wallet = CheckCoinsExisting(user);

            dbUserToUpdate.Username = user.Username;
            dbUserToUpdate.Password = user.Password;
            context.SaveChanges();

            return mapper.Map<User>(dbUserToUpdate);

        }

        private List<DbUserAsset> CheckCoinsExisting(User user)
        {
            var walletCoins = user.Wallet.Select(x => x.Coin?.CoinId);
            var dbCoins = context.Coins.Where(c => walletCoins.Contains(c.CoinId));

            var newWallet = new List<DbUserAsset>();
            int i = 0;
            foreach (var coin in dbCoins)
            {
                newWallet.Add(new DbUserAsset()
                {
                    Coin = coin,
                    Amount = user.Wallet.Select(x => x.Amount).ToArray()[i++],
                });
            }

            return newWallet;
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
