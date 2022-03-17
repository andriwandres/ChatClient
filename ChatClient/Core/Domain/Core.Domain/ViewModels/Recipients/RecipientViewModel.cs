using Core.Domain.Enums;
using Core.Domain.ViewModels.Groups;
using Core.Domain.ViewModels.Messages;
using Core.Domain.ViewModels.Users;

namespace Core.Domain.ViewModels.Recipients;

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