using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.RecommendationAlgorithm;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Web_App.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IBookManager _bookManager;
        private readonly RecommendationEngine _recommendationEngine;

        // Properties for pagination
        public List<Book> Books { get; set; }
        public int TotalBooks { get; set; }
        public int PageSize { get; set; } = 5; // Default page size
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }

        // Properties for recommendations
        public List<Book> RecommendedBooks { get; set; }


        [BindProperty(SupportsGet = true)]
        public string SearchQuery { get; set; }


        public IndexModel(IBookManager bookManager, RecommendationEngine recommendationEngine)
        {
            _bookManager = bookManager;
            _recommendationEngine = recommendationEngine;
        }

        public async Task OnGet(int? pageNumber, string searchQuery)
        {
            // Trim and validate the search query
            SearchQuery = searchQuery?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                SearchQuery = "";
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

            if (User.Identity.IsAuthenticated)
            {
                // Get the recommended books
                int userID = int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int parsedUserID) ? parsedUserID : 0;
                RecommendedBooks = await _recommendationEngine.GetRecommendationsForUserAsync(userID);
            }
        }
    }
}
