using AssignmentPRN221_TaskManagement.DataAccess;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AssignmentPRN221_TaskManagement.Pages
{
    public class HomeModel : PageModel
    {
        GroupManagementContext context = new GroupManagementContext();
        public void OnGet()
        {
            var username = HttpContext.Session.GetString("username");
            User user = context.Users.SingleOrDefault(u => u.Username == username);
            List<Member> members = context.Members.Include(m => m.Group).Include(m => m.Role).Where(m => m.UserId == user.UserId && m.Group.Status == true && m.Status == true).ToList();
            //List<Group> groups = context.Groups.Include(x => x.Members).Where(g => g.Members.Contains(member)).ToList();
            ViewData["user"] = user;
            ViewData["members"] = members;
            //ViewData["groups"] = groups;
        }

        public void OnPostLeaveGroup(string groupid)
        {

            var username = HttpContext.Session.GetString("username");
            User user = context.Users.SingleOrDefault(u => u.Username == username);

            Group group = context.Groups.SingleOrDefault(g => g.GroupId == int.Parse(groupid));
            List<Member> members = group.Members.Where(m => m.GroupId == group.GroupId && m.UserId == user.UserId).ToList();
            foreach (Member m in members)
            {
                m.Status = false;
            }
            context.SaveChanges();


            ViewData["MessLeaveGroup"] = "Leave group " + group.GroupName + " successfully";

            Member member = context.Members.SingleOrDefault(m => m.UserId == user.UserId);
            List<Group> groups = context.Groups.Include(x => x.Members).Where(g => g.Members.Contains(member)).ToList();
            ViewData["user"] = user;
            ViewData["groups"] = groups;
        }

    }
}
