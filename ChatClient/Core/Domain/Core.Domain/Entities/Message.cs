using System;
using System.Collections.Generic;

namespace Core.Domain.Entities;

public class Message
{
    public int MessageId { get; set; }
    public int AuthorId { get; set; }
    public int? ParentId { get; set; }
    public string HtmlContent { get; set; }
    public bool IsEdited { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime Created { get; set; }

    public User Author { get; set; }
    public Message Parent { get; set; }

    public ICollection<MessageReaction> Reactions { get; set; }
    public ICollection<MessageRecipient> MessageRecipients { get; set; }

    public Message()
    {
        Reactions = new HashSet<MessageReaction>();
        MessageRecipients = new HashSet<MessageRecipient>();
    }
}