using CoinPrediction.Model;

namespace CoinPrediction.DAL
{
    public interface IUserRepository: IDisposable
    {
        IEnumerable<User> GetUsers();
        User? GetUserByID(Guid id);
        User InsertUser(User coin);
        bool DeleteUser(Guid id);
        User? UpdateUser(User coin);
    }
}
