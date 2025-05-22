using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nexus.API.Manager.Dto;
using Nexus.API.Manager.Manager;

namespace Nexus.API.WebClient.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ApiController
    {
        private readonly ProjectManager _projectManager;

        public ProjectController(ProjectManager projectManager)
        {
            _projectManager = projectManager;
        }

        [HttpGet("GetAllProjects")]
        public async Task<IActionResult> GetAllProjects()
        {
            try
            {
                var res = await _projectManager.GetAllProjects();

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

        [HttpPost("AddProject")]
        public async Task<IActionResult> AddProject(ProjectDto projectDto)
        {
            try
            {
                var res = await _projectManager.AddProject(projectDto);

                return Ok(new
                {
                    result = true,
                    message = "Project added Successfully",
                    data = res
                });
            }
            catch (Exception ex)
            {
                return GetExceptions(ex);
            }
        }

        [HttpPut("UpdateProjectById")]
        public async Task<IActionResult> UpdateProject(int projectId, ProjectDto projectDto)
        {
            try
            {
                var res = await _projectManager.UpdateProject(projectId, projectDto);

                return Ok(new
                {
                    result = true,
                    message = "Project Updated Successfully",
                    data = res
                });
            }
            catch (Exception ex)
            {
                return GetExceptions(ex);
            }
        }

        [HttpDelete("DeleteProjectById")]
        public async Task<IActionResult> DeleteProject(int projectId)
        {
            try
            {
                var res = await _projectManager.DeleteProject(projectId);

                return Ok(new
                {
                    result = true,
                    message = "Project deleted Successfully",
                    data = res
                });
            }
            catch (Exception ex)
            {
                return GetExceptions(ex);
            }
        }

        [HttpGet("GetProjectById")]
        public async Task<IActionResult> GetProjectById(int projectId)
        {
            try
            {
                var res = await _projectManager.GetprojectById(projectId);

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
