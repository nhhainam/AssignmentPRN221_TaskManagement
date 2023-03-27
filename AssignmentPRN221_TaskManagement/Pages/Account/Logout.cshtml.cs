using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AssignmentPRN221_TaskManagement.Pages.Account
{
    [Authorize]
    public class LogoutModel : PageModel
    {

        public void OnGet()
        {
            HttpContext.SignOutAsync();
            //HttpContext.Session.Clear();
            Response.Redirect("Login");
        }
    }
}
