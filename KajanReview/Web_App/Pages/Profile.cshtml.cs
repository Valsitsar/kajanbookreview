using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.ManagerClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Web_App.Pages
{
    [Authorize(Roles = "Reader, Author")]
    public class ProfileModel : PageModel
    {
        [BindProperty]
        public UserDTO CurrentUser { get; set; }
        public List<int> bookshelfBookCounts { get; set; }

        private readonly IUserManager _userManager;
        private readonly IBookshelfManager _bookshelfManager;

        public ProfileModel(IUserManager userManager, IBookshelfManager bookshelfManager)
        {
            _userManager = userManager;
            _bookshelfManager = bookshelfManager;
        }

        public async Task OnGet()
        {
            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (int.TryParse(userID, out int parsedUserID))
            {
                var user = await _userManager.GetUserByIDAsync(parsedUserID);
                CurrentUser = user;
                CurrentUser.Reviews = await _userManager.GetReviewsByUserAsync(parsedUserID);
                (List<Bookshelf> bookshelves, List<int> bookCounts) = await _userManager.GetBookshelfNamesAndCountsForUserAsync(parsedUserID);
                CurrentUser.Bookshelves = bookshelves;
                bookshelfBookCounts = bookCounts;

                if (User.IsInRole("Author"))
                {
                    var authorBooks = await _bookshelfManager.GetBooksByAuthorAsync(parsedUserID);
                    Bookshelf authorBookshelf = new Bookshelf()
                    {
                        Name = "Written by Me",
                        Books = authorBooks
                    };
                    var authorBooksCount = authorBooks.Count;

                    bookshelves.Add(authorBookshelf);
                    bookCounts.Add(authorBooksCount);
                }
            }
        }
    }
}
