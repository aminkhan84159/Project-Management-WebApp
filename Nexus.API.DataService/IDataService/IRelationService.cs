using Nexus.API.DataService.Models;

namespace Nexus.API.DataService.IDataService
{
    public interface IRelationService
    {
        Task<List<Relation>> GetAllRelations();
        Task<Relation> AddRelation(Relation relation);
        Task<Relation> GetRelationById(int? userId, int? projectId);
        Task<Relation> DeleteRelation(Relation relation);
        Task<List<Project>> GetProjectsByUserId(int userId);
        Task<List<User>> GetMembersByProjectId(int projectId);
        Task<int> GetMembersCountByProjectId(int projectId);
    }
}
