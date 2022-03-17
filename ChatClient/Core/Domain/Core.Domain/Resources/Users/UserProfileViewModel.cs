using Core.Domain.Enums;

namespace Core.Domain.Resources.Users;

public class UserProfileViewModel
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public AvailabilityStatus AvailabilityStatus { get; set; }
}