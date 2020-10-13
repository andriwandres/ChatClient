using ChatClient.Core.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace ChatClient.Data.Seeds
{
    public class UserRelationshipSeed : IEntityTypeConfiguration<UserRelationship>
    {
        public void Configure(EntityTypeBuilder<UserRelationship> builder)
        {
            //builder.HasData(Relationships);
        }

        private readonly IEnumerable<UserRelationship> Relationships = new HashSet<UserRelationship>
        {
            new UserRelationship
            {
                UserRelationshipId = 1,
                InitiatorId = 1,
                TargetId = 2,
                Status = UserRelationshipStatus.Accepted,
                CreatedAt = DateTime.UtcNow,
            },
            new UserRelationship
            {
                UserRelationshipId = 2,
                InitiatorId = 1,
                TargetId = 3,
                Status = UserRelationshipStatus.Pending,
                CreatedAt = DateTime.UtcNow,
            },
            new UserRelationship
            {
                UserRelationshipId = 3,
                InitiatorId = 4,
                TargetId = 1,
                Status = UserRelationshipStatus.Pending,
                CreatedAt = DateTime.UtcNow,
            }
        };
    }
}
