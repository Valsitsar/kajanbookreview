using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web_App.Pages
{
    public class SignInModel : PageModel
    {
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            // TODO: Implement sign in logic (authentication)
            // var result = _userService.SignIn(User);
            
            // For now, just redirect to the home page
            return RedirectToPage("Index");
        }
    }
}
