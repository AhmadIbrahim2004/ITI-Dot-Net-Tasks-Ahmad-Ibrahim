using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Project.Filters
{
    // This is a custom Action Filter. It runs before any action in a controller it's applied to.
    public class AuthorizeStudentFilter : ActionFilterAttribute
    {
        // This method is executed right before the controller action.
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // We check the session to see if a "user" key has been set.
            // This is our simple way of checking if someone has "logged in".
            if (context.HttpContext.Session.GetString("user") == null)
            {
                // If there's no user in the session, they are not logged in.
                // We will redirect them to the Login page.
                context.Result = new RedirectToActionResult("Login", "Account", null);
            }

            // If a user *is* in the session, we do nothing and let the action proceed as normal.
            base.OnActionExecuting(context);
        }
    }
}

