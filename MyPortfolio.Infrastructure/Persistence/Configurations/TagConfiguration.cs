using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPortfolio.Domain.Entities;

namespace MyPortfolio.Infrastructure.Persistence.Configurations;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.OwnsOne(x => x.Slug, slug =>
        {
            slug.Property(s => s.Value)
                .HasColumnName("Slug")
                .HasMaxLength(300)
                .IsRequired();
            slug.HasIndex(s => s.Value).IsUnique();
        });
    }
}
