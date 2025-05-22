using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nexus.API.Manager.Dto;
using Nexus.API.Manager.Manager;

namespace Nexus.API.WebClient.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class IssueController : ApiController
    {
        private readonly IssueManager _issueManager;

        public IssueController(IssueManager issueManager)
        {
            _issueManager = issueManager;
        }

        [HttpGet("GetAllIssues")]
        public async Task<IActionResult> GetAllIssues()
        {
            try
            {
                var res = await _issueManager.GetAllIssues();

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

        [HttpPost("AddIssue")]
        public async Task<IActionResult> AddIssue(IssueDto issueDto)
        {
            try
            {
                var res = await _issueManager.AddIssue(issueDto);

                return Ok(new
                {
                    result = true,
                    message = "Issue Added Successfully",
                    data = res
                });
            }
            catch (Exception ex)
            {
                return GetExceptions(ex);
            }
        }

        [HttpPut("UpdateIssueById")]
        public async Task<IActionResult> UpdateIssue(int issueId, IssueDto issueDto)
        {
            try
            {
                var res = await _issueManager.UpdateIssue(issueId, issueDto);

                return Ok(new
                {
                    result = true,
                    message = "Issue Updated Successfully",
                    data = res
                });
            }
            catch (Exception ex)
            {
                return GetExceptions(ex);
            }
        }

        [HttpDelete("DeleteIssueById")]
        public async Task<IActionResult> DeleteIssue(int issueId)
        {
            try
            {
                var res = await _issueManager.DeleteIssue(issueId);

                return Ok(new
                {
                    result = true,
                    message = "Issue deleted Successfully",
                    data = res
                });
            }
            catch (Exception ex)
            {
                return GetExceptions(ex);
            }
        }

        [HttpGet("GetIssueById")]
        public async Task<IActionResult> GetIssueById(int issueId)
        {
            try
            {
                var res = await _issueManager.GetIssueById(issueId);

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
