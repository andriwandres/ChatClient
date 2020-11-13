using System;

namespace Core.Domain.Entities
{
    public class FriendshipChange
    {
        public int FriendshipChangeId { get; set; }
        public int FriendshipId { get; set; }
        public FriendshipStatusId StatusId { get; set; }
        public DateTime Created { get; set; }

        public Friendship Friendship { get; set; }
        public FriendshipStatus Status { get; set; }
    }
}
