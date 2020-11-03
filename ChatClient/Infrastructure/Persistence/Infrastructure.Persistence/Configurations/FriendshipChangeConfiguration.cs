using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class FriendshipChangeConfiguration : IEntityTypeConfiguration<FriendshipChange>
    {
        public void Configure(EntityTypeBuilder<FriendshipChange> builder)
        {
            // Keys
            builder.HasKey(change => change.FriendshipChangeId);

            // Properties
            builder.Property(change => change.FriendshipId);

            builder.Property(change => change.StatusId);

            builder.Property(change => change.Created)
                .IsRequired();

            // Relationships
            builder.HasOne(change => change.Friendship)
                .WithMany(friendship => friendship.StatusChanges)
                .HasForeignKey(change => change.FriendshipId);

            builder.HasOne(change => change.Status)
                .WithMany(status => status.StatusChanges)
                .HasForeignKey(change => change.StatusId);
        }
    }
}
