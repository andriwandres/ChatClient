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

            // Indexes
            builder.HasIndex(friendship => new {friendship.RequesterId, friendship.AddresseeId})
                .IsUnique();

            // Properties
            builder.Property(friendship => friendship.RequesterId);

            builder.Property(friendship => friendship.AddresseeId);

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
