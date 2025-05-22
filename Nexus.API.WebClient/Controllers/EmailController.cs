using Microsoft.AspNetCore.Mvc;
using Nexus.API.Manager.Dto;
using Nexus.API.Manager.Manager;

namespace Nexus.API.WebClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ApiController
    {
        private readonly EmailManager _emailManager;

        public EmailController(EmailManager emailManager)
        {
            _emailManager = emailManager;
        }

        [HttpPost("InviteUser")]
        public async Task<IActionResult> SendEmail(InviteDto inviteDto)
        {
            try
            {
                await _emailManager.SendEmail(inviteDto);

                return Ok();
            }
            catch (Exception ex)
            {
                return GetExceptions(ex);
            }
        }
    }
}
