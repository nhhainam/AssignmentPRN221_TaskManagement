using AssignmentPRN221_TaskManagement.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.RegularExpressions;
using Group = AssignmentPRN221_TaskManagement.DataAccess.Group;

namespace AssignmentPRN221_TaskManagement.Pages.Issues
{
    [Authorize]
    public class ViewIssueModel : PageModel
    {
        GroupManagementContext context = new GroupManagementContext();
        public IActionResult OnGet(string projectid, string groupid)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            string username = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            User user = context.Users.SingleOrDefault(u => u.Username == username && u.Status == true);
            Member member = context.Members.SingleOrDefault(m => m.GroupId == int.Parse(groupid) && m.UserId == user.UserId && m.Status == true);
            Group group = context.Groups.SingleOrDefault(g => g.GroupId == int.Parse(groupid));
            Project project = context.Projects.SingleOrDefault(p => p.ProjectId == int.Parse(projectid));

            if (member == null || project == null || project.GroupId != group.GroupId)
            {
                return RedirectToPage("/AccessDenied");
            }

            List<Issue> issues = context.Issues.Include(i => i.Project).Where(i => i.ProjectId == int.Parse(projectid) && i.Status == true).ToList();

            ViewData["user"] = user;
            ViewData["member"] = member;
            ViewData["group"] = group;
            ViewData["project"] = project;
            ViewData["issues"] = issues;
            return Page();
        }

        public IActionResult OnPostRemoveIssue(string projectid, string groupid, string issueid)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            string username = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            User user = context.Users.SingleOrDefault(u => u.Username == username && u.Status == true);
            Member member = context.Members.SingleOrDefault(m => m.GroupId == int.Parse(groupid) && m.UserId == user.UserId && m.Status == true);
            Group group = context.Groups.SingleOrDefault(g => g.GroupId == int.Parse(groupid));
            Project project = context.Projects.SingleOrDefault(p => p.ProjectId == int.Parse(projectid));
            Issue issue = context.Issues.SingleOrDefault(i => i.IssueId == int.Parse(issueid) && i.Status == true);
            issue.Status = false;
            context.SaveChanges();


            if (member == null || project == null || project.GroupId != group.GroupId)
            {
                return RedirectToPage("/AccessDenied");
            }

            List<Issue> issues = context.Issues.Include(i => i.Project).Where(i => i.ProjectId == int.Parse(projectid) && i.Status == true).ToList();

            ViewData["user"] = user;
            ViewData["member"] = member;
            ViewData["group"] = group;
            ViewData["project"] = project;
            ViewData["issues"] = issues;

            return Page();
        }
    }
}
