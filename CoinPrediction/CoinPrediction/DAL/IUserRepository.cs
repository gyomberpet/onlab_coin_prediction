using CoinPrediction.Model;

namespace CoinPrediction.DAL
{
    public interface IUserRepository: IDisposable
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUserByID(int id);
        Task<User> InsertUser(User coin);
        Task<bool> DeleteUser(int id);
        Task<User> AddAssetToUser(int id, UserAsset asset);
    }
}
