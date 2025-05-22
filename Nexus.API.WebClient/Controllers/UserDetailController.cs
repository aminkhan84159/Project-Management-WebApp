using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nexus.API.Manager.Dto;
using Nexus.API.Manager.Manager;

namespace Nexus.API.WebClient.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailController : ApiController
    {
        private readonly UserDetailManager _userDetailManager;

        public UserDetailController(UserDetailManager userDetailManager)
        {
            _userDetailManager = userDetailManager;
        }

        [HttpGet("GetAllUserDetails")]
        public async Task<IActionResult> GetAllUserDetails()
        {
            try
            {
                var res = await _userDetailManager.GetAllUserDetails();

                return Ok(new
                {
                    result = true,
                    message = "Success",
                    data = res
                });
            }
            catch (Exception ex)
            {
                return GetExceptions(ex);
            }
        }

        [AllowAnonymous]
        [HttpPost("AddUserDetails")]
        public async Task<IActionResult> AddUserDetail(UserDetailDto userDetailDto)
        {
            try
            {
                var res = await _userDetailManager.AddUserDetail(userDetailDto);

                return Ok(new
                {
                    result = true,
                    message = "User added Successfully",
                    data = res
                });
            }
            catch (Exception ex)
            {
                return GetExceptions(ex);
            }
        }

        [HttpPut("UpdateUserDetailsById")]
        public async Task<IActionResult> UpdateUserDetail(int userDetailId, UserDetailDto userDetailDto)
        {
            try
            {
                var res = await _userDetailManager.UpdateUserDetail(userDetailId, userDetailDto);

                return Ok(new
                {
                    result = true,
                    message = "User updated Successfully",
                    data = res
                });
            }
            catch (Exception ex)
            {
                return GetExceptions(ex);
            }
        }

        [HttpDelete("DeleteUserDetailsById")]
        public async Task<IActionResult> DeleteUserDetail(int userDetailId)
        {
            try
            {
                var res = await _userDetailManager.DeleteUserDetail(userDetailId);

                return Ok(new
                {
                    result = true,
                    message = "User deleted Successfully",
                    data = res
                });
            }
            catch (Exception ex)
            {
                return GetExceptions(ex);
            }
        }

        [HttpGet("GetUserDetailsById")]
        public async Task<IActionResult> GetUserDetailById(int userDetailId)
        {
            try
            {
                var res = await _userDetailManager.GetUserDetailById(userDetailId);

                return Ok(new
                {
                    result = true,
                    message = "Success",
                    data = res
                });
            }
            catch (Exception ex)
            {
                return GetExceptions(ex);
            }
        }

        [HttpGet("GetUserDetailsByUserId")]
        public async Task<IActionResult> GetUserDetailByUserId(int userId)
        {
            try
            {
                var res = await _userDetailManager.GetUserDetailByUserId(userId);

                return Ok(new
                {
                    result = true,
                    message = "Success",
                    data = res
                });
            }
            catch (Exception ex)
            {
                return GetExceptions(ex);
            }
        }
    }
}
