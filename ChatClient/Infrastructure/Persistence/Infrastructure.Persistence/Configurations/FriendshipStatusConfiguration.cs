using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Persistence.Configurations
{
    public class FriendshipStatusConfiguration : IEntityTypeConfiguration<FriendshipStatus>
    {
        public void Configure(EntityTypeBuilder<FriendshipStatus> builder)
        {
            // Keys
            builder.HasKey(status => status.FriendshipStatusId);
            
            // Properties
            builder.Property(status => status.Name)
                .IsRequired();

            // Relationships
            builder.HasMany(status => status.StatusChanges)
                .WithOne(change => change.Status);

            // Seed data
            IEnumerable<FriendshipStatus> friendshipStatuses = Enum
                .GetValues(typeof(FriendshipStatusId))
                .Cast<FriendshipStatusId>()
                .Select(status => new FriendshipStatus()
                {
                    FriendshipStatusId = status,
                    Name = status.ToString()
                });

            builder.HasData(friendshipStatuses);
        }
    }
}
