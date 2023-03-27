using System;
using System.Collections.Generic;

namespace AssignmentPRN221_TaskManagement.DataAccess;

public partial class Member
{
    public int UserId { get; set; }

    public int GroupId { get; set; }

    public int? RoleId { get; set; }

    public int? State { get; set; }

    public bool? Status { get; set; }

    public virtual Group Group { get; set; } = null!;

    public virtual Role? Role { get; set; }

    public virtual User User { get; set; } = null!;
}
