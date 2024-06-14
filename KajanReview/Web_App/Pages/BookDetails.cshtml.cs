using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Web_App.Pages
{
    public class BookDetailsModel : PageModel
    {
        private readonly IBookManager _bookManager;
        private readonly IReviewManager _reviewManager;
        private readonly IUserManager _userManager;
        private readonly IBookshelfManager _bookshelfManager;
        private readonly ILogger<BookDetailsModel> _logger;

        public BookDetailsModel(IBookManager bookManager, IReviewManager reviewManager,  ILogger<BookDetailsModel> logger, IUserManager userManager, IBookshelfManager bookshelfManager)
        {
            _bookManager = bookManager;
            _reviewManager = reviewManager;
            _logger = logger;
            _userManager = userManager;
            _bookshelfManager = bookshelfManager;
        }

        public Book? CurrentBook { get; set; }

        [BindProperty]
        public int CurrentBookID { get; set; }

        [BindProperty]
        public int BookshelfID { get; set; }

        [BindProperty]
        public string? ReviewTitle { get; set; }

        [BindProperty]
        public string? ReviewBody { get; set; }

        [BindProperty]
        public int? UserRating { get; set; }

        public List<Bookshelf> UserBookshelves { get; set; }

        public async Task OnGet(int id)
        {
            try
            {
                CurrentBook = await _bookManager.GetBookByIDAsync(id);
                if (CurrentBook == null)
                {
                    // Log a warning if the book is not found
                    TempData["ErrorMessage"] = "Book not found.";
                    _logger.LogWarning($"Book with ID {id} not found.");
                }
                else
                {
                   CurrentBookID = CurrentBook.ID;
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while retrieving the book.";
                _logger.LogError(ex, $"An error occurred while retrieving book with ID {id}.");
            }
            try
            {
                UserBookshelves = await _userManager.GetBookshelvesForUserAsync(int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0"));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while retrieving the user's bookshelves.";
                _logger.LogError(ex, "An error occurred while retrieving the user's bookshelves.");
            }
        }

        public async Task<IActionResult> OnPostCreateReviewAsync()
        {
            try
            {
                // Get the current user
                var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var parsedUserID = int.Parse(userID ?? "0");

                var username = User.Identity?.Name ?? string.Empty;

                CurrentBook = await _bookManager.GetBookByIDAsync(CurrentBookID);

                // Create a new review
                var review = new Review
                {
                    Title = ReviewTitle,
                    Body = ReviewBody,
                    BookRating = UserRating.Value,
                    PostDate = DateTime.Now,
                    SourceBook = CurrentBook,
                    Poster = new User() { ID = parsedUserID, Username = username }
                };

                // Add the review to the database
                await _reviewManager.CreateReviewAsync(review);

                // Redirect to the book details page
                return RedirectToPage("/BookDetails", new { id = CurrentBookID });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while creating the review.";
                _logger.LogError(ex, "An error occurred while creating a review.");
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAddToBookshelfAsync(int bookshelfID)
        {
            try
            {
                // Get the current user
                var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var parsedUserID = int.Parse(userID ?? "0");

                var username = User.Identity?.Name ?? string.Empty;

                CurrentBook = await _bookManager.GetBookByIDAsync(CurrentBookID);

                if (!User.Identity.IsAuthenticated)
                {
                    TempData["ErrorMessage"] = "You must be logged in to add a book to a bookshelf.";
                    return RedirectToPage("/SignIn", new { id = CurrentBookID});
                }

                // Add the book to the bookshelf
                var wasAdded = await _bookManager.TryAddBookToBookshelfAsync(CurrentBookID, bookshelfID);

                if (wasAdded)
                {
                    TempData["SuccessMessage"] = "Book added to bookshelf.";
                }
                else
                {
                    TempData["ErrorMessage"] = "An error occurred while adding the book to a bookshelf.";
                }
                return RedirectToPage("/BookDetails", new { id = CurrentBookID });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while adding the book to a bookshelf.";
                _logger.LogError(ex, "An error occurred while adding the book to a bookshelf.");
                return RedirectToPage("/BookDetails", new { id = CurrentBookID });
            }
        }
    }
}
