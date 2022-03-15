using System;
using System.Collections.Generic;

namespace Core.Domain.Entities;

public class User
{
    public int UserId { get; set; }
    public int? ProfileImageId { get; set; }


    public string UserName { get; set; }
    public string Email { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public DateTime Created { get; set; }
    public bool IsEmailConfirmed { get; set; }

    public Recipient Recipient { get; set; }
    public Availability Availability { get; set; }
    public DisplayImage ProfileImage { get; set; }

    public ICollection<RedeemToken> RedeemTokens { get; set; }
    public ICollection<ArchivedRecipient> ArchivedRecipients { get; set; }
    public ICollection<PinnedRecipient> PinnedRecipients { get; set; }
    public ICollection<Friendship> AddressedFriendships{ get; set; }
    public ICollection<Friendship> RequestedFriendships { get; set; }
    public ICollection<GroupMembership> GroupMemberships { get; set; }
    public ICollection<Message> AuthoredMessages { get; set; }
    public ICollection<MessageReaction> MessageReactions { get; set; }
    public ICollection<NicknameAssignment> AddressedNicknames { get; set; }
    public ICollection<NicknameAssignment> RequestedNicknames { get; set; }

    public User()
    {
        RedeemTokens = new HashSet<RedeemToken>();
        ArchivedRecipients = new HashSet<ArchivedRecipient>();
        PinnedRecipients = new HashSet<PinnedRecipient>();
        AddressedFriendships = new HashSet<Friendship>();
        RequestedFriendships = new HashSet<Friendship>();
        GroupMemberships = new HashSet<GroupMembership>();
        AuthoredMessages = new HashSet<Message>();
        MessageReactions = new HashSet<MessageReaction>();
        AddressedNicknames = new HashSet<NicknameAssignment>();
        RequestedNicknames = new HashSet<NicknameAssignment>();
    }
}