namespace Core.Domain.Resources.Friendships;

public class FriendshipResource
{
    public int FriendshipId { get; set; }
    public int RequesterId { get; set; }
    public int AddresseeId { get; set; }
}