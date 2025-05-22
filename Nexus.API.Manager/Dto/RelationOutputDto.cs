using Nexus.API.DataService.Models;

namespace Nexus.API.Manager.Dto
{
    public class RelationOutputDto
    {
        public int RelationId { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public string Role { get; set; } = null!;
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedOn { get; set; }

        public static RelationOutputDto MapToEntity(Relation relation) => new RelationOutputDto
        {
            RelationId = relation.RelationId,
            UserId = relation.UserId,
            ProjectId = relation.ProjectId,
            Role = relation.Role,
            IsActive = relation.IsActive,
            CreatedBy = relation.CreatedBy,
            CreatedOn = relation.CreatedOn,
            ChangedBy = relation.ChangedBy,
            ChangedOn = relation.ChangedOn
        };
    }
}
