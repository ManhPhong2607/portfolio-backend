// src/MyPortfolio.Infrastructure/Persistence/Configurations/SocialLinkConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPortfolio.Domain.Entities;

namespace MyPortfolio.Infrastructure.Persistence.Configurations;

public class SocialLinkConfiguration : IEntityTypeConfiguration<SocialLink>
{
    public void Configure(EntityTypeBuilder<SocialLink> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Platform).HasMaxLength(50).IsRequired();
        builder.Property(x => x.Label).HasMaxLength(100);
        builder.Property(x => x.Url).HasMaxLength(500).IsRequired();
        builder.Property(x => x.IconUrl).HasMaxLength(500);
        builder.Property(x => x.DisplayOrder).HasDefaultValue(0);
        builder.Property(x => x.IsVisible).HasDefaultValue(true);

        builder.HasIndex(x => x.DisplayOrder);
    }
}