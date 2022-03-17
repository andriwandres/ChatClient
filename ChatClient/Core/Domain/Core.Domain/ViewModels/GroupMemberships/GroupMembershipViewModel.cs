namespace Core.Domain.ViewModels.GroupMemberships;

public class GroupMembershipViewModel
{
    public int GroupMembershipId { get; set; }
    public int GroupId { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
    public bool IsAdmin { get; set; }
}