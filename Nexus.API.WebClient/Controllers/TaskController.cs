using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nexus.API.Manager.Dto;
using Nexus.API.Manager.Manager;

namespace Nexus.API.WebClient.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ApiController
    {
        private readonly TaskManager _taskManager;

        public TaskController(TaskManager taskManager)
        {
            _taskManager = taskManager;
        }

        [HttpGet("GetAllTasks")]
        public async Task<IActionResult> GetAllTasks()
        {
            try
            {
                var res = await _taskManager.GetAllTasks();

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

        [HttpPost("AddTask")]
        public async Task<IActionResult> AddTask(TaskDto taskDto)
        {
            try
            {
                var res = await _taskManager.AddTask(taskDto);

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

        [HttpPut("UpdateTaskById")]
        public async Task<IActionResult> UpdateTask(int taskId, TaskDto taskDto)
        {
            try
            {
                var res = await _taskManager.UpdateTask(taskId, taskDto);

                return Ok(new
                {
                    result = true,
                    message = "Project updated Successfully",
                    data = res
                });
            }
            catch (Exception ex)
            {
                return GetExceptions(ex);
            }
        }

        [HttpDelete("DeleteTaskById")]
        public async Task<IActionResult> DeleteTask(int taskId)
        {
            try
            {
                var res = await _taskManager.DeleteTask(taskId);

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

        [HttpGet("GetTaskById")]
        public async Task<IActionResult> GetTaskById(int taskId)
        {
            try
            {
                var res = await _taskManager.GetTaskById(taskId);

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

        [HttpGet("GetTasksByProjectId")]
        public async Task<IActionResult> GetTasksByProjectId(int projectId)
        {
            try
            {
                var res = await _taskManager.GetTasksByProjectId(projectId);

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

        [HttpGet("GetTasksByUserId")]
        public async Task<IActionResult> GetTasksByUserId(int userId)
        {
            try
            {
                var res = await _taskManager.GetTasksByUserId(userId);

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

        [HttpGet("GetTasksByProjectAndUserId")]
        public async Task<IActionResult> GetTasksByProjectAndUserId(int projectId,int userId)
        {
            try
            {
                var res = await _taskManager.GetTasksByProjectAndUserId(projectId,userId);

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

        [HttpGet("GetTasksCountByProjectId")]
        public async Task<IActionResult> GetTasksCountByProjectId(int projectId)
        {
            try
            {
                var res = await _taskManager.GetTasksCountByProjectId(projectId);

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
