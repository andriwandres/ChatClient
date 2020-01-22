using System;
using System.Collections.Generic;

namespace ChatClient.Core.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public int AuthorId { get; set; }
        public int? ParentId { get; set; }
        public string TextContent { get; set; }
        public bool IsForwarded { get; set; }
        public bool IsEdited { get; set; }
        public DateTime CreatedAt { get; set; }

        public User Author { get; set; }
        public Message Parent { get; set; }
        public ICollection<MessageRecipient> Recipients { get; set; }
    }
}
