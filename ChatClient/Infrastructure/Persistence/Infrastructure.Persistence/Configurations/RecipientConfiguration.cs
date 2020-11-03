using Core.Domain.Entities;
using Infrastructure.Persistence.Generators;
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

            // Indexes
            builder.HasIndex(recipient => recipient.DisplayId)
                .IsUnique();

            builder.HasIndex(recipient => new { recipient.UserId, recipient.GroupMembershipId })
                .IsUnique();

            // Properties
            builder.Property(recipient => recipient.UserId);

            builder.Property(recipient => recipient.GroupMembershipId);

            builder.Property(recipient => recipient.DisplayId)
                .IsRequired()
                .HasMaxLength(8)
                .ValueGeneratedOnAdd()
                .HasValueGenerator<DisplayIdGenerator>();

            // Relationships
            builder.HasMany(recipient => recipient.Archives)
                .WithOne(archive => archive.Recipient);

            builder.HasOne(recipient => recipient.GroupMembership)
                .WithOne(membership => membership.Recipient)
                .HasForeignKey<Recipient>(recipient => recipient.GroupMembershipId);

            builder.HasOne(recipient => recipient.User)
                .WithOne(user => user.Recipient)
                .HasForeignKey<Recipient>(recipient => recipient.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(recipient => recipient.Pins)
                .WithOne(pin => pin.Recipient);

            builder.HasMany(recipient => recipient.ReceivedMessages)
                .WithOne(messageRecipient => messageRecipient.Recipient);
        }
    }
}
