using ChatClient.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatClient.Data.Configurations
{
    public class MessageRecipientConfiguration : IEntityTypeConfiguration<MessageRecipient>
    {
        public void Configure(EntityTypeBuilder<MessageRecipient> builder)
        {
            builder.HasKey(recipient => recipient.MessageRecipientId);

            builder.HasAlternateKey(recipient => new
            {
                recipient.MessageId,
                recipient.RecipientUserId,
                recipient.RecipientGroupId,
            });

            builder.Property(recipient => recipient.MessageId)
                .IsRequired();

            builder.Property(recipient => recipient.IsRead)
                .IsRequired();

            builder.HasOne(recipient => recipient.Message)
                .WithMany(message => message.Recipients)
                .HasForeignKey(recipient => recipient.MessageId);

            builder.HasOne(recipient => recipient.RecipientUser)
                .WithMany(user => user.ReceivedMessages)
                .HasForeignKey(recipient => recipient.RecipientUserId);

            builder.HasOne(recipient => recipient.RecipientGroup)
                .WithMany(membership => membership.ReceivedMessages)
                .HasForeignKey(recipient => recipient.RecipientGroupId);
        }
    }
}
