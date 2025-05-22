namespace Nexus.API.DataService.Models;

public partial class User
{
    public int UserId { get; set; }
    public string Email { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public DateOnly? JoinedDate { get; set; }
    public bool? IsActive { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public int? ChangedBy { get; set; }
    public DateTime? ChangedOn { get; set; }

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
    public virtual ICollection<Relation> Relations { get; set; } = new List<Relation>();
    public virtual UserDetail? UserDetail { get; set; }
}
