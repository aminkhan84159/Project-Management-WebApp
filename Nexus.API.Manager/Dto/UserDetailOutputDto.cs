using Nexus.API.DataService.Models;

namespace Nexus.API.Manager.Dto
{
    public class UserDetailOutputDto
    {
        public int UserDetailId { get; set; }
        public int? UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public int Age { get; set; }
        public string PhoneNo { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public bool? IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedOn { get; set; }

        public static UserDetailOutputDto MapToEntity(UserDetail userDetail) => new UserDetailOutputDto
        {
            UserDetailId = userDetail.UserDetailId,
            UserId = userDetail.UserId,
            FirstName = userDetail.FirstName,
            LastName = userDetail.LastName,
            PhoneNo = userDetail.PhoneNo,
            Gender = userDetail.Gender,
            Age = userDetail.Age,
            Address = userDetail.Address,
            City = userDetail.City,
            State = userDetail.State,
            IsActive = userDetail.IsActive,
            CreatedBy = userDetail.CreatedBy,
            CreatedOn = userDetail.CreatedOn,
            ChangedBy = userDetail.ChangedBy,
            ChangedOn = userDetail.ChangedOn
        };
    }
}
