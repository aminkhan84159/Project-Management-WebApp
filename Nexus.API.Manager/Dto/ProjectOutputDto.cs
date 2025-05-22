using Nexus.API.DataService.Models;

namespace Nexus.API.Manager.Dto
{
    public class ProjectOutputDto
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; } = null!;
        public string? Description { get; set; }
        public DateOnly? DueDate { get; set; }
        public int? StatusId { get; set; }
        public bool? IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedOn { get; set; }

        public static ProjectOutputDto MapToEntity(Project project) => new ProjectOutputDto
        {
            ProjectId = project.ProjectId,
            ProjectName = project.ProjectName,
            Description = project.Description,
            DueDate = project.DueDate,
            StatusId = project.StatusId,
            IsActive = project.IsActive,
            CreatedBy = project.CreatedBy,
            CreatedOn = project.CreatedOn,
            ChangedBy = project.ChangedBy,
            ChangedOn = project.ChangedOn
        };
    }
}
