namespace Core.Domain.Resources.GroupMemberships
{
    public class GroupMembershipResource
    {
        public int GroupMembershipId { get; set; }
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public bool IsAdmin { get; set; }
    }
}
