using AssignmentPRN221_TaskManagement.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AssignmentPRN221_TaskManagement.Pages.Groups
{
    public class AddGroupModel : PageModel
    {
        GroupManagementContext context = new GroupManagementContext();
        public void OnGet()
        {
            var username = HttpContext.Session.GetString("username");
            User curUser = context.Users.SingleOrDefault(u => u.Username == username);
            ViewData["curUser"] = curUser;
        }
        public Boolean CheckGroupName(string name)
        {
            try
            {
                var groupname = context.Groups.Where(g => g.GroupName.Equals(name));
                return groupname.ToList().Count() != 0;
            }
            catch (Exception ex)
            {
                ex.StackTrace.ToString();
                return false;
            }
        }

        public void OnPost(string groupname, string description, string purpose, string state)
        {

            var username = HttpContext.Session.GetString("username");
            User curUser = context.Users.SingleOrDefault(u => u.Username == username);
            ViewData["curUser"] = curUser;


            Group group = new Group();
            int privateOrPublic;
            Boolean isExist = CheckGroupName(groupname);
            if (isExist == true)
            {
                ViewData["MessAddGroup"] = "Group name duplicate";
            }

            else
            {
                group.GroupName = groupname;
                group.Description = description;
                if (state.Equals("Private"))
                {
                    privateOrPublic = 0;
                }
                else
                {
                    privateOrPublic = 1;
                }
                group.State = privateOrPublic;
                group.Purpose = purpose;
                group.Status = true;
                context.Groups.Add(group);
                context.SaveChanges();

                //Add first member
                Member m = new Member { UserId = curUser.UserId, GroupId = group.GroupId, RoleId = 1, State = 1, Status = true };
                context.Members.Add(m);
                context.SaveChanges();


                ViewData["MessAddGroup"] = "Add group successfully";
            }
        }
    }
}
