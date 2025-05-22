using Microsoft.AspNetCore.Http;
using Nexus.API.DataService.IDataService;
using Nexus.API.DataService.Models;
using Nexus.API.Manager.Dto;
using Nexus.API.Manager.Validations;

namespace Nexus.API.Manager.Manager
{
    public class ProjectManager 
    {
        private readonly IProjectService _projectService;
        private readonly IHttpContextAccessor _accessor;
        private readonly IStatusService _statusService;
        private readonly IRelationService _relationService;

        public ProjectManager(
            IProjectService projectService, 
            IHttpContextAccessor accessor,
            IStatusService statusService,
            IRelationService relationService)
        {
            _projectService = projectService;
            _accessor = accessor;
            _statusService = statusService;
            _relationService = relationService;
        }

        public async Task<List<ProjectOutputDto>> GetAllProjects()
        {
            Dictionary<string, string> validations = new Dictionary<string, string>();

            var project = await _projectService.GetAllProjects();

            if (project != null)
            {
                var res = project.Select(x => ProjectOutputDto.MapToEntity(x)).ToList();

                return res;
            }
            else
            {
                validations.Add("Project", "No Projects");

                throw new CustomValidation(validations);
            }
        }

        public async Task<ProjectOutputDto> AddProject(ProjectDto projectDto)
        {
            Dictionary<string, string> validations = new Dictionary<string, string>();

            if (await _projectService.GetProjectByName(projectDto.ProjectName) != null)
                validations.Add("Project Name exist", "Project Name already exist , choose another name");

            if (string.IsNullOrWhiteSpace(projectDto.ProjectName))
                validations.Add("Project Name null", "project name is missing");

            if (projectDto.ProjectName.Length > 80)
                validations.Add("Project Name length", "Maximum 80 Characters are allowed");

            if (projectDto.Description.Length > 255)
                validations.Add("Description length", "Maximum 255 Characters are allowed");

            if (await _statusService.GetStatusById(projectDto.StatusId) == null)
                validations.Add("Status Id Invalid", "Status with this Id doesn't exist");

            if (validations.Count == 0)
            {
                var project = new Project
                {
                    ProjectName = projectDto.ProjectName,
                    Description = projectDto.Description,
                    DueDate = projectDto.DueDate,
                    StatusId = projectDto.StatusId,
                    CreatedOn = DateTime.UtcNow,
                    CreatedBy = int.Parse(_accessor.HttpContext.User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value)
                };

                var currentProject = await _projectService.AddProject(project);

                var relation = new Relation
                {
                    UserId = int.Parse(_accessor.HttpContext.User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value),
                    ProjectId = currentProject.ProjectId,
                    Role = "Admin"
                };

                await _relationService.AddRelation(relation);


                return ProjectOutputDto.MapToEntity(project);
            }
            else
            {
                throw new CustomValidation(validations);
            }
        }

        public async Task<ProjectOutputDto> UpdateProject(int projectId, ProjectDto projectDto)
        {
            Dictionary<string, string> validations = new Dictionary<string, string>();

            var project = await _projectService.GetProjectById(projectId);

            if (project != null)
            {
                if (await _projectService.GetProjectByName(projectDto.ProjectName) != null)
                    validations.Add("Project Name exist", "Project Name already exist , choose another name");

                if (string.IsNullOrWhiteSpace(projectDto.ProjectName))
                    validations.Add("Project Name null", "project name is missing");

                if (projectDto.ProjectName.Length > 80)
                    validations.Add("Project Name length", "Maximum 80 Characters are allowed");

                if (projectDto.Description.Length > 255)
                    validations.Add("Description length", "Maximum 255 Characters are allowed");

                if (projectDto.StatusId != project.StatusId)
                {
                    if (await _statusService.GetStatusById(projectDto.StatusId) != null)
                        validations.Add("Status Id Invalid", "Status with this Id doesn't exist");
                }
            }
            else
            {
                validations.Add("ProjectId Invalid", "Project with this Id doesn't exist");
            }

            if (validations.Count == 0)
            {
                project.ProjectName = projectDto.ProjectName;
                project.Description = projectDto.Description;
                project.StatusId = projectDto.StatusId;
                project.ChangedOn = DateTime.UtcNow;
                project.ChangedBy = int.Parse(_accessor.HttpContext.User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);

                await _projectService.UpdateProject(project);

                return ProjectOutputDto.MapToEntity(project);
            }
            else
            {
                throw new CustomValidation(validations);
            }
        }

        public async Task<ProjectOutputDto> DeleteProject(int projectId)
        {
            Dictionary<string, string> validations = new Dictionary<string, string>();

            var project = await _projectService.GetProjectById(projectId);

            if (project != null)
            {
                await _projectService.DeleteProject(project);

                return ProjectOutputDto.MapToEntity(project);
            }
            else
            {
                validations.Add("Project", "Project with this Id doesn't exist");

                throw new CustomValidation(validations);
            }
        }

        public async Task<ProjectOutputDto> GetprojectById(int projectId)
        {
            Dictionary<string, string> validations = new Dictionary<string, string>();

            var project = await _projectService.GetProjectById(projectId);

            if (project != null)
            {
                return ProjectOutputDto.MapToEntity(project);
            }
            else
            {
                validations.Add("Project", "Project with this Id doesn't exist");

                throw new CustomValidation(validations);
            }
        }
    }
}
