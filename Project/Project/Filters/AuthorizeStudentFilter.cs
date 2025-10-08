using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Project.Filters
{
    // This is a custom Action Filter. It runs before any action in a controller it's applied to.
    public class AuthorizeStudentFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // --- THIS IS THE CORRECT LOGIC ---
            // It now checks if the user is genuinely authenticated by ASP.NET Core Identity.
            if (context.HttpContext.User.Identity == null || !context.HttpContext.User.Identity.IsAuthenticated)
            {
                // If not logged in, redirect to the REAL Identity login page.
                context.Result = new RedirectToActionResult("Login", "Account", new { area = "Identity" });
            }
            base.OnActionExecuting(context);
        }
    }
}

