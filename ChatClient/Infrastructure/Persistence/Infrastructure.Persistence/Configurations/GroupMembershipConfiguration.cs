using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class GroupMembershipConfiguration : IEntityTypeConfiguration<GroupMembership>
    {
        public void Configure(EntityTypeBuilder<GroupMembership> builder)
        {
            // Keys
            builder.HasKey(membership => membership.GroupMembershipId);

            builder.HasAlternateKey(membership => new { membership.UserId, membership.GroupId });

            // Properties
            builder.Property(membership => membership.IsAdmin)
                .IsRequired();

            // Relationships
            builder.HasOne(membership => membership.Group)
                .WithMany(group => group.Memberships)
                .HasForeignKey(membership => membership.GroupId);

            builder.HasOne(membership => membership.User)
                .WithMany(user => user.GroupMemberships)
                .HasForeignKey(membership => membership.UserId);
        }
    }
}
