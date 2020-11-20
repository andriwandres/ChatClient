using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class RecipientConfiguration : IEntityTypeConfiguration<Recipient>
    {
        public void Configure(EntityTypeBuilder<Recipient> builder)
        {
            // Keys
            builder.HasKey(recipient => recipient.RecipientId);

            builder.HasIndex(recipient => new { recipient.UserId, recipient.GroupMembershipId })
                .IsUnique();

            // Properties
            builder.Property(recipient => recipient.UserId);

            builder.Property(recipient => recipient.GroupMembershipId);

            // Relationships
            builder.HasMany(recipient => recipient.Archives)
                .WithOne(archive => archive.Recipient);

            builder.HasOne(recipient => recipient.GroupMembership)
                .WithOne(membership => membership.Recipient)
                .HasForeignKey<Recipient>(recipient => recipient.GroupMembershipId)
                .IsRequired(false);

            builder.HasOne(recipient => recipient.User)
                .WithOne(user => user.Recipient)
                .HasForeignKey<Recipient>(recipient => recipient.UserId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            builder.HasMany(recipient => recipient.Pins)
                .WithOne(pin => pin.Recipient);

            builder.HasMany(recipient => recipient.ReceivedMessages)
                .WithOne(messageRecipient => messageRecipient.Recipient);
        }
    }
}
