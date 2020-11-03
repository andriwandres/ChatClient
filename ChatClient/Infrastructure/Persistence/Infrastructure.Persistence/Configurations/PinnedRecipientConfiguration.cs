using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class PinnedRecipientConfiguration : IEntityTypeConfiguration<PinnedRecipient>
    {
        public void Configure(EntityTypeBuilder<PinnedRecipient> builder)
        {
            // Keys
            builder.HasKey(pin => pin.PinnedRecipientId);

            // Indexes
            builder.HasIndex(pin => new {pin.UserId, pin.RecipientId})
                .IsUnique();

            // Properties
            builder.Property(pin => pin.UserId);

            builder.Property(pin => pin.RecipientId);

            builder.Property(pin => pin.OrderIndex);

            builder.Property(pin => pin.Modified)
                .IsRequired();

            // Relationships
            builder.HasOne(pin => pin.User)
                .WithMany(user => user.PinnedRecipients)
                .HasForeignKey(pin => pin.UserId);

            builder.HasOne(pin => pin.Recipient)
                .WithMany(recipient => recipient.Pins)
                .HasForeignKey(pin => pin.RecipientId);
        }
    }
}
