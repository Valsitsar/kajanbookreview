using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web_App.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IBookManager _bookManager;

        // Properties for pagination
        public List<Book> Books { get; set; }
        public int TotalBooks { get; set; }
        public int PageSize { get; set; } = 5; // Default page size
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchQuery { get; set; }


        public IndexModel(IBookManager bookManager)
        {
            _bookManager = bookManager;
        }

        public async Task OnGet(int? pageNumber, string searchQuery)
        {
            // Trim and validate the search query
            searchQuery = searchQuery?.Trim();
            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                SearchQuery = null;
            }
            else
            {
                // Case-insensitive matching
                SearchQuery = SearchQuery.ToLower();
            }

            // Take the page number from the query string or set to 1
            CurrentPage = pageNumber ?? 1;

            // Get the total number of books
            TotalBooks = await _bookManager.GetTotalBooksCountAsync(SearchQuery);

            // Calculate the total number of pages
            TotalPages = (int)Math.Ceiling(TotalBooks / (double)PageSize);

            // Get the paginated list of books
            Books = await _bookManager.GetBooksByPageAsync(CurrentPage, PageSize, SearchQuery);
        }
    }
}
