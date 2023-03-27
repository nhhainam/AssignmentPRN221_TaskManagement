using AssignmentPRN221_TaskManagement.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace AssignmentPRN221_TaskManagement.Pages.Account
{
    public class ViewInvitationModel : PageModel
    {
        GroupManagementContext context = new GroupManagementContext();
        public void OnGet()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            string username = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            User user = context.Users.SingleOrDefault(u => u.Username == username && u.Status == true);
            List<Member> invitations = context.Members.Include(m => m.Group).Include(m => m.Role).Include(m => m.User).Where(m => m.UserId == user.UserId && m.State == 0 && m.Status == true).ToList();


            ViewData["user"] = user;
            ViewData["invitations"] = invitations;
        }

        public void OnPostAcceptInvitation(string groupid)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            string username = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            User user = context.Users.SingleOrDefault(u => u.Username == username && u.Status == true);
            Member invitation = context.Members.FirstOrDefault(i => i.UserId == user.UserId && i.GroupId == int.Parse(groupid) && i.Status == true);
            invitation.State = 1;
            context.SaveChanges();
            List<Member> invitations = context.Members.Include(m => m.Group).Include(m => m.Role).Include(m => m.User).Where(m => m.UserId == user.UserId && m.State == 0 && m.Status == true).ToList();

            ViewData["user"] = user;
            ViewData["invitations"] = invitations;
        }
        public void OnPostRefuseInvitation(string groupid)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            string username = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            User user = context.Users.SingleOrDefault(u => u.Username == username && u.Status == true);
            Member invitation = context.Members.FirstOrDefault(i => i.UserId == user.UserId && i.GroupId == int.Parse(groupid) && i.Status == true);
            context.Remove(invitation);
            context.SaveChanges();
            List<Member> invitations = context.Members.Include(m => m.Group).Include(m => m.Role).Include(m => m.User).Where(m => m.UserId == user.UserId && m.State == 0 && m.Status == true).ToList();

            ViewData["user"] = user;
            ViewData["invitations"] = invitations;
            ViewData["MessInvite"] = "Refused invitation";
        }
    }
}
