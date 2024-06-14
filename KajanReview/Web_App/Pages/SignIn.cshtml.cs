using BusinessLogicLayer;
using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.ManagerClasses;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Web_App.Pages
{
    public class SignInModel : PageModel
    {
        [BindProperty]
        public string UsernameOrEmail { get; set; }
        [BindProperty]
        public string Password { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ReturnUrl { get; set; }

        private readonly IUserManager _userManager;
        private readonly PasswordAuthenticator _passwordVerifier;

        public SignInModel(IUserManager userManager, PasswordAuthenticator passwordVerifier)
        {
            _userManager = userManager;
            _passwordVerifier = passwordVerifier;
        }

        public async Task OnGet()
        {
            ModelState.Remove("ReturnUrl");
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            ModelState.Remove("ReturnUrl");

            if (!ModelState.IsValid) { return Page(); }

            // TODO: Implement sign-in logic here (e.g. check if the user exists in the database)
            User user = UsernameOrEmail.Contains('@')
                ? await _userManager.GetUserByEmailForLoginAsync(UsernameOrEmail)
                : await _userManager.GetUserByUsernameForLoginAsync(UsernameOrEmail);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Username / Email not recognized.");
                return Page();
            }

            bool passwordIsValid = _passwordVerifier.IsPasswordHashValid(Password, user.PasswordHash, user.PasswordSalt);

            if (passwordIsValid)
            {
                // Login successful
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.Name)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity));

                // Check if returnUrl is not null, not empty, and local
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToPage("Index");
                }
            }
            else
            {
                // Login failed
                ModelState.AddModelError(string.Empty, "Incorrect password.");
                return Page();
            }
        }
    }
}
