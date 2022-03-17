using System;

namespace Core.Domain.Resources.Messages;

public class LatestMessageViewModel
{
    public int MessageId { get; set; }
    public int MessageRecipientId { get; set; }
    public int AuthorId { get; set; }
    public string AuthorName { get; set; }
    public string HtmlContent { get; set; }
    public bool IsRead { get; set; }
    public bool IsOwnMessage { get; set; }
    public DateTime Created { get; set; }
}