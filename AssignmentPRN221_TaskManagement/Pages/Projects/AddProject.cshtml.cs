using AssignmentPRN221_TaskManagement.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace AssignmentPRN221_TaskManagement.Pages.Projects
{
    [Authorize]
    public class AddProjectModel : PageModel
    {
        GroupManagementContext context = new GroupManagementContext();
        public void OnGet(string groupid)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            string username = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            User user = context.Users.SingleOrDefault(u => u.Username == username);
            Group group = context.Groups.SingleOrDefault(g => g.GroupId == int.Parse(groupid));

            ViewData["group"] = group;
            ViewData["user"] = user;
        }

        public void OnPost(string projectname, string description, string groupid)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            string username = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            User user = context.Users.SingleOrDefault(u => u.Username == username);
            Group group = context.Groups.SingleOrDefault(g => g.GroupId == int.Parse(groupid));
            ViewData["user"] = user;
            ViewData["group"] = group;

            Project project = new Project();
            project.ProjectName = projectname;
            project.Description = description;
            project.Createdate = DateTime.Now;
            project.Group = group;
            project.Status = true;
            context.Projects.Add(project);
            context.SaveChanges();

            ViewData["MessAddProject"] = "Add Project successfully";
        }
    }
}
