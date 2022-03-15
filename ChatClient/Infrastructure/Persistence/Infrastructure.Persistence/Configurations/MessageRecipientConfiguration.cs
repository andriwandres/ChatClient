using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class MessageRecipientConfiguration : IEntityTypeConfiguration<MessageRecipient>
{
    public void Configure(EntityTypeBuilder<MessageRecipient> builder)
    {
        // Keys
        builder.HasKey(messageRecipient => messageRecipient.MessageRecipientId);

        // Indexes
        builder.HasIndex(messageRecipient => new { messageRecipient.RecipientId, messageRecipient.MessageId })
            .IsUnique();

        // Properties
        builder.Property(messageRecipient => messageRecipient.MessageId);

        builder.Property(messageRecipient => messageRecipient.RecipientId);

        builder.Property(messageRecipient => messageRecipient.IsForwarded)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(messageRecipient => messageRecipient.IsRead)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(messageRecipient => messageRecipient.ReadDate);

        // Relationships
        builder.HasOne(messageRecipient => messageRecipient.Message)
            .WithMany(message => message.MessageRecipients)
            .HasForeignKey(messageRecipient => messageRecipient.MessageId);

        builder.HasOne(messageRecipient => messageRecipient.Recipient)
            .WithMany(recipient => recipient.ReceivedMessages)
            .HasForeignKey(messageRecipient => messageRecipient.RecipientId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}