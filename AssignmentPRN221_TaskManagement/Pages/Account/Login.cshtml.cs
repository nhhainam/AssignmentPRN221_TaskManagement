using AssignmentPRN221_TaskManagement.DataAccess;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
            if (User.Identity.IsAuthenticated)
            {
                Response.Redirect("/Account/Home");
            }
        }
        public async Task<IActionResult> OnPostAsync(string usernameLogin, string passwordLogin)
        {
            if (User.Identity.IsAuthenticated)
            {
                Response.Redirect("/Account/Home");
            }

            User user = await context.Users.Include(u => u.Invitations)
                                             .SingleOrDefaultAsync(u => u.Username == usernameLogin
                                                                        && u.Password == passwordLogin
                                                                        && u.Status == true);
            if (user != null)
            {
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, "User")
        };
                var identity = new ClaimsIdentity(claims, "custom");
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(principal, new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddDays(7)
                });
                return RedirectToPage("/Account/Home");
            }
            else
            {
                ViewData["MessLogin"] = "Login failed";
                return Page();
            }
        }
    }
}
