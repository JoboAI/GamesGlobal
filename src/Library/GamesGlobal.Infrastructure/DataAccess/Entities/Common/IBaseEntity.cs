namespace GamesGlobal.Infrastructure.DataAccess.Entities.Common;

public interface IBaseEntity
{
    Guid Id { get; set; }
    string CreatedBy { get; set; }
    string UpdatedBy { get; set; }
    DateTime CreatedOn { get; set; }
    DateTime? UpdatedOn { get; set; }
}