using System;

namespace Core.Domain.Resources.Groups;

public class GroupViewModel
{
    public int GroupId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Created { get; set; }
}