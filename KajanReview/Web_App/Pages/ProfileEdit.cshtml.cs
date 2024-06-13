using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessLogicLayer.DTOs;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using BusinessLogicLayer.ManagerClasses;
using System.Security.Claims;
using BusinessLogicLayer;
using System.Diagnostics;

namespace Web_App.Pages
{
    [Authorize(Roles = "Reader, Author")]
    public class ProfileEditModel : PageModel
    {
        [BindProperty]
        public UserDTO CurrentUser { get; set; }

        [BindProperty]
        public string CurrentPassword { get; set; }

        [BindProperty]
        public string NewPassword { get; set; }

        [BindProperty]
        public string ConfirmPassword { get; set; }

        [BindProperty]
        public IFormFile ProfilePictureFile { get; set; }

        public string ChangePasswordError { get; set; }

        private readonly IUserManager _userManager;
        private readonly PasswordAuthenticator _passwordAuthenticator;
        private readonly IWebHostEnvironment _environment;

        public ProfileEditModel(IUserManager userManager, IWebHostEnvironment environment)
        {
            _userManager = userManager;
            _environment = environment;
            CurrentPassword = string.Empty;
            NewPassword = string.Empty;
            ConfirmPassword = string.Empty;
            ChangePasswordError = string.Empty;
        }

        public async Task OnGetAsync()
        {
            CurrentUser = await GetCurrentUserAsync();
        }

        public async Task<IActionResult> OnPostUpdateProfileAsync()
        {
            ModelState.Remove("ProfilePictureFile");
            ModelState.Remove("CurrentPassword");
            ModelState.Remove("NewPassword");
            ModelState.Remove("ConfirmPassword");

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                foreach (var error in errors)
                {
                    // Log the error for debugging purposes
                    Debug.WriteLine("\nError: " + error); // Replace with your logging mechanism
                }
                return Page();
            }

            if (ProfilePictureFile != null)
            {
                var filePath = Path.Combine(_environment.WebRootPath, "img", "profile-pics", $"profile-pic-{CurrentUser.Username}.png");

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ProfilePictureFile.CopyToAsync(stream);
                }

                CurrentUser.ProfilePictureFilePath = $"/img/profile-pics/profile-pic-{CurrentUser.Username}.png";
            }

            await _userManager.UpdateUserAsync(CurrentUser);

            return RedirectToPage("./ProfileEdit");
        }

        public async Task<IActionResult> OnPostChangePasswordAsync()
        {
            if (string.IsNullOrWhiteSpace(CurrentPassword) || string.IsNullOrWhiteSpace(NewPassword) || string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                ChangePasswordError = "All password fields are required.";
                return Page();
            }

            var (storedHashedPassword, storedSalt) = await _userManager.GetPasswordHashAndSaltByUserIDAsync(CurrentUser.ID);

            if (storedHashedPassword == null || storedSalt == null)
            {
                ChangePasswordError = "An error occurred. Please try again.";
                return Page();
            }

            if ( _passwordAuthenticator.IsPasswordHashValid(CurrentPassword, storedHashedPassword, storedSalt))
            {
                ChangePasswordError = "The current password is incorrect.";
                return Page();
            }

            if (NewPassword != ConfirmPassword)
            {
                ChangePasswordError = "The new passwords do not match.";
                return Page();
            }

            await _userManager.UpdatePasswordHashAndSaltByUserIDAsync(CurrentUser.ID, NewPassword, storedSalt);

            return RedirectToPage("./ProfileEdit");
        }

        private async Task<UserDTO> GetCurrentUserAsync()
        {
            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.GetUserByIDAsync(int.Parse(userID));

            return new UserDTO()
            {
                ID = user.ID,
                ProfilePictureFilePath = user.ProfilePictureFilePath != null ? user.ProfilePictureFilePath : "/img/default-profile-picture.png",
                FirstName = user.FirstName,
                MiddleNames = user.MiddleNames,
                LastName = user.LastName,
                Username = user.Username,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };
        }
    }
}
