using System;
using System.Collections.Generic;

namespace AssignmentPRN221_TaskManagement.DataAccess;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Fullname { get; set; } = null!;

    public string? Email { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<Invitation> Invitations { get; } = new List<Invitation>();

    public virtual ICollection<Issue> IssueAssigneeNavigations { get; } = new List<Issue>();

    public virtual ICollection<Issue> IssueCreatorNavigations { get; } = new List<Issue>();

    public virtual ICollection<MemberIssue> MemberIssues { get; } = new List<MemberIssue>();

    public virtual ICollection<Member> Members { get; } = new List<Member>();
}
