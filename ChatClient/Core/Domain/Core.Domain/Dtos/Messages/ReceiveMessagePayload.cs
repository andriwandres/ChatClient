using Core.Domain.Resources.Messages;

namespace Core.Domain.Dtos.Messages;

public class ReceiveMessagePayload
{
    public int RecipientId { get; set; }
    public ChatMessageViewModel Message { get; set; }
}