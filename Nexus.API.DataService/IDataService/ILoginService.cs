using Nexus.API.DataService.Models;

namespace Nexus.API.DataService.IDataService
{
    public interface ILoginService
    {
        Task<User> UserLogin(string info, string password);
        Task<User> GetUserByInfo(string info);
    }
}
