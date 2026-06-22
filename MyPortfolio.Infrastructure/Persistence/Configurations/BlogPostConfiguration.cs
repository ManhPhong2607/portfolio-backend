
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPortfolio.Domain.Entities;
using MyPortfolio.Domain.Enums;

namespace MyPortfolio.Infrastructure.Persistence.Configurations;

public class BlogPostConfiguration : IEntityTypeConfiguration<BlogPost>
{
    public void Configure(EntityTypeBuilder<BlogPost> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .HasMaxLength(255).IsRequired();

        // Slug Value Object
        builder.OwnsOne(x => x.Slug, slug =>
        {
            slug.Property(s => s.Value)
                .HasColumnName("Slug")
                .HasMaxLength(300)
                .IsRequired();
            slug.HasIndex(s => s.Value).IsUnique();
        });

        builder.Property(x => x.Content).IsRequired();
        builder.Property(x => x.Excerpt).HasMaxLength(500);

        builder.Property(x => x.Status)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired()
            .HasDefaultValue(PostStatus.Draft);

        builder.HasIndex(x => x.Status);

        builder.Property(x => x.ReadingTimeMinutes)
            .IsRequired().HasDefaultValue(1);

        builder.Property(x => x.ViewCount)
            .IsRequired().HasDefaultValue(0);

        // FK → MediaFiles (cover image) — SET NULL khi xoá ảnh
        builder.HasOne(x => x.CoverMedia)
            .WithMany()
            .HasForeignKey(x => x.CoverMediaId)
            .OnDelete(DeleteBehavior.SetNull);

        // FK → Users (author)
        builder.HasOne(x => x.Author)
            .WithMany()
            .HasForeignKey(x => x.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);

        // Many-to-many Tags
        builder.HasMany(x => x.Tags)
            .WithMany(x => x.BlogPosts)
            .UsingEntity(
                "BlogPostTags",
                l => l.HasOne(typeof(Tag)).WithMany()
                    .HasForeignKey("TagId")
                    .OnDelete(DeleteBehavior.Cascade),
                r => r.HasOne(typeof(BlogPost)).WithMany()
                    .HasForeignKey("BlogPostId")
                    .OnDelete(DeleteBehavior.Cascade)
            );
    }
}