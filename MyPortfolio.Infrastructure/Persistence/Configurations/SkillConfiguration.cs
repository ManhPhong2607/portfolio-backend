using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPortfolio.Domain.Entities;

namespace MyPortfolio.Infrastructure.Persistence.Configurations;

public class SkillConfiguration : IEntityTypeConfiguration<Skill>
{
    public void Configure(EntityTypeBuilder<Skill> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(100).IsRequired();

        builder.Property(x => x.Category)
            .HasConversion<string>()
            .HasMaxLength(50).IsRequired();

        builder.Property(x => x.ProficiencyLevel).IsRequired();
        builder.Property(x => x.IconUrl).HasMaxLength(500);
        builder.Property(x => x.DisplayOrder).HasDefaultValue(0);
    }
}