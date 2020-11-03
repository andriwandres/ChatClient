using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class FriendshipConfiguration : IEntityTypeConfiguration<Friendship>
    {
        public void Configure(EntityTypeBuilder<Friendship> builder)
        {
            // Keys
            builder.HasKey(friendship => friendship.FriendshipId);

            builder.HasAlternateKey(friendship => new { friendship.RequesterId, friendship.AddresseeId });

            // Relationships
            builder.HasOne(friendship => friendship.Requester)
                .WithMany(user => user.RequestedFriendships)
                .HasForeignKey(friendship => friendship.RequesterId);

            builder.HasOne(friendship => friendship.Addressee)
                .WithMany(user => user.AddressedFriendships)
                .HasForeignKey(friendship => friendship.AddresseeId);

            builder.HasMany(friendship => friendship.StatusChanges)
                .WithOne(change => change.Friendship);
        }
    }
}
