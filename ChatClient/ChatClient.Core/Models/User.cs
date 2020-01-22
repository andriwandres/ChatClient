using System;
using System.Collections.Generic;
using System.Text;

namespace ChatClient.Core.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserTag { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<GroupMembership> GroupMemberships { get; set; }
        public ICollection<MessageRecipient> ReceivedMessages { get; set; }
    }
}
