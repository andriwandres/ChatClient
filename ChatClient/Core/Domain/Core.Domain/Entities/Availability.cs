using System;

namespace Core.Domain.Entities
{
    public class Availability
    {
        public int AvailabilityId { get; set; }
        public AvailabilityStatusId StatusId { get; set; }
        public int UserId { get; set; }
        public bool ModifiedManually { get; set; }
        public DateTime Modified { get; set; }

        public User User { get; set; }
        public AvailabilityStatus Status { get; set; }
    }
}
