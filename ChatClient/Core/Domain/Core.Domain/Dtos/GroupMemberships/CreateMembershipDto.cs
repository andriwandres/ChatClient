namespace Core.Domain.Dtos.GroupMemberships
{
    public class CreateMembershipDto
    {
        public int UserId { get; set; }
        public int GroupId { get; set; }
    }
}
