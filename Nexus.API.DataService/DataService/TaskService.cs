using Microsoft.EntityFrameworkCore;
using Nexus.API.DataService.IDataService;
using Nexus.API.DataService.Models;

namespace Nexus.API.DataService.DataService
{
    public class TaskService : ITaskService
    {
        private readonly NexusContext _context;

        public TaskService(NexusContext context)
        {
            _context = context;
        }

        public async Task<List<Nexus.API.DataService.Models.Task>> GetAllTasks()
        {
            var res = await _context.Tasks.ToListAsync();

            return res;
        }

        public async Task<Nexus.API.DataService.Models.Task> AddTask(Nexus.API.DataService.Models.Task task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return task;
        }

        public async Task<Nexus.API.DataService.Models.Task> UpdateTask(Nexus.API.DataService.Models.Task task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();

            return task;
        }

        public async Task<Nexus.API.DataService.Models.Task> DeleteTask(Nexus.API.DataService.Models.Task task)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return task;
        }

        public async Task<Nexus.API.DataService.Models.Task> GetTaskById(int? taskId)
        {
            var res = await _context.Tasks.FindAsync(taskId);

            return res;
        }
        public async Task<List<Nexus.API.DataService.Models.Task>> GetTasksByProjectId(int projectId)
        {
            var res = await _context.Tasks.Where(x => x.ProjectId == projectId).ToListAsync();

            return res;
        }

        public async Task<List<Nexus.API.DataService.Models.Task>> GetTasksByUsertId(int userId)
        {
            var res = await _context.Tasks.Where(x => x.UserId == userId).ToListAsync();

            return res;
        }

        public async Task<List<Nexus.API.DataService.Models.Task>> GetTasksByProjectAndUserId(int projectId, int userId)
        {
            var res = await _context.Tasks.Where(x => x.ProjectId == projectId && x.UserId == userId).ToListAsync();

            return res;
        }

        public async Task<int> GetTasksCountByProjectId(int projectId)
        {
            var res = await _context.Tasks.Where(x => x.ProjectId == projectId).Select(x => x.TaskId).CountAsync();

            return res;
        }
    }
}
