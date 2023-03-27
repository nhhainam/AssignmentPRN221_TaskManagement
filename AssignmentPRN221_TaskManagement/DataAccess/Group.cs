using System;
using System.Collections.Generic;

namespace AssignmentPRN221_TaskManagement.DataAccess;

public partial class Group
{
    public int GroupId { get; set; }

    public string? GroupName { get; set; }

    public string? Description { get; set; }

    public string? Purpose { get; set; }

    public int? State { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<Invitation> Invitations { get; } = new List<Invitation>();

    public virtual ICollection<Member> Members { get; } = new List<Member>();

    public virtual ICollection<Project> Projects { get; } = new List<Project>();
}
