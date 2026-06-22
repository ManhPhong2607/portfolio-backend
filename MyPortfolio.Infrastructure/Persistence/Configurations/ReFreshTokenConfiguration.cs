
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPortfolio.Domain.Entities;

namespace MyPortfolio.Infrastructure.Persistence.Configurations;

public class ReFreshTokenConfiguration : IEntityTypeConfiguration<ReFreshToken>
{
    public void Configure(EntityTypeBuilder<ReFreshToken> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Token)
            .IsRequired();

        builder.HasIndex(x => x.Token).IsUnique();

        builder.Property(x => x.ExpiresAt).IsRequired();
        builder.Property(x => x.IsRevoked).IsRequired()
            .HasDefaultValue(false);

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}