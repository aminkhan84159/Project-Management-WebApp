using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nexus.API.Manager.Dto;
using Nexus.API.Manager.Manager;

namespace Nexus.API.WebClient.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RelationController : ApiController
    {
        private readonly RelationManager _relationManager;

        public RelationController(RelationManager relationManager)
        {
            _relationManager = relationManager;
        }

        [HttpGet("GetAllRelations")]
        public async Task<IActionResult> GetAllRelations()
        {
            try
            {
                var res = await _relationManager.GetAllRelations();

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

        [HttpPost("AddRelation")]
        public async Task<IActionResult> AddRelation(RelationDto relationDto)
        {
            try
            {
                var res = await _relationManager.AddRelation(relationDto);

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

        [HttpDelete("DeleteRelationById")]
        public async Task<IActionResult> DeleteRelation(RelationDto relationDto)
        {
            try
            {
                var res = await _relationManager.DeleteRelation(relationDto);

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

        [HttpGet("GetRelationById")]
        public async Task<IActionResult> GetRelationById(int userId, int projectId)
        {
            try
            {
                var res = await _relationManager.GetRelationById(userId,projectId);

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

        [HttpGet("GetProjectsByUserId")]
        public async Task<IActionResult> GetProjectByUserId(int userId)
        {
            try
            {
                var res = await _relationManager.GetProjectsByUserId(userId);

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

        [HttpGet("GetMembersByProjectId")]
        public async Task<IActionResult> GetMembersByProjectId(int projectId)
        {
            try
            {
                var res = await _relationManager.GetMembersByProjectId(projectId);

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

        [HttpGet("GetMembersCountByProjectId")]
        public async Task<IActionResult> GetMembersCountByProjectId(int projectId)
        {
            try
            {
                var res = await _relationManager.GetMembersCountByProjectId(projectId);

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
