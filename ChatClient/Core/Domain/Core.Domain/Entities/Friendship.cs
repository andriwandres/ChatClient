using System.Collections.Generic;

namespace Core.Domain.Entities
{
    public class Friendship
    {
        public int FriendshipId { get; set; }
        public int RequesterId { get; set; }
        public int AddresseeId { get; set; }

        public User Requester { get; set; }
        public User Addressee { get; set; }

        public ICollection<FriendshipChange> StatusChanges { get; set; }

        public Friendship()
        {
            StatusChanges = new HashSet<FriendshipChange>();
        }
    }
}
