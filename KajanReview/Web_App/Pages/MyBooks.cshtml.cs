using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Web_App.Pages
{
    [Authorize(Roles = "Reader, Author")]
    // TODO: Make covers and titles clickable and open the book's details page
    public class MyBooksModel : PageModel
    {
        public List<Bookshelf> Bookshelves { get; set; }
        public string SelectedShelf { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public List<Book> PagedBooks { get; set; }
        public bool HasBooks => PagedBooks.Any();
        public bool IsAuthor => User.IsInRole("Author");

        private const int PageSize = 10;

        private readonly IBookshelfManager _bookshelfManager;

        public MyBooksModel(IBookshelfManager bookshelfManager)
        {
            _bookshelfManager = bookshelfManager;
        }

        public IActionResult OnGetLoadBooksForShelf(string shelfName)
        {
            var books = Bookshelves.First(shelf => shelf.Name == shelfName).Books;
            return new JsonResult(books);
        }

        public async Task OnGet(string shelf = "All", int pageNumber = 1)
        {
            var userID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            SelectedShelf = shelf;
            CurrentPage = pageNumber;

            // Populate the list if it's empty (e.g., when the page is first opened)
            if (Bookshelves == null || !Bookshelves.Any())
            { 
                await GetAllBookshelvesData(); 
            }


            if (SelectedShelf != "All")
            {
                var specificShelf = Bookshelves.FirstOrDefault(shelf => shelf.Name == SelectedShelf);
                if (specificShelf != null)
                {
                    PagedBooks = await _bookshelfManager.GetPagedBooksByBookshelfIDAsync(specificShelf.ID, CurrentPage, PageSize);
                    var totalBooksCount = await _bookshelfManager.GetTotalBooksCountByBookshelfIDAsync(specificShelf.ID);
                    TotalPages = (int)System.Math.Ceiling(totalBooksCount / (double)PageSize);
                }
                else
                {
                    PagedBooks = new List<Book>();
                    TotalPages = 0;
                }
            }
            else
            { 
                // Get all books from all bookshelves from the user
                PagedBooks = await _bookshelfManager.GetPagedBooksAcrossAllShelvesAsync(userID, CurrentPage, PageSize);
                var totalBooksCount = await _bookshelfManager.GetTotalBooksCountAcrossAllShelvesAsync(userID);
                TotalPages = (int)System.Math.Ceiling(totalBooksCount / (double)PageSize);
            }

            var allBooks = (SelectedShelf == "All")
               ? Bookshelves.SelectMany(shelf => shelf.Books).ToList()
               : Bookshelves.FirstOrDefault(shelf => shelf.Name == SelectedShelf)?.Books;

            if (allBooks != null)
            {
                TotalPages = (int)System.Math.Ceiling(allBooks.Count / (double)PageSize);
                PagedBooks = allBooks.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
            }
            else
            {
                TotalPages = 0;
                PagedBooks = new List<Book>();
            }
        }
        private async Task GetAllBookshelvesData()
        {
            Bookshelves = new List<Bookshelf>();
            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (int.TryParse(userID, out int parsedUserID))
            { 
                Bookshelves = await _bookshelfManager.GetAllBookshelvesForUserAsync(int.Parse(userID));

                if (IsAuthor)
                {
                    var authorShelf = new Bookshelf
                    {
                        Name = "Written by Me",
                        Books = await _bookshelfManager.GetBooksByAuthorAsync(parsedUserID)
                    };
                    Bookshelves.Add(authorShelf);
                }
            }
        }
    }
}
