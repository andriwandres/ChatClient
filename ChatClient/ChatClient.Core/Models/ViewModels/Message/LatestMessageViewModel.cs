using System;

namespace ChatClient.Core.Models.ViewModels.Message
{
    public class LatestMessageViewModel
    {
        public int MessageRecipientId { get; set; }
        public int MessageId { get; set; }
        public bool IsRead { get; set; }
        public int UnreadMessagesCount { get; set; }
        public string TextContent { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public byte[] Image { get; set; }
        public DateTime CreatedAt { get; set; }

        public RecipientUserViewModel RecipientUser { get; set; }
        public RecipientGroupViewModel RecipientGroup { get; set; }
    }

    public class RecipientUserViewModel
    {
        public int UserId { get; set; }
        public string DisplayName { get; set; }
        public bool IsOnline { get; set; }
    }

    public class RecipientGroupViewModel
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
    }
}
