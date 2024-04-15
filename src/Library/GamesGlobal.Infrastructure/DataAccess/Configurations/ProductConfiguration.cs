using GamesGlobal.Infrastructure.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamesGlobal.Infrastructure.DataAccess.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<ProductDataModel>
{
    public void Configure(EntityTypeBuilder<ProductDataModel> builder)
    {
        // Set the table name for the entity
        builder.ToTable("Product");

        // Configure primary key
        builder.HasKey(p => p.Id);

        // Configure properties
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(p => p.Description)
            .HasMaxLength(500);


        builder.HasMany(p => p.SpecificationAttributes)
            .WithOne(a => a.Product)
            .HasForeignKey(attr => attr.ProductId);

        builder.HasMany(p => p.ShoppingCartItems)
            .WithOne(a => a.Product)
            .HasForeignKey(cartItem => cartItem.ProductId);
    }
}