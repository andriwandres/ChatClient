using Core.Domain.Entities;

namespace Core.Domain.Resources.AvailabilityStatuses
{
    public class AvailabilityStatusResource
    {
        public AvailabilityStatusId AvailabilityStatusId { get; set; }
        public string Name { get; set; }
        public string IndicatorColor { get; set; }
        public string IndicatorOverlay { get; set; }
    }
}
