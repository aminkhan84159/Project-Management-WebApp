using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nexus.API.Manager.Dto;
using Nexus.API.Manager.Manager;

namespace Nexus.API.WebClient.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ApiController
    {
        private readonly StatusManager _statusManager;

        public StatusController(StatusManager statusManager)
        {
            _statusManager = statusManager;
        }

        [HttpGet("GetAllStatus")]
        public async Task<IActionResult> GetAllStatus()
        {
            try
            {
                var res = await _statusManager.GetAllStatus();

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

        [HttpPost("AddStatus")]
        public async Task<IActionResult> AddStatus(StatusDto statusDto)
        {
            try
            {
                var res = await _statusManager.AddStatus(statusDto);

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

        [HttpPut("UpdateStatusById")]
        public async Task<IActionResult> UpdateStatus(int statusId, StatusDto statusDto)
        {
            try
            {
                var res = await _statusManager.UpdateStatus(statusId, statusDto);

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
        
        [HttpDelete("DeleteStatusById")]
        public async Task<IActionResult> DeleteStatus(int statusId)
        {
            try
            {
                var res = await _statusManager.DeleteStatus(statusId);

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

        [HttpGet("GetStatusById")]
        public async Task<IActionResult> GetStatusById(int statusId)
        {
            try
            {
                var res = await _statusManager.GetStatusById(statusId);

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
