using AssignmentPRN221_TaskManagement.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AssignmentPRN221_TaskManagement.Pages
{
    [Authorize]
    public class HomeModel : PageModel
    {
        GroupManagementContext context = new GroupManagementContext();

        public void OnGet()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            string username = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            User user = context.Users.SingleOrDefault(u => u.Username == username);
            List<Member> members = context.Members.Include(m => m.Group).Include(m => m.Role).Where(m => m.UserId == user.UserId && m.Group.Status == true && m.Status == true && m.State==1).ToList();
            //List<Group> groups = context.Groups.Include(x => x.Members).Where(g => g.Members.Contains(member)).ToList();
            ViewData["user"] = user;
            ViewData["members"] = members;
            //ViewData["groups"] = groups;
        }

        public void OnPostLeaveGroup(string groupid)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            string username = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            User user = context.Users.SingleOrDefault(u => u.Username == username);

            Group group = context.Groups.Include(g=>g.Members).SingleOrDefault(g => g.GroupId == int.Parse(groupid));
            List<Member> members = group.Members.Where(m => m.GroupId == group.GroupId && m.UserId == user.UserId).ToList();
            foreach (Member m in members)
            {
                context.Remove(m);
            }
            context.SaveChanges();


            ViewData["MessLeaveGroup"] = "Leave group " + group.GroupName + " successfully";

            List<Member> memberList = context.Members.Include(m => m.Group).Include(m => m.Role).Where(m => m.UserId == user.UserId && m.Group.Status == true && m.Status == true && m.State==1).ToList();
            ViewData["user"] = user;
            ViewData["members"] = memberList;
        }
    }
}
