using Nexus.API.DataService.Models;

namespace Nexus.API.DataService.IDataService
{
    public interface IUserDetailService
    {
        Task<List<UserDetail>> GetAllUserDetails();
        Task<UserDetail> AddUserDetail(UserDetail userDetail);
        Task<UserDetail> UpdateUserDetail(UserDetail userDetail);
        Task<UserDetail> DeleteUserDetail(UserDetail userDetail);
        Task<UserDetail> GetUserDetailById(int? userDetailId);
        Task<UserDetail> GetUserDetailByPhoneNo(string  phoneNo);
        Task<UserDetail> GetUserDetailByUserId(int? userId);
    }
}
