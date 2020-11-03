using System.Collections.Generic;

namespace Core.Domain.Entities
{
    public class FriendshipStatus
    {
        public int FriendshipStatusId { get; set; }
        public string Name { get; set; }

        public ICollection<FriendshipChange> StatusChanges { get; set; }
    }
}
