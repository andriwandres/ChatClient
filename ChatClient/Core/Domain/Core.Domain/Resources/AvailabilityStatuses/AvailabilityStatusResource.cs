using Core.Domain.Enums;

namespace Core.Domain.Resources.AvailabilityStatuses
{
    public class AvailabilityStatusResource
    {
        public AvailabilityStatus AvailabilityStatus { get; set; }
        public string Name { get; set; }
        public string IndicatorColor { get; set; }
        public string IndicatorOverlay { get; set; }
    }
}
