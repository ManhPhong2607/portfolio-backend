
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPortfolio.Domain.Entities;

namespace MyPortfolio.Infrastructure.Persistence.Configurations;

public class MediaFileConfiguration : IEntityTypeConfiguration<MediaFile>
{
    public void Configure(EntityTypeBuilder<MediaFile> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.PublicId)
            .IsRequired();
        builder.HasIndex(x => x.PublicId).IsUnique();

        builder.Property(x => x.SecureUrl).IsRequired();

        builder.Property(x => x.ResourceType)
            .HasMaxLength(20).IsRequired()
            .HasDefaultValue("image");

        builder.Property(x => x.OriginalFilename).HasMaxLength(255);
        builder.Property(x => x.Folder).HasMaxLength(100);
        builder.Property(x => x.UploadedAt).IsRequired();
    }
}