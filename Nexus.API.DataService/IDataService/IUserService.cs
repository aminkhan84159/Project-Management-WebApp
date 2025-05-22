using Nexus.API.DataService.Models;

namespace Nexus.API.DataService.IDataService
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();
        Task<User> AddUser(User user);
        Task<User> UpdateUser(User user);
        Task<User> DeleteUser(User user);
        Task<User> GetUserById(int? userId);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByUsername(string username);
    }
}
