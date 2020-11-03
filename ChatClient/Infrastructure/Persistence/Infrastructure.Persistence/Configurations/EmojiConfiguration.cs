using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class EmojiConfiguration : IEntityTypeConfiguration<Emoji>
    {
        public void Configure(EntityTypeBuilder<Emoji> builder)
        {
            // Keys
            builder.HasKey(emoji => emoji.EmojiId);

            // Properties
            builder.Property(emoji => emoji.Value)
                .IsRequired();

            builder.Property(emoji => emoji.Label)
                .IsRequired();

            builder.Property(emoji => emoji.Shortcut);

            // Relationships
            builder.HasMany(emoji => emoji.Reactions)
                .WithOne(reaction => reaction.Emoji);
        }
    }
}
