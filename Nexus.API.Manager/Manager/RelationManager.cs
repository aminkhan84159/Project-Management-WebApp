using Nexus.API.DataService.IDataService;
using Nexus.API.DataService.Models;
using Nexus.API.Manager.Dto;
using Nexus.API.Manager.Validations;

namespace Nexus.API.Manager.Manager
{
    public class RelationManager
    {
        private readonly IRelationService _relationService;
        private readonly IProjectService _projectService;
        private readonly IUserService _userService;

        public RelationManager(
            IRelationService relationService, 
            IProjectService projectService,
            IUserService userService)
        {
            _relationService = relationService;
            _projectService = projectService;
            _userService = userService;
        }

        public async Task<List<RelationOutputDto>> GetAllRelations()
        {
            Dictionary<string, string> validations = new Dictionary<string, string>();

            var relation = await _relationService.GetAllRelations();

            if (relation != null)
            {
                var res = relation.Select(x => RelationOutputDto.MapToEntity(x)).ToList();

                return res;
            }
            else
            {
                validations.Add("Relation", "No Relations");

                throw new CustomValidation(validations);
            }
        }

        public async Task<RelationOutputDto> AddRelation(RelationDto relationDto)
        {
            Dictionary<string, string> validations = new Dictionary<string, string>();

            if (await _userService.GetUserById(relationDto.UserId) == null)
                validations.Add("Invalid user", "User with this id doesn't exist");

            if (await _projectService.GetProjectById(relationDto.ProjectId) == null)
                validations.Add("Invalid project", "Project with this Id doesn't exist");

            if (validations.Count == 0)
            {

                var relation = new Relation
                {
                    UserId = relationDto.UserId,
                    ProjectId = relationDto.ProjectId,
                    Role = "Member"
                };

                await _relationService.AddRelation(relation);

                return RelationOutputDto.MapToEntity(relation);
            }
            else
            {
                throw new CustomValidation(validations);
            }

        }

        public async Task<RelationOutputDto> DeleteRelation(RelationDto relationDto)
        {
            Dictionary<string, string> validations = new Dictionary<string, string>();

            var relation = await _relationService.GetRelationById(relationDto.UserId, relationDto.ProjectId);

            if (relation != null)
            {
                await _relationService.DeleteRelation(relation);

                return RelationOutputDto.MapToEntity(relation);
            }
            else
            {
                validations.Add("Relation", "This relation doesn't exist");

                throw new CustomValidation(validations);
            }
        }

        public async Task<RelationOutputDto> GetRelationById(int userId, int projectId)
        {
            Dictionary<string, string> validations = new Dictionary<string, string>();

            if (await _userService.GetUserById(userId) == null)
                validations.Add("Invalid user", "User with this id doesn't exist");

            if (await _projectService.GetProjectById(projectId) == null)
                validations.Add("Invalid project", "Project with this Id doesn't exist");

            var relation = await _relationService.GetRelationById(userId,projectId);

            if (relation == null)
                validations.Add("Relation", "This relation doesn't exist");

            if (validations.Count == 0)
            {
                return RelationOutputDto.MapToEntity(relation);
            }
            else
            {
                throw new CustomValidation(validations);
            }
        }

        public async Task<List<ProjectOutputDto>> GetProjectsByUserId(int userId)
        {
            Dictionary<string, string> validations = new Dictionary<string, string>();

            if (await _userService.GetUserById(userId) == null)
                validations.Add("Invalid user", "User with this id doesn't exist");

            var project = await _relationService.GetProjectsByUserId(userId);

            if (project == null)
                validations.Add("Project", "No Projects");

            if (validations.Count == 0)
            {
                var res = project.Select(x => ProjectOutputDto.MapToEntity(x)).ToList();

                return res;
            }
            else
            {
                throw new CustomValidation(validations);
            }
        }

        public async Task<List<UserOutputDto>> GetMembersByProjectId(int projectId)
        {
            Dictionary<string, string> validations = new Dictionary<string, string>();

            if (await _projectService.GetProjectById(projectId) != null)
            {
                var members = await _relationService.GetMembersByProjectId(projectId);

                if (members == null)
                    validations.Add("Members", "There are no membrs in this project");

                if (validations.Count == 0)
                {
                    var res = members.Select(x => UserOutputDto.MapToEntity(x)).ToList();

                    return res;
                }
                else
                {
                    throw new CustomValidation(validations);
                }
            }
            else
            {
                validations.Add("Project ID Invalid","Project with this id doesn't exist");

                throw new CustomValidation(validations);
            }
        }

        public async Task<int> GetMembersCountByProjectId(int projectId)
        {
            Dictionary<string, string> validations = new Dictionary<string, string>();

            var project = await _projectService.GetProjectById(projectId);

            if (project != null)
            {
                return await _relationService.GetMembersCountByProjectId(projectId);
            }
            else
            {
                validations.Add("Project Id Invalid", "Project with this Id doesn't exist");

                throw new CustomValidation(validations);
            }
        }
    }
}
