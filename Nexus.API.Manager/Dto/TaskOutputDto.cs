namespace Nexus.API.Manager.Dto
{
    public class TaskOutputDto
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; } = null!;
        public string? Description { get; set; }
        public string Priority { get; set; } = null!;
        public string? Type { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public int StatusId { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public bool? IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedOn { get; set; }

        public static TaskOutputDto MapToEntity(Nexus.API.DataService.Models.Task task) => new TaskOutputDto
        {
            TaskId = task.TaskId,
            TaskName = task.TaskName,
            Description = task.Description,
            Priority = task.Priority,
            Type = task.Type,
            StartDate = task.StartDate,
            EndDate = task.EndDate,
            StatusId = task.StatusId,
            UserId = task.UserId,
            ProjectId = task.ProjectId,
            IsActive = task.IsActive,
            CreatedBy = task.CreatedBy,
            CreatedOn = task.CreatedOn,
            ChangedBy = task.ChangedBy,
            ChangedOn = task.ChangedOn
        };
    }
}
