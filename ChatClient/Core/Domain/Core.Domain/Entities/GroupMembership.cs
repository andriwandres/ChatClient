﻿using System;

namespace Core.Domain.Entities;

public class GroupMembership
{
    public int GroupMembershipId { get; set; }
    public int GroupId { get; set; }
    public int UserId { get; set; }
    public bool IsAdmin { get; set; }
    public DateTime Created { get; set; }

    public User User { get; set; }
    public Group Group { get; set; }
    public Recipient Recipient { get; set; }
}