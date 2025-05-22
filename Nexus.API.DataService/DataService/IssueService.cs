using Microsoft.EntityFrameworkCore;
using Nexus.API.DataService.IDataService;
using Nexus.API.DataService.Models;

namespace Nexus.API.DataService.DataService
{
    public class IssueService : IIssueService
    {
        private readonly NexusContext _context;

        public IssueService(NexusContext context)
        {
            _context = context;
        }

        public async Task<List<Issue>> GetAllIssues()
        {
            var res = await _context.Issues.ToListAsync();

            return res;
        }

        public async Task<Issue> AddIssue(Issue issue)
        {
            _context.Issues.Add(issue);
            await _context.SaveChangesAsync();

            return issue;
        }

        public async Task<Issue> UpdateIssue(Issue issue)
        {
            _context.Issues.Update(issue);
            await _context.SaveChangesAsync();

            return issue;
        }

        public async Task<Issue> DeleteIssue(Issue issue)
        {
            _context.Issues.Remove(issue);
            await _context.SaveChangesAsync();

            return issue;
        }

        public async Task<Issue> GetIssueById(int? issueId)
        {
            var res = await _context.Issues.FindAsync(issueId);

            return res;
        }
    }
}
