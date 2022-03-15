using System;
using System.Collections.Generic;

namespace Core.Domain.Entities;

public class Group
{
    public int GroupId { get; set; }
    public int? GroupImageId { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Created { get; set; }

    public bool IsDeleted { get; set; }

    public DisplayImage GroupImage { get; set; }
    public ICollection<GroupMembership> Memberships { get; set; }

    public Group()
    {
        Memberships = new HashSet<GroupMembership>();
    }
}