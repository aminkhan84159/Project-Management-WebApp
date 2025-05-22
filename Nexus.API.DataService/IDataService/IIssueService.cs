using Nexus.API.DataService.Models;

namespace Nexus.API.DataService.IDataService
{
    public interface IIssueService
    {
        Task<List<Issue>> GetAllIssues();
        Task<Issue> AddIssue(Issue issue);
        Task<Issue> UpdateIssue(Issue issue);
        Task<Issue> DeleteIssue(Issue issue);
        Task<Issue> GetIssueById(int? issueId);
    }
}
