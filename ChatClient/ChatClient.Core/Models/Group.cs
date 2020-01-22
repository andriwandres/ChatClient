using System;
using System.Collections.Generic;
using System.Text;

namespace ChatClient.Core.Models
{
    public class Group
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<GroupMembership> GroupMemberships { get; set; }
    }
}
