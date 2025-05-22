using Microsoft.EntityFrameworkCore;
using Nexus.API.DataService.IDataService;
using Nexus.API.DataService.Models;

namespace Nexus.API.DataService.DataService
{
    public class LoginService : ILoginService
    {
        private readonly NexusContext _context;

        public LoginService(NexusContext context)
        {
            _context = context;
        }

        public async Task<User> UserLogin(string info, string password)
        {
            var user = await _context.Users.Where(x => x.Email == info && x.Password == password || x.Username == info && x.Password == password).FirstOrDefaultAsync();

            return user;
        }

        public async Task<User> GetUserByInfo(string info)
        {
            var user = await _context.Users.Where(x => x.Email == info || x.Username == info).FirstOrDefaultAsync();

            return user;
        }
    }
}
