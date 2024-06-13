using BusinessLogicLayer.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web_App.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        // Properties for pagination
        public List<Book> Books { get; set; }
        public int TotalBooks { get; set; }
        public int PageSize { get; set; } = 10; // Default page size
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }


        public IndexModel(IBookManager bookManager)
        {
            _bookManager = bookManager;
        }

        public async Task OnGet(int? pageNumber)
        {
            // Take the page number from the query string or set to 1
            CurrentPage = pageNumber ?? 1;

            // Get the total number of books
            TotalBooks = await _bookManager.GetTotalBooksCountAsync();

            // Calculate the total number of pages
            TotalPages = (int)Math.Ceiling(TotalBooks / (double)PageSize);

            // Get the paginated list of books
            Books = await _bookManager.GetBooksByPageAsync(CurrentPage, PageSize);
        }
    }
}
