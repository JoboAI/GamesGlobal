using System.ComponentModel.DataAnnotations;

namespace GamesGlobal.Infrastructure.DataAccess.Entities.Common;

public abstract class BaseEntity : IBaseEntity
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string CreatedBy { get; set; }

    public string UpdatedBy { get; set; }

    [Required]
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedOn { get; set; }
}