using ChatClient.Core.Models.Domain;
using System.Collections.Generic;
using System.Linq;

namespace ChatClient.Data.Extensions
{
    public static class QueryableExtensions
    {
        /// <summary>
        ///     Stores a unique target key alongside a Recipient
        /// </summary>
        private class RecipientTargetGroup
        {
            public RecipientTargetKey Key { get; set; }
            public MessageRecipient Recipient { get; set; }
        }

        /// <summary>
        ///     Uniquely identifies a chat target (eg. Group chat or private chat)
        /// </summary>
        private class RecipientTargetKey
        {
            public int? TargetUserId { get; set; }
            public int? GroupMembershipId { get; set; }
        }

        /// <summary>
        ///     This method performs grouping of messages by chat target and gets the 
        ///     latest message sent by that target.
        ///     
        ///     <para>The LINQ equivalent (which is not yet translatable by expression trees) would be:</para>
        ///     <code>
        ///         <para>recipients</para>
        ///         <para>.GroupBy(r => new { r.RecipientUserId, r.RecipientGroupId })</para>
        ///         <para>.Select(g => g.OrderByDescending(r => r.Message.CreatedAt).First())</para>
        ///     </code>
        /// </summary>
        /// <param name="source">
        ///     Source queryable to group by
        /// </param>
        /// <param name="userId">
        ///     User ID for whom to load the chat messages for
        /// </param>
        /// <returns>
        ///     List of latest <see cref="MessageRecipient"/> of chat targets that are releavant for the given user
        /// </returns>
        public static IQueryable<MessageRecipient> GroupByTargetAndGetLatestMessages(this IQueryable<MessageRecipient> source, int userId)
        {
            // Wrap all elements to target group objects
            IQueryable<RecipientTargetGroup> targetGroups = source.Select(recipient => new RecipientTargetGroup
            {
                Recipient = recipient,

                // The key property uniquely identifies a chat target
                Key = new RecipientTargetKey
                {
                    GroupMembershipId = recipient.RecipientGroupId,
                    TargetUserId = recipient.RecipientUserId == userId
                        ? recipient.Message.AuthorId
                        : recipient.RecipientUserId
                }
            });

            // Group by key and get the latest message of each chat target
            IQueryable<MessageRecipient> latestMessages = targetGroups
                .Select(group => group.Key)
                .Distinct()
                .SelectMany(key => targetGroups
                    .Where(group => group.Key.TargetUserId == key.TargetUserId && group.Key.GroupMembershipId == key.GroupMembershipId)
                    .Select(group => group.Recipient)
                    .OrderByDescending(r => r.Message.CreatedAt)
                    .Take(1)
                );

            return latestMessages;
        }
    }
}
