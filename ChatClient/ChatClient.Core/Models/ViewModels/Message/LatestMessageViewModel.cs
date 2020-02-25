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

        public RecipientUser UserRecipient { get; set; }
        public RecipientGroup GroupRecipient { get; set; }
        
        #region Nested Classes

        public class RecipientUser
        {
            public int UserId { get; set; }
            public string DisplayName { get; set; }
            public bool IsOnline { get; set; }
        }

        public class RecipientGroup
        {
            public int GroupId { get; set; }
            public string Name { get; set; }
        }

        #endregion
    }
}
