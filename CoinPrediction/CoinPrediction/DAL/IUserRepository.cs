using CoinPrediction.Model;

namespace CoinPrediction.DAL
{
    public interface IUserRepository: IDisposable
    {
        IEnumerable<User> GetUsers();
        User GetUserByID(int id);
        User InsertUser(User coin);
        bool DeleteUser(int id);
        User AddAssetToUser(int id, UserAsset asset);
      //  User UpdateUser(User coin);
    }
}
