using System;

namespace ChatClient.Core.Models.ViewModels.Message
{
    public class ChatMessageViewModel
    {
        public int MessageRecipientId { get; set; }
        public int MessageId { get; set; }
        public string AuthorName { get; set; }
        public string TextContent { get; set; }
        public bool IsRead { get; set; }
        public bool IsOwnMessage { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
