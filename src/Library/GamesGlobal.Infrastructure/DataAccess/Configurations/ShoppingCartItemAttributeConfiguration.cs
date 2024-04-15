using System.Text.Json;
using GamesGlobal.Infrastructure.DataAccess.Entities;
using GamesGlobal.Infrastructure.DataAccess.Entities.ShoppingCartItemAttribute;
using GamesGlobal.Infrastructure.JsonConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GamesGlobal.Infrastructure.DataAccess.Configurations;

public class ShoppingCartItemAttributeConfiguration : IEntityTypeConfiguration<ShoppingCartItemAttributeDataModel>
{
    public void Configure(EntityTypeBuilder<ShoppingCartItemAttributeDataModel> builder)
    {
        // Set the table name for the entity
        builder.ToTable("ShoppingCartItemAttribute");

        // Configure primary key inherited from BaseEntity
        builder.HasKey(scia => scia.Id);

        // Configure fields
        builder.Property(scia => scia.ProductSpecificationAttributeId).IsRequired();
        builder.Property(scia => scia.ShoppingCartItemId).IsRequired();

        // Configure the 1-to-many relationship with ProductSpecificationAttribute
        builder.HasOne(scia => scia.ProductSpecificationAttribute)
            .WithMany()
            .HasForeignKey(scia => scia.ProductSpecificationAttributeId)
            .IsRequired();

        // Configure the 1-to-many relationship with ShoppingCartItem
        builder.HasOne(scia => scia.ShoppingCartItem)
            .WithMany()
            .HasForeignKey(scia => scia.ShoppingCartItemId)
            .IsRequired();

        // Configure JSON serialization for the Value property
        var jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new ShoppingCartItemAttributeValueConverter() }
        };

        var valueConverter = new ValueConverter<IShoppingCartItemAttributeValueDataModel, string>(
            v => JsonSerializer.Serialize(v, jsonSerializerOptions),
            v => JsonSerializer.Deserialize<IShoppingCartItemAttributeValueDataModel>(v ?? "{}", jsonSerializerOptions)
                 ?? new ImageShoppingCartItemAttributeValueDataModel()
        );

        builder
            .Property(e => e.Value)
            .HasConversion(valueConverter);
    }
}