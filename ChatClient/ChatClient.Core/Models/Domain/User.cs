using System;
using System.Collections.Generic;

namespace ChatClient.Core.Models.Domain
{
    public class User
    {
        public int UserId { get; set; }
        public string UserCode { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<Message> AuthoredMessages { get; set; }
        public ICollection<GroupMembership> GroupMemberships { get; set; }
        public ICollection<MessageRecipient> ReceivedPrivateMessages { get; set; }
        public ICollection<UserRelationship> TargetedUserRelationships { get; set; }
        public ICollection<UserRelationship> InitiatedUserRelationships { get; set; }
    }
}
