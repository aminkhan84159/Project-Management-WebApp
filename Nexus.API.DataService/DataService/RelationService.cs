using Microsoft.EntityFrameworkCore;
using Nexus.API.DataService.IDataService;
using Nexus.API.DataService.Models;

namespace Nexus.API.DataService.DataService
{
    public class RelationService : IRelationService
    {
        private readonly NexusContext _context;

        public RelationService(NexusContext context)
        {
            _context = context;
        }

        public async Task<List<Relation>> GetAllRelations()
        {
            var res = await _context.Relations.ToListAsync();

            return res;
        }

        public async Task<Relation> AddRelation(Relation relation)
        {
            _context.Relations.Add(relation);
            await _context.SaveChangesAsync();

            return relation;
        }

        public async Task<Relation> GetRelationById(int? userId, int? projectId)
        {
            var res = await _context.Relations.Where(x => x.UserId == userId && x.ProjectId == projectId).FirstOrDefaultAsync();

            return res;
        }

        public async Task<Relation> DeleteRelation(Relation relation)
        {
            _context.Relations.Remove(relation);
            await _context.SaveChangesAsync();

            return relation;
        }

        public async Task<List<Project>> GetProjectsByUserId(int userId)
        {
            var res = await _context.Relations.Where(x => x.UserId == userId).Select(x => x.Project).ToListAsync();

            return res;
        }

        public async Task<List<User>> GetMembersByProjectId(int projectId)
        {
            var res = await _context.Relations.Where(x => x.ProjectId == projectId && x.Role == "Member").Select(x => x.User).ToListAsync();

            return res;
        }
        public async Task<int> GetMembersCountByProjectId(int projectId)
        {
            var res = await _context.Relations.Where(x => x.ProjectId == projectId && x.Role == "Member").Select(x => x.User).CountAsync();

            return res;
        }
    }
}
