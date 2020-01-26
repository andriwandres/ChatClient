using ChatClient.Core.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatClient.Data.Configurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(message => message.MessageId);

            builder.Property(message => message.AuthorId)
                .IsRequired();

            builder.Property(message => message.ParentId)
                .IsRequired(false);

            builder.Property(message => message.TextContent)
                .IsRequired();

            builder.Property(message => message.IsForwarded)
                .IsRequired();

            builder.Property(message => message.IsEdited)
                .IsRequired();

            builder.Property(message => message.CreatedAt)
                .IsRequired();

            builder.HasOne(message => message.Author)
                .WithMany(user => user.AuthoredMessages)
                .HasForeignKey(message => message.AuthorId);

            builder.HasOne(message => message.Parent)
                .WithOne()
                .HasForeignKey<Message>(message => message.ParentId);

            builder.HasMany(message => message.Recipients)
                .WithOne(recipient => recipient.Message);
        }
    }
}
