using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        // Keys
        builder.HasKey(group => group.GroupId);

        // Properties
        builder.Property(group => group.GroupImageId);

        builder.Property(group => group.Name)
            .IsRequired();

        builder.Property(group => group.Description)
            .IsRequired();

        builder.Property(group => group.Created)
            .IsRequired();

        builder.Property(group => group.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        // Relationships
        builder.HasOne(group => group.GroupImage)
            .WithOne()
            .HasForeignKey<Group>(group => group.GroupImageId);

        builder.HasMany(group => group.Memberships)
            .WithOne(membership => membership.Group);
    }
}