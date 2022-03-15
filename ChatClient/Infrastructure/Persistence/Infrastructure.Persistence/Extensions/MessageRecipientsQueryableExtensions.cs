using Core.Domain.Entities;
using System.Linq;

namespace Infrastructure.Persistence.Extensions;

public static class MessageRecipientsQueryableExtensions
{
    private class MessageRecipientTargetKey
    {
        public int TargetUserId { get; set; }
        public int GroupMembershipId { get; set; }
    }

    private class MessageRecipientTargetGroup
    {
        public MessageRecipientTargetKey Key { get; set; }
        public MessageRecipient MessageRecipient { get; set; }
    }

    public static IQueryable<MessageRecipient> GroupByTargetAndGetLatest(this IQueryable<MessageRecipient> source, int userId)
    {
        // Map all relevant message recipients to target group objects
        IQueryable<MessageRecipientTargetGroup> targetGroups = source.Select(mr => new MessageRecipientTargetGroup
        {
            MessageRecipient = mr,

            // The 'Key' uniquely identifies a chat target
            Key = new MessageRecipientTargetKey
            {
                GroupMembershipId = mr.Recipient.GroupMembershipId ?? 0,
                TargetUserId = mr.Recipient.UserId == userId
                    ? mr.Message.AuthorId
                    : mr.Recipient.UserId ?? 0
            }
        });

        // Group by key and get the latest message of each chat target
        IQueryable<MessageRecipient> latestMessages = targetGroups
            .Select(group => group.Key)
            .Distinct()
            .SelectMany(key => targetGroups
                .Where(group =>
                    group.Key.TargetUserId == key.TargetUserId &&
                    group.Key.GroupMembershipId == key.GroupMembershipId
                )
                .Select(group => group.MessageRecipient)
                .OrderByDescending(mr => mr.Message.Created)
                .Take(1)
            );

        return latestMessages;
    }
}