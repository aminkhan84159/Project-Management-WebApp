namespace Nexus.API.DataService.Models;

public partial class Project
{
    public int ProjectId { get; set; }
    public string ProjectName { get; set; } = null!;
    public string? Description { get; set; }
    public int? StatusId { get; set; }
    public bool? IsActive { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public int? ChangedBy { get; set; }
    public DateTime? ChangedOn { get; set; }
    public DateOnly? DueDate { get; set; }

    public virtual ICollection<Issue> Issues { get; set; } = new List<Issue>();
    public virtual ICollection<Relation> Relations { get; set; } = new List<Relation>();
    public virtual Status? Status { get; set; }
    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
