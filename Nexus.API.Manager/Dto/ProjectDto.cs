namespace Nexus.API.Manager.Dto
{
    public class ProjectDto
    {
        public string ProjectName { get; set; } = null!;
        public string? Description { get; set; }
        public DateOnly? DueDate { get; set; }
        public int? StatusId { get; set; }
    }
}
