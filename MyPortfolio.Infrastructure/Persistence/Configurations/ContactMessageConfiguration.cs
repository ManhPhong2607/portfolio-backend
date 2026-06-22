using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPortfolio.Domain.Entities;
using MyPortfolio.Domain.Enums;

namespace MyPortfolio.Infrastructure.Persistence.Configurations;

public class ContactMessageConfiguration : IEntityTypeConfiguration<ContactMessage>
{
    public void Configure(EntityTypeBuilder<ContactMessage> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.SenderName).HasMaxLength(100).IsRequired();

        builder.OwnsOne(x => x.SenderEmail, email =>
        {
            email.Property(e => e.Value)
                .HasColumnName("SenderEmail")
                .HasMaxLength(255)
                .IsRequired();
        });

        builder.Property(x => x.Subject).HasMaxLength(255);
        builder.Property(x => x.Body).IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<string>()
            .HasMaxLength(20).IsRequired()
            .HasDefaultValue(MessageStatus.Unread);

        builder.HasIndex(x => x.Status);

        builder.Property(x => x.SentAt).IsRequired();
    }
}