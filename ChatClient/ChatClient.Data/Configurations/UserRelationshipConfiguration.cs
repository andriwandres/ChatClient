using ChatClient.Core.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatClient.Data.Configurations
{
    public class UserRelationshipConfiguration : IEntityTypeConfiguration<UserRelationship>
    {
        public void Configure(EntityTypeBuilder<UserRelationship> builder)
        {
            builder.HasKey(ur => ur.UserRelationshipId);

            builder.Property(ur => ur.InitiatorId)
                .IsRequired();

            builder.Property(ur => ur.TargetId)
                .IsRequired();

            builder.Property(ur => ur.CreatedAt)
                .IsRequired();

            builder.Property(ur => ur.Status)
                .IsRequired();

            builder.Property(ur => ur.Message)
                .IsRequired(false);

            builder.HasOne(ur => ur.Initiator)
                .WithMany(u => u.InitiatedUserRelationships)
                .HasForeignKey(ur => ur.InitiatorId);

            builder.HasOne(ur => ur.Target)
                .WithMany(u => u.TargetedUserRelationships)
                .HasForeignKey(ur => ur.TargetId);
            
        }
    }
}
