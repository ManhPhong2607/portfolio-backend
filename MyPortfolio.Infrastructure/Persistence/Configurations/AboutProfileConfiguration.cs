using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPortfolio.Domain.Entities;

namespace MyPortfolio.Infrastructure.Persistence.Configurations;

public class AboutProfileConfiguration : IEntityTypeConfiguration<AboutProfile>
{
    public void Configure(EntityTypeBuilder<AboutProfile> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FullName).HasMaxLength(255).IsRequired();
        builder.Property(x => x.Tagline).HasMaxLength(300);
        builder.Property(x => x.Location).HasMaxLength(255);

        builder.OwnsOne(x => x.ContactEmail, email =>
        {
            email.Property(e => e.Value)
                .HasColumnName("Email")
                .HasMaxLength(255);
        });

        // FK → MediaFiles (avatar) — SET NULL khi xoá ảnh
        builder.HasOne(x => x.Avatar)
            .WithMany()
            .HasForeignKey(x => x.AvatarMediaId)
            .OnDelete(DeleteBehavior.SetNull);

        // FK → MediaFiles (CV) — SET NULL khi xoá file
        builder.HasOne(x => x.CvFile)
            .WithMany()
            .HasForeignKey(x => x.CvMediaId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}