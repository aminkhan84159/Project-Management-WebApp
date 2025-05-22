using Microsoft.AspNetCore.Http;
using Nexus.API.DataService.DataService;
using Nexus.API.DataService.IDataService;
using Nexus.API.Manager.Dto;
using Nexus.API.Manager.Validations;
using System.Text.RegularExpressions;

namespace Nexus.API.Manager.Manager
{
    public class TaskManager
    {
        private readonly ITaskService _taskService;
        private readonly IHttpContextAccessor _accessor;
        private readonly IProjectService _projectService;
        private readonly IUserService _userService;
        private readonly IStatusService _statusService;

        public TaskManager(
            ITaskService taskService, 
            IHttpContextAccessor accessor, 
            IProjectService projectService, 
            IUserService userService,
            IStatusService statusService)
        {
            _taskService = taskService;
            _accessor = accessor;
            _projectService = projectService;
            _userService = userService;
            _statusService = statusService;
        }

        public async Task<List<TaskOutputDto>> GetAllTasks()
        {
            Dictionary<string, string> validations = new Dictionary<string, string>();

            var task = await _taskService.GetAllTasks();

            if (task != null)
            {
                var res = task.Select(x => TaskOutputDto.MapToEntity(x)).ToList();

                return res;
            }
            else
            {
                validations.Add("Task", "No Tasks");

                throw new CustomValidation(validations);
            }
        }

        public async Task<TaskOutputDto> AddTask(TaskDto taskDto)
        {
            Dictionary<string, string> validations = new Dictionary<string, string>();

            if (string.IsNullOrWhiteSpace(taskDto.TaskName))
                validations.Add("TaskName null", "TaskName is missing");

            if (taskDto.TaskName.Length > 50)
                validations.Add("TaskName length", "Maximum 50 Characters are allowed");

            if (string.IsNullOrWhiteSpace(taskDto.Description))
                validations.Add("Description null", "Description is missing");

            if (taskDto.Description.Length > 255)
                validations.Add("Description length", "Maximum 255 Characters are allowed");

            Regex priorityRegex = new Regex(@"^([Low]|[Medium]|[High])+$");
            Match priority = priorityRegex.Match(taskDto.Priority);

            if (priority.Success == false)
                validations.Add("Priority Invalid", "Only contains (Low,Medium,High) values");

            if (string.IsNullOrWhiteSpace(taskDto.Priority))
                validations.Add("Priority null", "Priority is missing");

            if (taskDto.Priority.Length > 30)
                validations.Add("Priority length", "Maximum 30 Characters are allowed");

            if (await _statusService.GetStatusById(taskDto.StatusId) == null)
                validations.Add("StatusId Invalid", "Status with this Id doesn't exist");

            if (await _userService.GetUserById(taskDto.UserId) == null)
                validations.Add("MemberId Invalid", "Member with this Id doesn't exist");

            if (await _projectService.GetProjectById(taskDto.ProjectId) == null)
                validations.Add("ProjectId Invalid","Project with this ID doesn't exist");

            /*Regex dateRegex = new Regex(@"\b(((0?[469]|11)/(0?[1-9]|[12]\d|30)|(0?[13578]|1[02])/(0?[1-9]|[12]\d|3[01])|0?2/(0?[1-9]|1\d|2[0-8]))/([1-9]\d{3}|\d{2})|0?2/29/([1-9]\d)?([02468][048]|[13579][26]))\b", RegexOptions.ECMAScript | RegexOptions.ExplicitCapture);
            Match date = dateRegex.Match(taskDto.EndtDate);*/

            if (validations.Count == 0)
            {
                var task = new Nexus.API.DataService.Models.Task
                {
                    TaskName = taskDto.TaskName,
                    Description = taskDto.Description,
                    Priority = taskDto.Priority,
                    Type = taskDto.Type,
                    StartDate = DateOnly.FromDateTime(DateTime.Now),
                    EndDate = taskDto.EndDate,
                    StatusId = taskDto.StatusId,
                    UserId = taskDto.UserId,
                    ProjectId = taskDto.ProjectId,
                    CreatedOn = DateTime.UtcNow,
                    CreatedBy = int.Parse(_accessor.HttpContext.User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value)
                };

                await _taskService.AddTask(task);

                return TaskOutputDto.MapToEntity(task);
            }
            else
            {
                throw new CustomValidation(validations);
            }
        }

        public async Task<TaskOutputDto> UpdateTask(int taskId, TaskDto taskDto)
        {
            Dictionary<string, string> validations = new Dictionary<string, string>();

            var task = await _taskService.GetTaskById(taskId);

            if (task != null)
            {
                if (string.IsNullOrWhiteSpace(taskDto.TaskName))
                    validations.Add("TaskName null", "TaskName is missing");

                if (taskDto.TaskName.Length > 50)
                    validations.Add("TaskName length", "Maximum 50 Characters are allowed");

                if (string.IsNullOrWhiteSpace(taskDto.Description))
                    validations.Add("Description null", "Description is missing");

                if (taskDto.Description.Length > 255)
                    validations.Add("Description length", "Maximum 255 Characters are allowed");

                Regex priorityRegex = new Regex(@"^([Low]|[Medium]|[High])+$");
                Match priority = priorityRegex.Match(taskDto.Priority);

                if (priority.Success == false)
                    validations.Add("Priority Invalid", "Only contains (Low,Medium,High) values");

                if (string.IsNullOrWhiteSpace(taskDto.Priority))
                    validations.Add("Priority null", "Priority is missing");

                if (taskDto.Priority.Length > 30)
                    validations.Add("Priority length", "Maximum 30 Characters are allowed");

                if (taskDto.StatusId != task.StatusId)
                {
                    if (await _statusService.GetStatusById(taskDto.StatusId) == null)
                        validations.Add("StatusId Invalid", "Status with this Id doesn't exist");
                }

                if (taskDto.UserId != task.UserId)
                {
                    if (await _userService.GetUserById(taskDto.UserId) == null)
                        validations.Add("MemberId Invalid", "Member with this Id doesn't exist");
                }
            }
            else
            {
                validations.Add("TaskId Invalid", "Task with this Id doesn't exist");
            }

            if (validations.Count == 0)
            {
                task.TaskName = taskDto.TaskName;
                task.Description = taskDto.Description;
                task.Priority = taskDto.Priority;
                task.Type = taskDto.Type;
                task.EndDate = taskDto.EndDate;
                task.StatusId = taskDto.StatusId;
                task.UserId = taskDto.UserId;
                task.ChangedOn = DateTime.UtcNow;
                task.ChangedBy = int.Parse(_accessor.HttpContext.User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);

                await _taskService.UpdateTask(task);

                return TaskOutputDto.MapToEntity(task);
            }
            else
            {
                throw new CustomValidation(validations);
            }
        }

        public async Task<TaskOutputDto> DeleteTask(int taskId)
        {
            Dictionary<string, string> validations = new Dictionary<string, string>();

            var task = await _taskService.GetTaskById(taskId);

            if (task != null)
            {
                await _taskService.DeleteTask(task);

                return TaskOutputDto.MapToEntity(task);
            }
            else
            {
                validations.Add("TaskId Invalid", "Task with this Id doesn't exist");

                throw new CustomValidation(validations);
            }
        }

        public async Task<TaskOutputDto> GetTaskById(int taskId)
        {
            Dictionary<string, string> validations = new Dictionary<string, string>();

            var task = await _taskService.GetTaskById(taskId);

            if (task != null)
            {
                return TaskOutputDto.MapToEntity(task);
            }
            else
            {
                validations.Add("TaskId Invalid", "Task with this Id doesn't exist");

                throw new CustomValidation(validations);
            }
        }

        public async Task<List<TaskOutputDto>> GetTasksByProjectId(int projectId)
        {
            Dictionary<string, string> validations = new Dictionary<string, string>();

            if (await _projectService.GetProjectById(projectId) != null)
            {
                var tasks = await _taskService.GetTasksByProjectId(projectId);

                if (tasks == null)
                    validations.Add("No Tasks in project", "There are no tasks in this project");

                if (validations.Count == 0)
                {
                    var res = tasks.Select(x => TaskOutputDto.MapToEntity(x)).ToList();

                    return res;
                }
                else
                {
                    throw new CustomValidation(validations);
                }
            }
            else
            {
                validations.Add("Project Id Invalid", "Project with this Id doens't exist");

                throw new CustomValidation(validations);
            }
        }

        public async Task<List<TaskOutputDto>> GetTasksByUserId(int userId)
        {
            Dictionary<string, string> validations = new Dictionary<string, string>();

            if (await _userService.GetUserById(userId) != null)
            {
                var tasks = await _taskService.GetTasksByUsertId(userId);

                if (tasks == null)
                    validations.Add("No Tasks in project", "There are no tasks in this project");

                if (validations.Count == 0)
                {
                    var res = tasks.Select(x => TaskOutputDto.MapToEntity(x)).ToList();

                    return res;
                }
                else
                {
                    throw new CustomValidation(validations);
                }
            }
            else
            {
                validations.Add("User Id Invalid", "User with this Id doens't exist");

                throw new CustomValidation(validations);
            }
        }

        public async Task<List<TaskOutputDto>> GetTasksByProjectAndUserId(int projectId, int userId)
        {
            Dictionary<string, string> validations = new Dictionary<string, string>();

            if (await _projectService.GetProjectById(projectId) != null)
            {
                var tasks = await _taskService.GetTasksByProjectAndUserId(projectId,userId);

                if (tasks == null)
                    validations.Add("No Tasks with this userId", "There are no tasks for this user");

                if (validations.Count == 0)
                {
                    var res = tasks.Select(x => TaskOutputDto.MapToEntity(x)).ToList();

                    return res;
                }
                else
                {
                    throw new CustomValidation(validations);
                }
            }
            else
            {
                validations.Add("Project Id Invalid", "Project with this Id doens't exist");

                throw new CustomValidation(validations);
            }
        }

        public async Task<int> GetTasksCountByProjectId(int projectId)
        {
            Dictionary<string, string> validations = new Dictionary<string, string>();

            var project = await _projectService.GetProjectById(projectId);

            if (project != null)
            {
                return await _taskService.GetTasksCountByProjectId(projectId);
            }
            else
            {
                validations.Add("Project Id Invalid", "Project with this Id doesn't exist");

                throw new CustomValidation(validations);
            }
        }
    }
}
