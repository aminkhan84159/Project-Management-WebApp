namespace Nexus.API.Manager.Dto
{
    public class TaskDto
    {
        public string TaskName { get; set; } = null!;
        public string? Description { get; set; }
        public string Priority { get; set; } = null!;
        public string? Type { get; set; }
        public int StatusId { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
    }
}
