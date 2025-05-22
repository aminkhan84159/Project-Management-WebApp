using Nexus.API.DataService.Models;

namespace Nexus.API.DataService.IDataService
{
    public interface ITaskService
    {
        Task<List<Nexus.API.DataService.Models.Task>> GetAllTasks();
        Task<Nexus.API.DataService.Models.Task> AddTask(Nexus.API.DataService.Models.Task Task);
        Task<Nexus.API.DataService.Models.Task> UpdateTask(Nexus.API.DataService.Models.Task Task);
        Task<Nexus.API.DataService.Models.Task> DeleteTask(Nexus.API.DataService.Models.Task Task);
        Task<Nexus.API.DataService.Models.Task> GetTaskById(int? taskId);
        Task<List<Nexus.API.DataService.Models.Task>> GetTasksByProjectId(int projectId);
        Task<List<Nexus.API.DataService.Models.Task>> GetTasksByUsertId(int userId);
        Task<List<Nexus.API.DataService.Models.Task>> GetTasksByProjectAndUserId(int projectId, int userId);
        Task<int> GetTasksCountByProjectId(int projectId);
    }
}
