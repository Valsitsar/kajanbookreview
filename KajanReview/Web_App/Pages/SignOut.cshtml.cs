using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web_App.Pages
{
    public class SignOutModel : PageModel
    {
        public async Task OnGet()
        {
            await HttpContext.SignOutAsync();
            Response.Redirect("/Index");
        }
    }
}
