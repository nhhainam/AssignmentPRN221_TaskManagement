using System;
using System.Collections.Generic;

namespace AssignmentPRN221_TaskManagement.DataAccess;

public partial class MemberIssue
{
    public int UserId { get; set; }

    public int IssueId { get; set; }

    public bool? Status { get; set; }

    public virtual Issue Issue { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
