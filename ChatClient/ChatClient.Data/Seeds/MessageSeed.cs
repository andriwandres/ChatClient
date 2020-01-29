using ChatClient.Core.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace ChatClient.Data.Seeds
{
    public class MessageSeed : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasData(Messages);
        }

        private readonly IEnumerable<Message> Messages = new HashSet<Message>
        {
            new Message
            {
                MessageId = 1,
                AuthorId = 1,
                TextContent = "Hello User 1",
                ParentId = null,
                CreatedAt = DateTime.UtcNow,
                IsEdited = false,
                IsForwarded = false,
            },
            new Message
            {
                MessageId = 2,
                AuthorId = 2,
                TextContent = "Hello AndriWandres",
                ParentId = null,
                CreatedAt = DateTime.UtcNow,
                IsEdited = false,
                IsForwarded = false,
            },
            new Message
            {
                MessageId = 3,
                AuthorId = 1,
                TextContent = "Hello Group 1",
                ParentId = null,
                CreatedAt = DateTime.UtcNow,
                IsEdited = false,
                IsForwarded = false,
            },
            new Message
            {
                MessageId = 4,
                AuthorId = 2,
                TextContent = "Hello together!",
                ParentId = null,
                CreatedAt = DateTime.UtcNow,
                IsEdited = false,
                IsForwarded = false,
            },
            new Message
            {
                MessageId = 5,
                AuthorId = 3,
                TextContent = "Greetings everyone!",
                ParentId = null,
                CreatedAt = DateTime.UtcNow,
                IsEdited = false,
                IsForwarded = false,
            },
            new Message
            {
                MessageId = 6,
                AuthorId = 2,
                TextContent = "Hello together!",
                ParentId = null,
                CreatedAt = DateTime.UtcNow,
                IsEdited = false,
                IsForwarded = false,
            },
            new Message
            {
                MessageId = 7,
                AuthorId = 1,
                TextContent = "Hi!",
                ParentId = null,
                CreatedAt = DateTime.UtcNow,
                IsEdited = false,
                IsForwarded = false,
            },
        };
    }
}
