
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPortfolio.Domain.Entities;
using MyPortfolio.Domain.Common;
namespace MyPortfolio.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Username)
            .HasMaxLength(50).IsRequired();

        builder.HasIndex(x => x.Username).IsUnique();

        builder.OwnsOne(x => x.Email, email =>
        {
            email.Property(e => e.Value)
                .HasColumnName("Email")
                .HasMaxLength(255)
                .IsRequired();
            email.HasIndex(e => e.Value).IsUnique();
        });

        builder.Property(x => x.PasswordHash)
            .HasMaxLength(255).IsRequired();

        builder.Property(x => x.Role)
            .HasMaxLength(20).IsRequired()
            .HasDefaultValue("admin");

        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.LastLoginAt);
    }
}