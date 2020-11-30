using System;

namespace Core.Domain.Resources.Messages
{
    public class MessageResource
    {
        public int MessageId { get; set; }
        public int MessageRecipientId { get; set; }
        public int AuthorId { get; set; }
        public int? ParentId { get; set; }

        public string AuthorName { get; set; }
        public string HtmlContent { get; set; }

        public DateTime Created { get; set; }
        public DateTime? ReadDate { get; set; }

        public bool IsRead { get; set; }
        public bool IsEdited { get; set; }
        public bool IsOwnMessage { get; set; }
    }
}
