using BusinessLogicLayer.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web_App.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public List<Book> Books { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            Books = new List<Book>
            {
                new Book
                {
                    CoverFilePath = "./images/Book_cover_unavailable.png",
                    Title = "Example book",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin tincidunt leo imperdiet ante venenatis, et tempus odio eleifend.",
                    PageCount = 1000,
                    ISBN = "1234567654321",
                    Format = new BookFormat { Name = "Hardcover" },
                    Publisher = "Example publisher",
                    PubDate = DateTime.Today,
                    Language = "English",
                    Genres = new List<Genre>
                    {
                        new Genre { Name = "Fantasy" },
                        new Genre { Name = "Romance" }
                    },
                    Authors = new List<User> { new User { FirstName = "Example", LastName = "Author" } }
                }
            };
        }
    }
}
