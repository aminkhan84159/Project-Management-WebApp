using Microsoft.EntityFrameworkCore;
using Nexus.API.DataService.IDataService;
using Nexus.API.DataService.Models;

namespace Nexus.API.DataService.DataService
{
    public class UserService : IUserService
    {
        private readonly NexusContext _context;

        public UserService(NexusContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var res = await _context.Users.ToListAsync();

            return res;
        }

        public async Task<User> AddUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User> UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User> DeleteUser(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User> GetUserById(int? userId)
        {
            var res = await _context.Users.FindAsync(userId);

            return res;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var res = await _context.Users.Where(x => x.Email == email).FirstOrDefaultAsync();

            return res;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            var res = await _context.Users.Where(x =>x.Username == username).FirstOrDefaultAsync();

            return res;
        }
    }
}
