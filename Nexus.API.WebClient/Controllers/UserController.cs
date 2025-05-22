using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nexus.API.Manager.Dto;
using Nexus.API.Manager.Manager;

namespace Nexus.API.WebClient.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ApiController
    {
        private readonly UserManager _userManager;

        public UserController(UserManager userManager)
        {
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var res = await _userManager.GetAllUsers();

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
        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(UserDto userDto)
        {
            try
            {
                var res = await _userManager.AddUser(userDto);

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

        [HttpPut("UpdateUserById")]
        public async Task<IActionResult> UpdateUser(int userId, UserDto userDto)
        {
            try
            {
                var res = await _userManager.UpdateUser(userId, userDto);

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

        [HttpDelete("DeleteUserById")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            try
            {
                var res = await _userManager.DeleteUser(userId);

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

        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            try
            {
                var res = await _userManager.GetUserById(userId);

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
