namespace Nexus.API.DataService.Models;

public partial class Status
{
    public int StatusId { get; set; }
    public string? Type { get; set; }
    public bool? IsActive { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public int? ChangedBy { get; set; }
    public DateTime? ChangedOn { get; set; }

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
