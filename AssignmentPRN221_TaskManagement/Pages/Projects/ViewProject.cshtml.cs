using AssignmentPRN221_TaskManagement.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AssignmentPRN221_TaskManagement.Pages.Projects
{
    [Authorize]
    public class ViewProjectModel : PageModel
    {
        GroupManagementContext context = new GroupManagementContext();
        public IActionResult OnGet(string groupid)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            string username = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            User user = context.Users.SingleOrDefault(u => u.Username == username && u.Status == true);
            Member member = context.Members.FirstOrDefault(m => m.UserId == user.UserId && m.Group.GroupId == int.Parse(groupid) && m.Status == true);

            if (member == null)
            {
                return RedirectToPage("/AccessDenied");
            }

            List<Project> projects = context.Projects.Include(p => p.Group.Members).Where(p => p.Group.Members.Contains(member) && p.GroupId == int.Parse(groupid) && p.Status == true).ToList();
            Group group = context.Groups.SingleOrDefault(g => g.GroupId == int.Parse(groupid));

            ViewData["group"] = group;
            ViewData["user"] = user;
            ViewData["projects"] = projects;

            return Page();
        }

        public IActionResult OnPostRemoveProject(string projectid, string groupid)
        {
            Project project = context.Projects.SingleOrDefault(p => p.ProjectId == int.Parse(projectid));
            project.Status = false;
            context.SaveChanges();


            ViewData["MessProjectRemove"] = "Remove Project successfully";


            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            string username = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            User user = context.Users.SingleOrDefault(u => u.Username == username && u.Status == true);
            Member member = context.Members.FirstOrDefault(m => m.UserId == user.UserId && m.Group.GroupId == int.Parse(groupid) && m.Status == true);

            if (member == null)
            {
                return RedirectToPage("/AccessDenied");
            }
            List<Project> projects = context.Projects.Include(p => p.Group.Members).Where(p => p.Group.Members.Contains(member) && p.GroupId == int.Parse(groupid) && p.Status == true).ToList();
            Group group = context.Groups.SingleOrDefault(g => g.GroupId == int.Parse(groupid));

            ViewData["group"] = group;
            ViewData["user"] = user;
            ViewData["projects"] = projects;

            return Page();
        }
    }
}
