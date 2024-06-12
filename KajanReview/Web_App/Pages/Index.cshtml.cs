using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web_App.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IBookManager _bookManager;

        public List<Book> Books { get; set; }


        public IndexModel(IBookManager bookManager)
        {
            _bookManager = bookManager;
        }

        public async Task OnGet()
        {
            
        }
    }
}
