using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class MessageReactionConfiguration : IEntityTypeConfiguration<MessageReaction>
{
    public void Configure(EntityTypeBuilder<MessageReaction> builder)
    {
        // Keys
        builder.HasKey(reaction => reaction.MessageReactionId);

        // Properties
        builder.Property(reaction => reaction.UserId);

        builder.Property(reaction => reaction.ReactionValue);

        builder.Property(reaction => reaction.MessageId);

        // Relationships
        builder.HasOne(reaction => reaction.User)
            .WithMany(user => user.MessageReactions)
            .HasForeignKey(reaction => reaction.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(reaction => reaction.Message)
            .WithMany(message => message.Reactions)
            .HasForeignKey(reaction => reaction.MessageId);
    }
}