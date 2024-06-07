using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessLogicLayer.DTOs;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Web_App.Pages
{
    public class ProfileEditModel : PageModel
    {
        [BindProperty]
        public UserDTO CurrentUser { get; set; }

        [BindNever]
        public string CurrentPassword { get; set; }

        [BindProperty]
        public string NewPassword { get; set; }

        [BindProperty]
        public string ConfirmPassword { get; set; }

        [BindProperty]
        public IFormFile ProfilePictureFile { get; set; }

        public string ChangePasswordError { get; set; }

        public void OnGet()
        {
            //// Retrieve the CurrentUser object from session state
            //CurrentUser = HttpContext.Session.Get<UserDTO>("CurrentUser");

            if (CurrentUser == null)
            {
                CurrentUser = new UserDTO()
                {
                    ProfilePictureFilePath = "~/img/default-profile-picture.png",
                    FirstName = "John",
                    MiddleNames = "Jacob",
                    LastName = "Doe",
                    Username = "johndoe",
                    Email = "john.doe@gmail.com",
                    PhoneNumber = "123-456-7890",
                    Password = "password123",
                };

                //// Store the CurrentUser object in session state
                //HttpContext.Session.Set("CurrentUser", CurrentUser);
            }

            NewPassword = string.Empty;
            ConfirmPassword = string.Empty;
        }

        // Async because it involves a file upload operation
        // Avoids blocking the server thread while waiting for the file to upload
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Handle file upload
            if (ProfilePictureFile != null)
            {
                var filePath = Path.Combine("~/img", ProfilePictureFile.FileName);

                // Ensure the directory exists
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                // Save the uploaded file to the server
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ProfilePictureFile.CopyToAsync(stream);
                }

                // Update the user's profile picture file path
                CurrentUser.ProfilePictureFilePath = $"~/img/{ProfilePictureFile.FileName}";
            }

            // Save the user data to the DB here
            // For demo purposes, we'll just display the same page

            return RedirectToPage("./ProfileEdit");
        }

        public IActionResult OnPostChangePassword()
        {
            // Clear validation state for properties not involved in this form submission
            ModelState.ClearValidationState(nameof(CurrentUser));
            ModelState.ClearValidationState(nameof(ProfilePictureFile));

            // Manually validate the password fields
            if (!PasswordFieldsAreValid(out string? error))
            {
                return new JsonResult(new { success = false, error });
            }

            // Update the user's password
            CurrentUser.Password = NewPassword;



            // Save changes to the database

            // Return a JSON response
            return new JsonResult(new { success = true });
        }

        public bool PasswordFieldsAreValid(out string? error)
        {
            // Handle password change logic here
            if (string.IsNullOrWhiteSpace(CurrentPassword) ||
                string.IsNullOrWhiteSpace(NewPassword) ||
                string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                error = "All password fields are required.";
                return false;
            }

            if (CurrentUser.Password != CurrentPassword)
            {
                error = "The current password is incorrect.";
                return false;
            }

            if (NewPassword != ConfirmPassword)
            {
                error = "The new passwords do not match.";
                return false;
            }

            error = null;
            return true;
        }
    }
}
