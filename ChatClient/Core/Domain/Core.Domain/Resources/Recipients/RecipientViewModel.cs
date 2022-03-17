using Core.Domain.Enums;
using Core.Domain.Resources.Groups;
using Core.Domain.Resources.Messages;
using Core.Domain.Resources.Users;

namespace Core.Domain.Resources.Recipients;

public class RecipientViewModel
{
    public int RecipientId { get; set; }
    public int UnreadMessagesCount { get; set; }
    public bool IsPinned { get; set; }
    public AvailabilityStatus? AvailabilityStatus { get; set; }
    public TargetGroupViewModel TargetGroup { get; set; }
    public TargetUserViewModel TargetUser { get; set; }
    public LatestMessageViewModel LatestMessage { get; set; }
}