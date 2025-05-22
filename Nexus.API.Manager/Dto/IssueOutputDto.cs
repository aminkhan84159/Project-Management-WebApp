using Nexus.API.DataService.Models;

namespace Nexus.API.Manager.Dto
{
    public class IssueOutputDto
    {
        public int IssueId { get; set; }
        public string? Description { get; set; }
        public int ProjectId { get; set; }
        public int TaskId { get; set; }
        public bool? IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedOn { get; set; }

        public static IssueOutputDto MapToEntity(Issue issue) => new IssueOutputDto
        {
            IssueId = issue.IssueId,
            Description = issue.Description,
            ProjectId = issue.ProjectId,
            TaskId = issue.TaskId,
            IsActive = issue.IsActive,
            CreatedBy = issue.CreatedBy,
            CreatedOn = issue.CreatedOn,
            ChangedBy = issue.ChangedBy,
            ChangedOn = issue.ChangedOn
        };
    }
}
