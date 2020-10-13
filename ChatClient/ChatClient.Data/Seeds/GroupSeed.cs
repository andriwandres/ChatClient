using ChatClient.Core.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace ChatClient.Data.Seeds
{
    public class GroupSeed : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            //builder.HasData(Groups);
        }

        private readonly IEnumerable<Group> Groups = new HashSet<Group>()
        {
            new Group
            {
                GroupId = 1,
                Name = "Group 1",
                Description = "Description for Group 1",
                CreatedAt = DateTime.UtcNow,
            },
            new Group
            {
                GroupId = 2,
                Name = "Group 2",
                Description = "Description for Group 2",
                CreatedAt = DateTime.UtcNow,
            },
        };
    }
}
