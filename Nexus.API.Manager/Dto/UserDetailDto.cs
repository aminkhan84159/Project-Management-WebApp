namespace Nexus.API.Manager.Dto
{
    public class UserDetailDto
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PhoneNo { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public int Age { get; set; }
        public string Address { get; set; } = null!;
        public string State { get; set; } = null!;
        public string City { get; set; } = null!;
    }
}
