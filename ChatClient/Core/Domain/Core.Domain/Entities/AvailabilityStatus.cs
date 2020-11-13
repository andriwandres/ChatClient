using System.Collections.Generic;

namespace Core.Domain.Entities
{
    public class AvailabilityStatus
    {
        public AvailabilityStatusId AvailabilityStatusId { get; set; }
        public string Name { get; set; }
        public string IndicatorColor { get; set; }
        public string IndicatorOverlay { get; set; }

        public ICollection<Availability> Availabilities { get; set; }
    }

    public enum AvailabilityStatusId : int
    {
        Online = 1,
        Away = 2,
        Busy = 3,
        DoNotDisturb = 4,
        Offline = 5
    }
}
