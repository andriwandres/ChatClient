using Core.Domain.Enums;

namespace Core.Domain.Resources.Users
{
    public class UserProfileResource
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public AvailabilityStatus AvailabilityStatus { get; set; }
    }
}
