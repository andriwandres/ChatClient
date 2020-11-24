namespace Core.Domain.Dtos.GroupMemberships
{
    public class CreateMembershipBody
    {
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public bool IsAdmin { get; set; }
    }
}
