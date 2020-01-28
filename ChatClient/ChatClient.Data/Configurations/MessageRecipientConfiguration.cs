using ChatClient.Core.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatClient.Data.Configurations
{
    public class MessageRecipientConfiguration : IEntityTypeConfiguration<MessageRecipient>
    {
        public void Configure(EntityTypeBuilder<MessageRecipient> builder)
        {
            builder.HasKey(recipient => recipient.MessageRecipientId);

            builder.Property(recipient => recipient.MessageId)
                .IsRequired();

            builder.Property(recipient => recipient.RecipientUserId)
                .IsRequired(false);

            builder.Property(recipient => recipient.RecipientGroupId)
                .IsRequired(false);

            builder.Property(recipient => recipient.IsRead)
                .IsRequired();

            builder.Property(recipient => recipient.ReadAt)
                .IsRequired(false);

            builder.HasOne(recipient => recipient.Message)
                .WithMany(message => message.Recipients)
                .HasForeignKey(recipient => recipient.MessageId);

            builder.HasOne(recipient => recipient.RecipientUser)
                .WithMany(user => user.ReceivedPrivateMessages)
                .HasForeignKey(recipient => recipient.RecipientUserId);

            builder.HasOne(recipient => recipient.RecipientGroup)
                .WithMany(membership => membership.ReceivedGroupMessages)
                .HasForeignKey(recipient => recipient.RecipientGroupId);
        }
    }
}
