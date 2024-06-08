using BusinessLogicLayer.ManagerClasses;
using BusinessLogicLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessLogicLayer;

namespace Web_App.Pages
{
    public class SignUpModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public string ConfirmPassword { get; set; }

        private readonly UserManager _userManager;
        private readonly PasswordHasher _passwordHasher;

        public SignUpModel(UserManager userManager)
        {
            _userManager = userManager;
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) { return Page(); }

            // TODO: Implement sign-up logic here (e.g. add the user to the database)
            if (Password != ConfirmPassword)
            {
                ModelState.AddModelError(string.Empty, "Passwords do not match.");
                return Page();
            }

            bool createSuccessful = false;
            try
            {
                (string hashedPassword, string salt) = _passwordHasher.HashAndSaltPassword(Password);
                _userManager.CreateUser(new User()
                {
                    Username = Username,
                    Email = Email
                },
                hashedPassword, salt);
                createSuccessful = true;
            }
            catch
            {
                // Sign-up failed
                ModelState.AddModelError(string.Empty, "Sign-up failed.");
                return Page();
            }
            

            if (createSuccessful)
            {
                // Sign-up successful
                return RedirectToPage("SignIn");
            }
            else
            {
                // Sign-up failed
                ModelState.AddModelError(string.Empty, "Sign-up failed.");
                return Page();
            }

            // Redirect to the sign-in for now
            return RedirectToPage("SignIn");
        }
    }
}
