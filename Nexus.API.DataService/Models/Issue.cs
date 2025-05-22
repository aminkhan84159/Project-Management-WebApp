namespace Nexus.API.DataService.Models;

public partial class Issue
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

    public virtual Project Project { get; set; } = null!;
    public virtual Task Task { get; set; } = null!;
}
