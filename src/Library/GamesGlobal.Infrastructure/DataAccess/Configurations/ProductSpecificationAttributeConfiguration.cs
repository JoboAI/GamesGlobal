using System.Text.Json;
using GamesGlobal.Infrastructure.DataAccess.Entities;
using GamesGlobal.Infrastructure.DataAccess.Entities.ProductSpecificationAttributes;
using GamesGlobal.Infrastructure.JsonConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GamesGlobal.Infrastructure.DataAccess.Configurations;

public class
    ProductSpecificationAttributeConfiguration : IEntityTypeConfiguration<ProductSpecificationAttributeDataModel>
{
    public void Configure(EntityTypeBuilder<ProductSpecificationAttributeDataModel> builder)
    {
        // Set the table name for the entity
        builder.ToTable("ProductSpecificationAttribute");

        // Configure primary key inherited from BaseEntity
        builder.HasKey(psa => psa.Id);

        // Configure fields
        builder.Property(psa => psa.Label)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(psa => psa.IsRequired).IsRequired();

        // Configure foreign key and relationship with Product
        builder.HasOne(psa => psa.Product)
            .WithMany(a => a.SpecificationAttributes)
            .HasForeignKey(psa => psa.ProductId)
            .IsRequired();

        var jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new ProductSpecificationAttributeConfigurationConverter() }
        };

        var attributesConverter = new ValueConverter<IProductSpecificationAttributeConfigurationDataModel, string>(
            v => JsonSerializer.Serialize(v, jsonSerializerOptions),
            v => JsonSerializer.Deserialize<IProductSpecificationAttributeConfigurationDataModel>(v ?? "{}",
                jsonSerializerOptions) ?? new ImageProductSpecificationAttributeConfigurationDataModel()
        );

        builder
            .Property(e => e.Configuration)
            .HasConversion(attributesConverter);
    }
}