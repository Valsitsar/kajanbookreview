using BusinessLogicLayer.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web_App.Pages
{
    public class BookDetailsModel : PageModel
    {
        //[BindProperty(SupportsGet = true)]
        //public Book CurrentBook { get; set; }
        public Book CurrentBook { get; private set; }
        public void OnGet()
        {
            // TODO: implement DB
            // Hard-coded values for now
            CurrentBook = new Book()
            {
                CoverFilePath = "./images/Book_cover_unavailable.png",
                Title = "Example book",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin tincidunt leo imperdiet ante venenatis, et tempus odio eleifend. Vivamus a magna arcu. In nisl ipsum, cursus sit amet posuere eu, bibendum eget mauris. Pellentesque semper semper orci, ut vehicula enim ultrices in. Duis tempor vulputate ante vel viverra. Sed fringilla dolor eget neque interdum, laoreet facilisis massa condimentum. Etiam ultrices sem non nunc gravida aliquet. Nunc eu ligula ut libero pulvinar porttitor a vitae nunc. Maecenas et nulla sem. Fusce ac risus ac justo dictum luctus id eu augue.",
                PageCount = 1000,
                ISBN = "1234567654321",
                Format = new BookFormat() { Name = "Hardcover" },
                Publisher = "Example publisher",
                PubDate = DateTime.Today,
                Language = "English",
                Genres = new List<Genre>
                {
                    new Genre() { Name = "Fantasy" }, new Genre() { Name = "Romance" }
                },
                Authors = new List<User> { new User() { FirstName = "Example", LastName = "Author" } },
                Reviews = new List<Review>
                {
                    new Review()
                    {
                        Poster = new User() { Username = "ReviewPoster1" },
                        Title = "I loved it!",
                        Body = "This book is amazing!",
                        BookRating = 5,
                        PostDate = DateTime.Today.AddDays(-4),
                        UpvoteCount = 69,
                    },
                    new Review()
                    {
                        Poster = new User() { Username = "ReviewPoster2" },
                        Title = "A mediocre experience.",
                        Body = "This book is okay.",
                        BookRating = 3,
                        PostDate = DateTime.Today.AddDays(-3),
                        UpvoteCount = 420,
                    },
                    new Review()
                    {
                        Poster = new User() { Username = "ReviewPoster3" },
                        Title = "Do not recommend to anyone. Stay away!",
                        Body = "This book is terrible.",
                        BookRating = 1,
                        PostDate = DateTime.Today.AddDays(-2),
                        DownvoteCount = 666,
                    },
                    new Review()
                    {
                        Poster = new User() { Username = "ReviewPoster4" },
                        Title = "This book changed my life - here's how.",
                        Body = "This book is the best book ever!",
                        BookRating = 5,
                        PostDate = DateTime.Today.AddDays(-1)
                    },
                    new Review()
                    { BookRating = 2 },
                    new Review()
                    { BookRating = 4 }
                }
            };
        }
    }
}
