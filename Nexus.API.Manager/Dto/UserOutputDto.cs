using Nexus.API.DataService.Models;

namespace Nexus.API.Manager.Dto
{
    public class UserOutputDto
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

        public static UserOutputDto MapToEntity(User user) => new UserOutputDto
        {
            UserId = user.UserId,
            Email = user.Email,
            Username = user.Username,
            Password = user.Password,
            JoinedDate = user.JoinedDate,
            IsActive = user.IsActive,
            CreatedBy = user.CreatedBy,
            CreatedOn = user.CreatedOn,
            ChangedBy = user.ChangedBy,
            ChangedOn = user.ChangedOn
        };
    }
}
