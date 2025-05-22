using Microsoft.AspNetCore.Http;
using Nexus.API.DataService.IDataService;
using Nexus.API.DataService.Models;
using Nexus.API.Manager.Dto;
using Nexus.API.Manager.Validations;

namespace Nexus.API.Manager.Manager
{
    public class IssueManager
    {
        private readonly IIssueService _issueService;
        private readonly IHttpContextAccessor _accessor;
        private readonly IProjectService _projectService;
        private readonly ITaskService _taskService;

        public IssueManager(
            IIssueService issueService, 
            IHttpContextAccessor accessor,
            IProjectService projectService,
            ITaskService taskService)
        {
            _issueService = issueService;
            _accessor = accessor;
            _projectService = projectService;
            _taskService = taskService;
        }

        public async Task<List<IssueOutputDto>> GetAllIssues()
        {
            Dictionary<string, string> validations = new Dictionary<string, string>();

            var issue = await _issueService.GetAllIssues();

            if (issue != null)
            {
                var res = issue.Select(x => IssueOutputDto.MapToEntity(x)).ToList();

                return res;
            }
            else
            {
                validations.Add("Issue", "No Issues");

                throw new CustomValidation(validations);
            }
        }

        public async Task<IssueOutputDto> AddIssue(IssueDto issueDto)
        {
            Dictionary<string, string> validations = new Dictionary<string, string>();

            if (string.IsNullOrWhiteSpace(issueDto.Description))
                validations.Add("Description null","Description is missing");

            if (issueDto.Description.Length > 255)
                validations.Add("Description length", "Maximum 255 Characters are allowed");

            if (await _projectService.GetProjectById(issueDto.ProjectId) == null)
                validations.Add("ProjectId Invalid", "Project with this Id doesn't exist");

            if (await _taskService.GetTaskById(issueDto.TaskId) == null)
                validations.Add("TaskId Invalid", "task with this Id doesn't exist");

            if (validations.Count == 0)
            {
                var issue = new Issue
                {
                    Description = issueDto.Description,
                    ProjectId = issueDto.ProjectId,
                    TaskId = issueDto.TaskId,
                    CreatedOn = DateTime.UtcNow,
                    CreatedBy = int.Parse(_accessor.HttpContext.User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value)
                };

                await _issueService.AddIssue(issue);

                return IssueOutputDto.MapToEntity(issue);
            }
            else
            {
                throw new CustomValidation(validations);
            }
        }

        public async Task<IssueOutputDto> UpdateIssue(int issueId, IssueDto issueDto)
        {
            Dictionary<string, string> validations = new Dictionary<string, string>();

            var issue = await _issueService.GetIssueById(issueId);

            if (issue != null)
            {
                if (string.IsNullOrWhiteSpace(issueDto.Description))
                    validations.Add("Description null", "Description is missing");

                if (issueDto.Description.Length > 255)
                    validations.Add("Description length", "Maximum 255 Characters are allowed");
            }
            else
            {
                validations.Add("IssueId Invalid", "Issue with this Id doesn't exist");
            }

            if (validations.Count == 0)
            {
                issue.Description = issueDto.Description;
                issue.ChangedOn = DateTime.UtcNow;
                issue.ChangedBy = int.Parse(_accessor.HttpContext.User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);

                await _issueService.UpdateIssue(issue);

                return IssueOutputDto.MapToEntity(issue);
            }
            else
            {
                throw new CustomValidation(validations);
            }
        }

        public async Task<IssueOutputDto> DeleteIssue(int issueId)
        {
            Dictionary<string, string> validations = new Dictionary<string, string>();

            var issue = await _issueService.GetIssueById(issueId);

            if (issue != null)
            {
                await _issueService.DeleteIssue(issue);

                return IssueOutputDto.MapToEntity(issue);
            }
            else
            {
                validations.Add("IssueId Invalid", "Issue with this Id doesn't exist");

                throw new CustomValidation(validations);
            }
        }

        public async Task<IssueOutputDto> GetIssueById(int issueId)
        {
            Dictionary<string, string> validations = new Dictionary<string, string>();

            var issue = await _issueService.GetIssueById(issueId);

            if (issue != null)
            {
                return IssueOutputDto.MapToEntity(issue);
            }
            else
            {
                validations.Add("IssueId Invalid", "Issue with this Id doesn't exist");

                throw new CustomValidation(validations);
            }
        }
    }
}
