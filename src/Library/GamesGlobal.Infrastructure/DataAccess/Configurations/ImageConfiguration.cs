using GamesGlobal.Infrastructure.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamesGlobal.Infrastructure.DataAccess.Configurations;

public class ImageConfiguration : IEntityTypeConfiguration<ImageDataModel>
{
    public void Configure(EntityTypeBuilder<ImageDataModel> builder)
    {
        // Set the table name for the entity
        builder.ToTable("Image");

        // Configure primary key, which is inherited from BaseEntity
        builder.HasKey(img => img.Id);

        // Configure properties
        builder.Property(img => img.ImageData)
            .HasColumnType("VARBINARY(MAX)");

        builder.Property(img => img.ContentType)
            .HasMaxLength(50);
    }
}