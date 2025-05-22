using Microsoft.EntityFrameworkCore;
using Nexus.API.DataService.IDataService;
using Nexus.API.DataService.Models;

namespace Nexus.API.DataService.DataService
{
    public class ProjectService : IProjectService
    {
        private readonly NexusContext _context;

        public ProjectService(NexusContext context)
        {
            _context = context;
        }

        public async Task<List<Project>> GetAllProjects()
        {
            var res = await _context.Projects.ToListAsync();

            return res;
        }

        public async Task<Project> AddProject(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return project;
        }

        public async Task<Project> UpdateProject(Project project)
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();

            return project;
        }

        public async Task<Project> DeleteProject(Project project)
        {
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return project;
        }

        public async Task<Project> GetProjectById(int? projectId)
        {
            var res = await _context.Projects.FindAsync(projectId);

            return res;
        }

        public async Task<Project> GetProjectByName(string projectName)
        {
            var res = await _context.Projects.Where(x => x.ProjectName ==  projectName).FirstOrDefaultAsync();

            return res;
        }
    }
}
