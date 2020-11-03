using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class MessageReactionConfiguration : IEntityTypeConfiguration<MessageReaction>
    {
        public void Configure(EntityTypeBuilder<MessageReaction> builder)
        {
            // Keys
            builder.HasKey(reaction => reaction.MessageReactionId);

            // Indexes
            builder.HasIndex(reaction => new { reaction.UserId, reaction.MessageId, reaction.EmojiId })
                .IsUnique();

            // Properties
            builder.Property(reaction => reaction.UserId);

            builder.Property(reaction => reaction.EmojiId);

            builder.Property(reaction => reaction.MessageId);

            // Relationships
            builder.HasOne(reaction => reaction.Message)
                .WithMany(message => message.Reactions)
                .HasForeignKey(reaction => reaction.MessageId);

            builder.HasOne(reaction => reaction.Emoji)
                .WithMany(emoji => emoji.Reactions)
                .HasForeignKey(reaction => reaction.EmojiId);
        }
    }
}
