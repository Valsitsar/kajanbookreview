using BusinessLogicLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web_App.Pages
{
    public class SignInModel : PageModel
    {
        [BindProperty]
        public string UsernameOrEmail { get; set; }
        [BindProperty]
        public string Password { get; set; }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) { return Page(); }

            // TODO: Implement sign-in logic here (e.g. check if the user exists in the database)
            //User user = null;
            //if (UsernameOrEmail.Contains('@'))
            //{
            //    // Treat as an email
            //    // TODO: Implement method, e.g. user = _userManager.AuthenticateByEmail(UsernameOrEmail, Password);
            //}
            //else
            //{
            //    // Treat as a username
            //    // TODO: Implement method, e.g. user = _userManager.AuthenticateByUsername(UsernameOrEmail, Password);
            //}


            //if (user != null)
            //{
            //    // Login successful
            //    return RedirectToPage("Index");
            //}
            //if (user == null)
            //{
            //    // Login failed
            //    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            //    return Page();
            //}


            // Redirect to the home page for now
            return RedirectToPage("Index");
        }
    }
}
