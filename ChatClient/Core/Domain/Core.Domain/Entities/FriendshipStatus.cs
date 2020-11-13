using System.Collections.Generic;

namespace Core.Domain.Entities
{
    public class FriendshipStatus
    {
        public FriendshipStatusId FriendshipStatusId { get; set; }
        public string Name { get; set; }

        public ICollection<FriendshipChange> StatusChanges { get; set; }
    }

    public enum FriendshipStatusId : int
    {
        Pending = 1,
        Accepted = 2,
        Ignored = 3,
        Blocked = 4
    }
}
