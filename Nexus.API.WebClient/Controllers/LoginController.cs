using Microsoft.AspNetCore.Mvc;
using Nexus.API.Manager.Manager;

namespace Nexus.API.WebClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ApiController
    {
        private readonly LoginManager _loginManager;

        public LoginController(LoginManager loginManager)
        {
            _loginManager = loginManager;
        }

        [HttpPost("Email/Username")]
        public async Task<IActionResult> UserLogin(string info, string password)
        {
            try
            {
                var token = await _loginManager.UserLogin(info, password);

                return Ok(new
                {
                    result = true,
                    message = "User added Successfully",
                    data = token
                });
            }
            catch (Exception ex)
            {
                return GetExceptions(ex);
            }
        }
    }
}
