using Microsoft.EntityFrameworkCore;
using Nexus.API.DataService.IDataService;
using Nexus.API.DataService.Models;

namespace Nexus.API.DataService.DataService
{
    public class UserDetailService : IUserDetailService
    {
        private readonly NexusContext _context;

        public UserDetailService(NexusContext context)
        {
            _context = context;
        }

        public async Task<List<UserDetail>> GetAllUserDetails()
        {
            var res = await _context.UserDetails.ToListAsync();

            return res;
        }

        public async Task<UserDetail> AddUserDetail(UserDetail userDetail)
        {
            _context.UserDetails.Add(userDetail);
            await _context.SaveChangesAsync();

            return userDetail;
        }

        public async Task<UserDetail> UpdateUserDetail(UserDetail userDetail)
        {
            _context.UserDetails.Update(userDetail);
            await _context.SaveChangesAsync();

            return userDetail;
        }

        public async Task<UserDetail> DeleteUserDetail(UserDetail userDetail)
        {
            _context.UserDetails.Remove(userDetail);
            await _context.SaveChangesAsync();

            return userDetail;
        }

        public async Task<UserDetail> GetUserDetailById(int? userDetailId)
        {
            var res = await _context.UserDetails.FindAsync(userDetailId);

            return res;
        }

        public async Task<UserDetail> GetUserDetailByPhoneNo(string phoneNo)
        {
            var res = await _context.UserDetails.Where(x => x.PhoneNo == phoneNo).FirstOrDefaultAsync();

            return res;
        }

        public async Task<UserDetail> GetUserDetailByUserId(int? userId)
        {
            var res = await _context.UserDetails.Where(x => x.UserId == userId).FirstOrDefaultAsync();

            return res;
        }
    }
}
