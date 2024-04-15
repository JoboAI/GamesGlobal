using GamesGlobal.Infrastructure.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamesGlobal.Infrastructure.DataAccess.Configurations;

public class ShoppingCartItemConfiguration : IEntityTypeConfiguration<ShoppingCartItemDataModel>
{
    public void Configure(EntityTypeBuilder<ShoppingCartItemDataModel> builder)
    {
        // Set the table name for the entity
        builder.ToTable("ShoppingCartItem");

        // Configure primary key inherited from BaseEntity
        builder.HasKey(sci => sci.Id);

        // Configure fields
        builder.Property(sci => sci.ProductId).IsRequired();
        builder.Property(sci => sci.Quantity).IsRequired();
        builder.Property(sci => sci.ShoppingCartId).IsRequired();

        // Configure relationships
        builder.HasOne(sci => sci.ShoppingCart)
            .WithMany(sc => sc.Items)
            .HasForeignKey(sci => sci.ShoppingCartId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(sci => sci.Product)
            .WithMany()
            .HasForeignKey(sci => sci.ProductId)
            .IsRequired();

        builder.HasMany(sci => sci.Attributes)
            .WithOne(attr => attr.ShoppingCartItem)
            .HasForeignKey(attr => attr.ShoppingCartItemId)
            .IsRequired();
    }
}