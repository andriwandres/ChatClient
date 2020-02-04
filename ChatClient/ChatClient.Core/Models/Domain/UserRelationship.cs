using System;

namespace ChatClient.Core.Models.Domain
{
    public class UserRelationship
    {
        public int UserRelationshipId { get; set; }
        public int InitiatorId { get; set; }
        public int TargetId { get; set; }
        public string Message { get; set; }
        public UserRelationshipStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public User Initiator { get; set; }
        public User Target { get; set; }
    }

    public enum UserRelationshipStatus
    {
        Pending = 0,
        Accepted = 1,
        Ignored = 2,
        Blocked = 3,
    }
}
