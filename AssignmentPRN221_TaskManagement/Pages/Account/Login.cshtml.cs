using AssignmentPRN221_TaskManagement.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AssignmentPRN221_TaskManagement.Pages
{
    public class LoginModel : PageModel
    {
        GroupManagementContext context = new GroupManagementContext();
        public void OnGet()
        {
        }

        public void OnPost(string usernameLogin, string passwordLogin) 
        {
            User user = context.Users.SingleOrDefault(u => u.Username == usernameLogin && u.Password == passwordLogin && u.Status == true);
            if (user != null)
            {
                HttpContext.Session.SetString("username", user.Username);
                Response.Redirect("Home");
            }
            else
            {
                ViewData["usernameLogin"] = usernameLogin;
                ViewData["passwordLogin"] = passwordLogin;
                ViewData["MessLogin"] = "Login failed";
                //Response.Redirect("Register");
            }
        }
    }
}
