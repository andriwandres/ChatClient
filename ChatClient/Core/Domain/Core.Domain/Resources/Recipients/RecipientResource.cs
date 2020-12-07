using Core.Domain.Entities;
using Core.Domain.Resources.Groups;
using Core.Domain.Resources.Messages;
using Core.Domain.Resources.Users;

namespace Core.Domain.Resources.Recipients
{
    public class RecipientResource
    {
        public int RecipientId { get; set; }
        public AvailabilityStatusId? AvailabilityStatusId { get; set; }
        public int UnreadMessagesCount { get; set; }
        public bool IsPinned { get; set; }
        public TargetGroupResource TargetGroup { get; set; }
        public TargetUserResource TargetUser { get; set; }
        public LatestMessageResource LatestMessage { get; set; }
    }
}
