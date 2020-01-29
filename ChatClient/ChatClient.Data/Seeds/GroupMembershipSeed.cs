using ChatClient.Core.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace ChatClient.Data.Seeds
{
    public class GroupMembershipSeed : IEntityTypeConfiguration<GroupMembership>
    {
        public void Configure(EntityTypeBuilder<GroupMembership> builder)
        {
            builder.HasData(Memberships);  
        }

        private readonly IEnumerable<GroupMembership> Memberships = new HashSet<GroupMembership>()
        {
            new GroupMembership
            {
                GroupMembershipId = 1,
                GroupId = 1,
                UserId = 1,
                IsAdmin = true,
                CreatedAt = DateTime.UtcNow,
            },
            new GroupMembership
            {
                GroupMembershipId = 2,
                GroupId = 1,
                UserId = 2,
                IsAdmin = false,
                CreatedAt = DateTime.UtcNow,
            },
            new GroupMembership
            {
                GroupMembershipId = 3,
                GroupId = 1,
                UserId = 3,
                IsAdmin = false,
                CreatedAt = DateTime.UtcNow,
            },
            new GroupMembership
            {
                GroupMembershipId = 4,
                GroupId = 2,
                UserId = 1,
                IsAdmin = false,
                CreatedAt = DateTime.UtcNow,
            },
            new GroupMembership
            {
                GroupMembershipId = 5,
                GroupId = 2,
                UserId = 3,
                IsAdmin = true,
                CreatedAt = DateTime.UtcNow,
            },
            new GroupMembership
            {
                GroupMembershipId = 6,
                GroupId = 2,
                UserId = 4,
                IsAdmin = false,
                CreatedAt = DateTime.UtcNow,
            },
        };
    }
}
