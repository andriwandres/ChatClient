using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class NicknameAssignmentConfiguration : IEntityTypeConfiguration<NicknameAssignment>
    {
        public void Configure(EntityTypeBuilder<NicknameAssignment> builder)
        {
            // Keys
            builder.HasKey(nicknameAssignment => nicknameAssignment.NicknameAssignmentId);

            // Indexes
            builder.HasIndex(nicknameAssignment => new { nicknameAssignment.RequesterId, nicknameAssignment.AddresseeId })
                .IsUnique();

            // Properties
            builder.Property(nicknameAssignment => nicknameAssignment.NicknameValue)
                .IsRequired();

            // Relationship
            builder.HasOne(nicknameAssignment => nicknameAssignment.Requester)
                .WithMany(user => user.RequestedNicknames)
                .HasForeignKey(nicknameAssignment => nicknameAssignment.RequesterId);

            builder.HasOne(nicknameAssignment => nicknameAssignment.Addressee)
                .WithMany(user => user.AddressedNicknames)
                .HasForeignKey(nicknameAssignment => nicknameAssignment.AddresseeId);
        }
    }
}
