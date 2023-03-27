using AssignmentPRN221_TaskManagement.DataAccess;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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
            User user = context.Users.Include(u=>u.Invitations).SingleOrDefault(u => u.Username == usernameLogin && u.Password == passwordLogin && u.Status == true);
            if (user != null)
            {
                //Add claims for logged in user
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    //get roles of user and assign it to claims, if you are using database
                    //new Claim(ClaimTypes.Role, user.Role)

                    new Claim(ClaimTypes.Role, "User")
                };
                var identity = new ClaimsIdentity(claims, "custom");
                var principal = new ClaimsPrincipal(identity);
                HttpContext.SignInAsync(principal, new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddDays(7)
                });
                Response.Redirect("Home");
            }
            else
            {

                //ViewData["usernameLogin"] = usernameLogin;
                //ViewData["passwordLogin"] = passwordLogin;
                ViewData["MessLogin"] = "Login failed";
                //Response.Redirect("Register");
            }
        }
    }
}
