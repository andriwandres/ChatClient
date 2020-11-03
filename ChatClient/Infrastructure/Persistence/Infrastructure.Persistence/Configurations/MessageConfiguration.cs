using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            // Keys
            builder.HasKey(message => message.MessageId);

            // Properties
            builder.Property(message => message.AuthorId);

            builder.Property(message => message.ParentId);

            builder.Property(message => message.HtmlContent)
                .IsRequired();

            builder.Property(message => message.IsEdited)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(message => message.Created)
                .IsRequired();

            // Relationships
            builder.HasOne(message => message.Author)
                .WithMany(user => user.AuthoredMessages)
                .HasForeignKey(message => message.AuthorId);

            builder.HasOne(message => message.Parent)
                .WithOne()
                .HasForeignKey<Message>(message => message.ParentId);

            builder.HasMany(message => message.Reactions)
                .WithOne(reaction => reaction.Message);

            builder.HasMany(message => message.MessageRecipients)
                .WithOne(messageRecipient => messageRecipient.Message);
        }
    }
}
