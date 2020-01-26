﻿using System;

namespace ChatClient.Core.Models.Domain
{
    public class MessageRecipient
    {
        public int MessageRecipientId { get; set; }
        public int MessageId { get; set; }
        public int? RecipientUserId { get; set; }
        public int? RecipientGroupId { get; set; }
        public bool IsRead { get; set; }
        public DateTime? ReadAt { get; set; }

        public Message Message { get; set; }
        public User RecipientUser { get; set; }
        public GroupMembership RecipientGroup { get; set; }
    }
}