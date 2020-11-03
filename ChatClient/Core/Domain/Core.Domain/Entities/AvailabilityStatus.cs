using System.Collections.Generic;

namespace Core.Domain.Entities
{
    public class AvailabilityStatus
    {
        public int AvailabilityStatusId { get; set; }
        public string Name { get; set; }
        public string IndicatorColor { get; set; }
        public string IndicatorOverlay { get; set; }

        public ICollection<Availability> Availabilities { get; set; }
    }
}
