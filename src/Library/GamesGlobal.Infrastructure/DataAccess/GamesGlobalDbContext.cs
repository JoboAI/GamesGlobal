using System.Reflection;
using GamesGlobal.Core.Interfaces.Common;
using GamesGlobal.Infrastructure.DataAccess.Entities;
using GamesGlobal.Infrastructure.DataAccess.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace GamesGlobal.Infrastructure.DataAccess;

public class GamesGlobalDbContext : DbContext
{
    private readonly IUserContext _userContext;

    public GamesGlobalDbContext(IUserContext userContext, DbContextOptions<GamesGlobalDbContext> options) :
        base(options)
    {
        _userContext = userContext;
    }

    public GamesGlobalDbContext()
    {
    }

    public DbSet<ShoppingCartDataModel> ShoppingCarts { get; set; }
    public DbSet<ImageDataModel> Images { get; set; }
    public DbSet<ProductDataModel> Products { get; set; }
    public DbSet<ShoppingCartItemDataModel> ShoppingCartItems { get; set; }
    public DbSet<ProductSpecificationAttributeDataModel> ProductSpecificationAttributes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //    optionsBuilder.UseLazyLoadingProxies();
        optionsBuilder.UseInMemoryDatabase("GamesGlobal", b => b.EnableNullChecks(false));
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        var currentUserId = _userContext.GetUserId();
        foreach (var entry in ChangeTracker.Entries<IBaseEntity>().ToList())
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedOn = DateTime.UtcNow;
                    entry.Entity.CreatedBy = currentUserId;
                    break;

                case EntityState.Modified:
                    entry.Entity.UpdatedOn = DateTime.UtcNow;
                    entry.Entity.UpdatedBy = currentUserId;
                    break;
            }

        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}