using System;

namespace Core.Domain.Dtos.Messages;

public class MessageBoundaries
{
    public int? Limit { get; set; }
    public DateTime? Before { get; set; }
    public DateTime? After { get; set; }
}