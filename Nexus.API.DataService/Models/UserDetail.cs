using System.ComponentModel.DataAnnotations;

namespace Nexus.API.DataService.Models;

public partial class UserDetail
{
    public int UserDetailId { get; set; }
    public int? UserId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Gender { get; set; } = null!;
    public int Age { get; set; }
    public string PhoneNo { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string State { get; set; } = null!;
    public string City { get; set; } = null!;
    public bool? IsActive { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public int? ChangedBy { get; set; }
    public DateTime? ChangedOn { get; set; }

    public virtual User? User { get; set; }
}
