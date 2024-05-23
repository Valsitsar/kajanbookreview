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
                    Format = new BookFormat
                    {
                        Name = "Hardcover"
                    },
                    Publisher = "Example publisher",
                    PubDate = DateTime.Today,
                    Language = "English",
                    Genres = new List<Genre>
                    {
                        new Genre { Name = "Fantasy" },
                        new Genre { Name = "Romance" }
                    },
                    Authors = new List<User>
                    {
                        new User { FirstName = "Example", LastName = "Author" }
                    }
                },
                new Book
                {
                    CoverFilePath = "./images/Book_cover_unavailable.png",
                    Title = "Example book 2",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin tincidunt leo imperdiet ante venenatis, et tempus odio eleifend. Vivamus a magna arcu. In nisl ipsum, cursus sit amet posuere eu, bibendum eget mauris. Pellentesque semper semper orci, ut vehicula enim ultrices in. Duis tempor vulputate ante vel viverra. Sed fringilla dolor eget neque interdum, laoreet facilisis massa condimentum. Etiam ultrices sem non nunc gravida aliquet. Nunc eu ligula ut libero pulvinar porttitor a vitae nunc. Maecenas et nulla sem. Fusce ac risus ac justo dictum luctus id eu augue.",
                    PageCount = 500,
                    ISBN = "1234567654321",
                    Format = new BookFormat
                    {
                        Name = "Paperback"
                    },
                    Publisher = "Example publisher 2",
                    PubDate = DateTime.Today,
                    Language = "French",
                    Genres = new List<Genre>
                    {
                        new Genre { Name = "Mystery" },
                        new Genre { Name = "Thriller" },
                        new Genre { Name = "Dark Fantasy" }
                    },
                    Authors = new List<User>
                    {
                        new User { FirstName = "FirstName", LastName = "Surname" }
                    }
                }
            };
        }
    }
}
