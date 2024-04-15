namespace GamesGlobal.Core.Entities.Common;

public abstract class BaseDomainModel
{
    protected BaseDomainModel()
    {
        Id = Guid.Empty;
    }

    public Guid Id { get; set; }
}