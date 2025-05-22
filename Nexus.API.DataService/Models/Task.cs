namespace Nexus.API.DataService.Models;

public partial class Task
{
    public int TaskId { get; set; }
    public string TaskName { get; set; } = null!;
    public string? Description { get; set; }
    public string Priority { get; set; } = null!;
    public string? Type { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public int StatusId { get; set; }
    public int UserId { get; set; }
    public int ProjectId { get; set; }
    public bool? IsActive { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public int? ChangedBy { get; set; }
    public DateTime? ChangedOn { get; set; }

    public virtual ICollection<Issue> Issues { get; set; } = new List<Issue>();
    public virtual Project Project { get; set; } = null!;
    public virtual Status Status { get; set; } = null!;
    public virtual User User { get; set; } = null!;
}
