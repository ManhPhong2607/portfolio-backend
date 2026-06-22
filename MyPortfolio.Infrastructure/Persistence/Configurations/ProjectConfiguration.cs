
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPortfolio.Domain.Entities;
using MyPortfolio.Domain.Enums;

namespace MyPortfolio.Infrastructure.Persistence.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .HasMaxLength(255).IsRequired();

        builder.OwnsOne(x => x.Slug, slug =>
        {
            slug.Property(s => s.Value)
                .HasColumnName("Slug")
                .HasMaxLength(300)
                .IsRequired();
            slug.HasIndex(s => s.Value).IsUnique();
        });

        builder.Property(x => x.ShortDescription).HasMaxLength(500);

        builder.Property(x => x.Status)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired()
            .HasDefaultValue(ProjectStatus.Draft);

        builder.Property(x => x.DisplayOrder).HasDefaultValue(0);
        builder.Property(x => x.IsFeatured).HasDefaultValue(false);

        builder.Property(x => x.DemoUrl).HasMaxLength(500);
        builder.Property(x => x.GithubUrl).HasMaxLength(500);

        // FK → MediaFiles (thumbnail) — SET NULL khi xoá ảnh
        builder.HasOne(x => x.ThumbnailMedia)
            .WithMany()
            .HasForeignKey(x => x.ThumbnailMediaId)
            .OnDelete(DeleteBehavior.SetNull);

        // Many-to-many Technologies
        builder.HasMany(x => x.Technologies)
            .WithMany(x => x.Projects)
            .UsingEntity(
                "ProjectTechnologies",
                l => l.HasOne(typeof(Technology)).WithMany()
                    .HasForeignKey("TechnologyId")
                    .OnDelete(DeleteBehavior.Cascade),
                r => r.HasOne(typeof(Project)).WithMany()
                    .HasForeignKey("ProjectId")
                    .OnDelete(DeleteBehavior.Cascade)
            );
    }
}