namespace Core.Domain.Dtos.Messages;

public class SendMessageBody
{
    public int RecipientId { get; set; }
    public int? ParentId { get; set; }
    public string HtmlContent { get; set; }
}