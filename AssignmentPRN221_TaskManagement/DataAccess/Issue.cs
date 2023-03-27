using System;
using System.Collections.Generic;

namespace AssignmentPRN221_TaskManagement.DataAccess;

public partial class Issue
{
    public int IssueId { get; set; }

    public string Title { get; set; } = null!;

    public DateTime? DueDate { get; set; }

    public DateTime? StartDate { get; set; }

    public string? Description { get; set; }

    public string? Content { get; set; }

    public int? State { get; set; }

    public int? Creator { get; set; }

    public int? Assignee { get; set; }

    public int? ProjectId { get; set; }

    public bool? Status { get; set; }

    public virtual User? AssigneeNavigation { get; set; }

    public virtual User? CreatorNavigation { get; set; }

    public virtual ICollection<MemberIssue> MemberIssues { get; } = new List<MemberIssue>();

    public virtual Project? Project { get; set; }
}
