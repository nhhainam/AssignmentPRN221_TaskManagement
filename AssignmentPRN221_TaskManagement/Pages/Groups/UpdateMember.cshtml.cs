using AssignmentPRN221_TaskManagement.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AssignmentPRN221_TaskManagement.Pages.Groups
{
    [Authorize]
    public class UpdateMemberModel : PageModel
    {
        GroupManagementContext context = new GroupManagementContext();
        public void OnGet(string userid, string groupid)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            string username = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            User curUser = context.Users.SingleOrDefault(u => u.Username == username);

            User user = context.Users.SingleOrDefault(u => u.UserId == int.Parse(userid));
            List<Role> roles = context.Roles.ToList();
            List<Member> members = context.Members.Include(m => m.Group).Include(m => m.Role).Include(m => m.User).Where(m => m.GroupId == int.Parse(groupid) && m.Status == true).ToList();
            foreach (Member member in members)
            {
                if (member.UserId == user.UserId)
                {
                    ViewData["usermember"] = member;
                }
            }


            ViewData["curUser"] = curUser;
            ViewData["user"] = user;
            ViewData["roles"] = roles;
        }

        public void OnPost(string role, string userid, string groupid)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            string username = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            User curUser = context.Users.SingleOrDefault(u => u.Username == username);
            User user = context.Users.SingleOrDefault(u => u.UserId == int.Parse(userid));

            List<Member> members = context.Members.Include(m => m.Group).Include(m => m.Role).Include(m => m.User).Where(m => m.GroupId == int.Parse(groupid) && m.Status == true).ToList();
            foreach (Member member in members)
            {
                if (member.UserId == int.Parse(userid))
                {
                    //context.Entry(member).CurrentValues.SetValues(role);
                    member.RoleId = int.Parse(role);
                    context.SaveChanges();
                    ViewData["usermember"] = member;
                }
            }
            context.SaveChanges();
            List<Role> roles = context.Roles.ToList();


            ViewData["curUser"] = curUser;
            ViewData["user"] = user;
            ViewData["roles"] = roles;
        }
    }
}
