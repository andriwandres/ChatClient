using System;
using System.Collections.Generic;

namespace ChatClient.Core.Models.Domain
{
    public class GroupMembership
    {
        public int GroupMembershipId { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime CreatedAt { get; set; }

        public User User { get; set; }
        public Group Group { get; set; }
        public ICollection<MessageRecipient> ReceivedGroupMessages { get; set; }
    }
}
