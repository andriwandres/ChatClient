using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class ArchivedRecipientConfiguration : IEntityTypeConfiguration<ArchivedRecipient>
    {
        public void Configure(EntityTypeBuilder<ArchivedRecipient> builder)
        {
            // Keys
            builder.HasKey(archive => archive.ArchivedRecipientId);

            // Indexes
            builder.HasIndex(archive => new { archive.UserId, archive.RecipientId })
                .IsUnique();

            // Properties
            builder.Property(archive => archive.UserId);

            builder.Property(archive => archive.RecipientId);

            // Relationships
            builder.HasOne(archive => archive.Recipient)
                .WithMany(recipient => recipient.Archives)
                .HasForeignKey(archive => archive.RecipientId);

            builder.HasOne(archive => archive.User)
                .WithMany(user => user.ArchivedRecipients)
                .HasForeignKey(archive => archive.UserId);
        }
    }
}
