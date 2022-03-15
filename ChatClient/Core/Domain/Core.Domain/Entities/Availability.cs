using Core.Domain.Enums;
using System;

namespace Core.Domain.Entities;

public class Availability
{
    public int AvailabilityId { get; set; }
    public int UserId { get; set; }
    public bool ModifiedManually { get; set; }
    public string StatusMessage { get; set; }
    public AvailabilityStatus Status { get; set; }
    public DateTime Modified { get; set; }

    public User User { get; set; }
}