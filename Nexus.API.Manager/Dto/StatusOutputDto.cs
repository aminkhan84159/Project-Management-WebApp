using Nexus.API.DataService.Models;

namespace Nexus.API.Manager.Dto
{
    public class StatusOutputDto
    {
        public int StatusId { get; set; }
        public string? Type { get; set; }
        public bool? IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedOn { get; set; }

        public static StatusOutputDto MapToEntity(Status status) => new StatusOutputDto
        {
            StatusId = status.StatusId,
            Type = status.Type,
            IsActive = status.IsActive,
            CreatedBy = status.CreatedBy,
            CreatedOn = status.CreatedOn,
            ChangedBy = status.ChangedBy,
            ChangedOn = status.ChangedOn
        };
    }
}
