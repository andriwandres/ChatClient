using ChatClient.Core.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace ChatClient.Data.Seeds
{
    public class MessageRecipientSeed : IEntityTypeConfiguration<MessageRecipient>
    {
        public void Configure(EntityTypeBuilder<MessageRecipient> builder)
        {
            //builder.HasData(Recipients);  
        }

        private readonly IEnumerable<MessageRecipient> Recipients = new HashSet<MessageRecipient>
        {
            // Message 1 - Private Message from USERID(1) to USERID(2)
            new MessageRecipient
            {
                MessageRecipientId = 1,
                MessageId = 1,
                RecipientUserId = 2,
                RecipientGroupId = null,
                IsRead = true,
                ReadAt = DateTime.UtcNow,
            },

            // Message 2 - Private Message from USERID(2) to USERID(1)
            new MessageRecipient
            {
                MessageRecipientId = 2,
                MessageId = 2,
                RecipientUserId = 1,
                RecipientGroupId = null,
                IsRead = false,
                ReadAt = DateTime.UtcNow,
            },

            // Message 3 - Group Message from USERID(1) to GROUPID(1)
            new MessageRecipient
            {
                MessageRecipientId = 3,
                MessageId = 3,
                RecipientUserId = null,
                RecipientGroupId = 2,
                IsRead = true,
                ReadAt = DateTime.UtcNow,
            },
            new MessageRecipient
            {
                MessageRecipientId = 4,
                MessageId = 3,
                RecipientUserId = null,
                RecipientGroupId = 3,
                IsRead = true,
                ReadAt = DateTime.UtcNow,
            },

            // Message 4 - Group Message from USERID(2) to GROUPID(1)
            new MessageRecipient
            {
                MessageRecipientId = 5,
                MessageId = 4,
                RecipientUserId = null,
                RecipientGroupId = 1,
                IsRead = true,
                ReadAt = DateTime.UtcNow,
            },
            new MessageRecipient
            {
                MessageRecipientId = 6,
                MessageId = 4,
                RecipientUserId = null,
                RecipientGroupId = 3,
                IsRead = true,
                ReadAt = DateTime.UtcNow,
            },

            // Message 5 - Group Message from USERID(3) to GROUPID(1)
            new MessageRecipient
            {
                MessageRecipientId = 7,
                MessageId = 5,
                RecipientUserId = null,
                RecipientGroupId = 1,
                IsRead = true,
                ReadAt = DateTime.UtcNow,
            },
            new MessageRecipient
            {
                MessageRecipientId = 8,
                MessageId = 5,
                RecipientUserId = null,
                RecipientGroupId = 2,
                IsRead = true,
                ReadAt = DateTime.UtcNow,
            },

            // Message 6 - Group Message from USERID(3) to GROUPID(2)
            new MessageRecipient
            {
                MessageRecipientId = 9,
                MessageId = 6,
                RecipientUserId = null,
                RecipientGroupId = 4,
                IsRead = true,
                ReadAt = DateTime.UtcNow,
            },
            new MessageRecipient
            {
                MessageRecipientId = 10,
                MessageId = 6,
                RecipientUserId = null,
                RecipientGroupId = 6,
                IsRead = true,
                ReadAt = DateTime.UtcNow,
            },

            // Message 7 - Group Message from USERID(1) to GROUPID(2)
            new MessageRecipient
            {
                MessageRecipientId = 11,
                MessageId = 7,
                RecipientUserId = null,
                RecipientGroupId = 5,
                IsRead = true,
                ReadAt = DateTime.UtcNow,
            },
            new MessageRecipient
            {
                MessageRecipientId = 12,
                MessageId = 7,
                RecipientUserId = null,
                RecipientGroupId = 6,
                IsRead = true,
                ReadAt = DateTime.UtcNow,
            },
        };
    }
}
