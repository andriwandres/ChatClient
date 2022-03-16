using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class GroupMembershipConfiguration : IEntityTypeConfiguration<GroupMembership>
{
    public void Configure(EntityTypeBuilder<GroupMembership> builder)
    {
        // Keys
        builder.HasKey(membership => membership.GroupMembershipId);

        // Properties
        builder.Property(membership => membership.UserId);

        builder.Property(membership => membership.GroupId);

        builder.Property(membership => membership.IsAdmin)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(membership => membership.Created)
            .IsRequired();

        // Relationships
        builder.HasOne(membership => membership.Recipient)
            .WithOne(recipient => recipient.GroupMembership)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(membership => membership.Group)
            .WithMany(group => group.Memberships)
            .HasForeignKey(membership => membership.GroupId);

        builder.HasOne(membership => membership.User)
            .WithMany(user => user.GroupMemberships)
            .HasForeignKey(membership => membership.UserId);
    }
}