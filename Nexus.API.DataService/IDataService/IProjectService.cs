using Nexus.API.DataService.Models;

namespace Nexus.API.DataService.IDataService
{
    public interface IProjectService
    {
        Task<List<Project>> GetAllProjects();
        Task<Project> AddProject(Project project);
        Task<Project> UpdateProject(Project project);
        Task<Project> DeleteProject(Project project);
        Task<Project> GetProjectById(int? projectId);
        Task<Project> GetProjectByName(string projectName);
    }
}
