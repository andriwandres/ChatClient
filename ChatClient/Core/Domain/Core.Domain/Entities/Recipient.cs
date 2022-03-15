using System.Collections.Generic;

namespace Core.Domain.Entities;

public class Recipient
{
    public int RecipientId { get; set; }
    public int? GroupMembershipId { get; set; }
    public int? UserId { get; set; }

    public User User { get; set; }
    public GroupMembership GroupMembership { get; set; }

    public ICollection<PinnedRecipient> Pins { get; set; }
    public ICollection<ArchivedRecipient> Archives { get; set; }
    public ICollection<MessageRecipient> ReceivedMessages { get; set; }

    public Recipient()
    {
        Pins = new HashSet<PinnedRecipient>();
        Archives = new HashSet<ArchivedRecipient>();
        ReceivedMessages = new HashSet<MessageRecipient>();
    }
}