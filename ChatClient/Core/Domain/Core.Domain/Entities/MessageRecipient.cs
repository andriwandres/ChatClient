using System;

namespace Core.Domain.Entities
{
    public class MessageRecipient
    {
        public int MessageRecipientId { get; set; }
        public int RecipientId { get; set; }
        public int MessageId { get; set; }
        public bool IsForwarded { get; set; }
        public bool IsRead { get; set; }
        public DateTime ReadDate { get; set; }

        public Recipient Recipient { get; set; }
        public Message Message { get; set; }
    }
}
