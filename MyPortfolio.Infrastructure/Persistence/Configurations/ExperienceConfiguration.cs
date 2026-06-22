using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPortfolio.Domain.Entities;

namespace MyPortfolio.Infrastructure.Persistence.Configurations;

public class ExperienceConfiguration : IEntityTypeConfiguration<Experience>
{
    public void Configure(EntityTypeBuilder<Experience> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.CompanyName).HasMaxLength(255).IsRequired();
        builder.Property(x => x.Position).HasMaxLength(255).IsRequired();
        builder.Property(x => x.Location).HasMaxLength(255);

        builder.Property(x => x.EmploymentType)
            .HasConversion<string>()
            .HasMaxLength(50).IsRequired();

        builder.Property(x => x.StartDate).IsRequired();
        builder.Property(x => x.IsCurrent).HasDefaultValue(false);
        builder.Property(x => x.DisplayOrder).HasDefaultValue(0);
    }
}