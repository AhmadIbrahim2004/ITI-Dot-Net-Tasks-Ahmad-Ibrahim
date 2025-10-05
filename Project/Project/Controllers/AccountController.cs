using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers
{
    public class AccountController : Controller
    {
        // This is a very simple controller to simulate login/logout for testing purposes.

        // GET: /Account/Login
        // This action displays the login form.
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        // This action handles the form submission.
        [HttpPost]
        public IActionResult Login(string username)
        {
            // For this simple example, we accept any username and consider the user logged in.
            // A real application would check a database for the username and a password.
            if (!string.IsNullOrEmpty(username))
            {
                // We store a simple key in the session to indicate the user is logged in.
                HttpContext.Session.SetString("user", username);
                return RedirectToAction("Index", "Home"); // Redirect to home page after login.
            }

            // If the username is empty, show an error and return to the login page.
            ViewBag.Error = "Username cannot be empty.";
            return View();
        }

        // --- NEW ACTION ---
        // GET: /Account/Logout
        // This action clears the session to log the user out.
        public IActionResult Logout()
        {
            // We remove the "user" key from the session.
            HttpContext.Session.Remove("user");
            return RedirectToAction("Index", "Home"); // Redirect to home page after logout.
        }
    }
}

