using AssignmentPRN221_TaskManagement.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics.Metrics;

namespace AssignmentPRN221_TaskManagement.Pages
{
    public class RegisterModel : PageModel
    {
        GroupManagementContext context = new GroupManagementContext();
        public void OnGet()
        {
        }
        public void OnPost(string usernameRegis, string passwordRegis, string repasswordRegis, string fullnameRegis, string emailRegis)
        {
            User user = context.Users.SingleOrDefault(u => u.Username == usernameRegis);
            if (user == null)
            {
                user = new User();
                user.Username = usernameRegis;
                user.Password = passwordRegis;
                user.Fullname = fullnameRegis;
                user.Email = emailRegis;
                user.Status = true;
                context.Users.Add(user);
                context.SaveChanges();
                Response.Redirect("Index");
            }
            else
            {
                ViewData["usernameRegis"] = usernameRegis;
                ViewData["passwordRegis"] = passwordRegis;
                ViewData["repasswordRegis"] = repasswordRegis;
                ViewData["fullnameRegis"] = fullnameRegis;
                ViewData["emailRegis"] = emailRegis;
                ViewData["MessRegis"] = "Register failed";
                //Response.Redirect("Register");
            }

        }
    }
}
