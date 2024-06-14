using BusinessLogicLayer.ManagerClasses;
using BusinessLogicLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessLogicLayer;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Interfaces;

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

        private IUserManager _userManager;
        private PasswordHasher _passwordHasher;

        public SignUpModel(IUserManager userManager, IBookshelfManager bookshelfManager)
        {
            _userManager = userManager;
            _passwordHasher = new PasswordHasher();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid) { return Page(); }

            if (Password != ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Passwords do not match.");
                return Page();
            }

            bool createSuccessful = false;
            try
            {
                (string hashedPassword, string salt) = _passwordHasher.HashAndSaltPassword(Password);
                await _userManager.CreateUserAsync(new UserDTO()
                {
                    Username = Username,
                    Email = Email,
                    Role = new Role() { ID = 3, Name = "Reader" }
                },
                hashedPassword, salt);
                createSuccessful = true;

                // Create default bookshelves for the new user
                var newUserID = await _userManager.GetLastUserID();
                await _userManager.CreateDefaultBookshelvesForUserAsync(newUserID);

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
        }
    }
}
