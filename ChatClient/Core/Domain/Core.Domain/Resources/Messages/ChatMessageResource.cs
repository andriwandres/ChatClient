using System;

namespace Core.Domain.Resources.Messages
{
    public class ChatMessageResource
    {
        public int MessageRecipientId { get; set; }
        public int MessageId { get; set; }
        public string AuthorName { get; set; }
        public string HtmlContent { get; set; }
        public bool IsRead { get; set; }
        public bool IsOwnMessage { get; set; }
        public DateTime Created { get; set; }
    }
}
