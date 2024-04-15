using GamesGlobal.Infrastructure.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamesGlobal.Infrastructure.DataAccess.Configurations;

public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCartDataModel>
{
    public void Configure(EntityTypeBuilder<ShoppingCartDataModel> builder)
    {
        // Set the table name for the entity
        builder.ToTable("ShoppingCart");

        builder.HasKey(sci => sci.Id);
        builder.Property(sci => sci.UserIdentifier).IsRequired();

        // Configure relationships
        builder.HasMany(u => u.Items)
            .WithOne()
            .HasForeignKey(sci => sci.ShoppingCartId)
            .IsRequired();
    }
}