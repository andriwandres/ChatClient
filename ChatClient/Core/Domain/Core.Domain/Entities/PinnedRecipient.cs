using System;

namespace Core.Domain.Entities
{
    public class PinnedRecipient
    {
        public int PinnedRecipientId { get; set; }
        public int RecipientId { get; set; }
        public int UserId { get; set; }
        public int OrderIndex { get; set; }
        public DateTime Modified { get; set; }

        public User User { get; set; }
        public Recipient Recipient { get; set; }
    }
}
