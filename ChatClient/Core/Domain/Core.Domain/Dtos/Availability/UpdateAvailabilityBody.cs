using Core.Domain.Enums;

namespace Core.Domain.Dtos.Availability;

public class UpdateAvailabilityBody
{
    public AvailabilityStatus AvailabilityStatus { get; set; }
}