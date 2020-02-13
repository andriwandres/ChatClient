using ChatClient.Core.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatClient.Data.Configurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasKey(group => group.GroupId);

            builder.Property(group => group.Name)
                .IsRequired();

            builder.Property(group => group.Description)
                .IsRequired(false);

            builder.Property(group => group.CreatedAt)
                .IsRequired();

            builder.HasOne(group => group.GroupImage)
                .WithOne(di => di.Group);

            builder.HasMany(group => group.GroupMemberships)
                .WithOne(membership => membership.Group);
        }
    }
}
