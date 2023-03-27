using System;
using System.Collections.Generic;

namespace AssignmentPRN221_TaskManagement.DataAccess;

public partial class Invitation
{
    public int UserId { get; set; }

    public int GroupId { get; set; }

    public bool? Status { get; set; }

    public virtual Group Group { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
