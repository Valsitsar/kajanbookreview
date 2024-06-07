using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web_App.Pages
{
    public class ProfileModel : PageModel
    {
        [BindProperty]
        public User CurrentUser { get; set; }

        public void OnGet()
        {
            // TODO: Implement DB connection to retreive user data

            // Example data
            Book theGreatGatsby = new Book()
            {
                Title = "The Great Gatsby",
                Authors = new List<User>()
                {
                    new User()
                    {
                        FirstName = "Francis",
                        MiddleNames = "Scott",
                        LastName = "Fitzgerald"
                    }
                },
                Genres = new List<Genre>()
                {
                    new Genre() { Name = "Fiction" },
                    new Genre() { Name = "Classic" },
                    new Genre() { Name = "Romance" }

                },
                Description = "The Great Gatsby is a novel by American writer F. Scott Fitzgerald. Set in the Jazz Age on Long Island, near New York City, the novel depicts first-person narrator Nick Carraway's interactions with mysterious millionaire Jay Gatsby and Gatsby's obsession to reunite with his former lover, Daisy Buchanan.",
                PageCount = 180,
                ISBN = "978-0-7432-7356-5",
                Format = new BookFormat() { Name = "Paperback" },
                Publisher = "Scribner",
                PubDate = new DateTime(1925, 4, 10),
                Language = "English",

            };
            Book theCatcherInTheRye = new Book()
            {
                Title = "The Catcher in the Rye",
                Authors = new List<User>()
                {
                    new User()
                    {
                        FirstName = "Jerome",
                        MiddleNames = "David",
                        LastName = "Salinger"
                    },
                },
                Genres = new List<Genre>()
                {
                    new Genre() { Name = "Fiction" },
                    new Genre() { Name = "Classic" },
                    new Genre() { Name = "Coming-of-age" }
                },
                Description = "The Catcher in the Rye is a novel by J. D. Salinger, partially published in serial form in 1945–1946 and as a novel in 1951. It was originally intended for adults, but is often read by adolescents for its themes of angst and alienation, and as a critique on superficiality in society.",
                PageCount = 234,
                ISBN = "978-0-316-76948-0",
                Format = new BookFormat() { Name = "Hardcover" },
                Publisher = "Little, Brown and Company",
                PubDate = new DateTime(1951, 7, 16),
                Language = "English",
            };
            Book theBookOfThePast = new Book()
            {
                Title = "The Book of the Past",
                Authors = new List<User>()
                    {
                        new User()
                        {
                            FirstName = "Past",
                            LastName = "Reader"
                        },
                    },
                Genres = new List<Genre>()
                    {
                        new Genre() { Name = "Non-fiction" },
                        new Genre() { Name = "Self-help" },
                        new Genre() { Name = "Motivational" }
                    },
                Description = "The Book of the Past is a book about finishing books. It is a self-help book that aims to motivate readers to finish reading books.",
                PageCount = 300,
                ISBN = "978-0-000-00000-1",
                Format = new BookFormat() { Name = "E-book" },
                Publisher = "Reading Books Inc.",
                PubDate = new DateTime(2021, 1, 1),
                Language = "English",
            };
            CurrentUser = new User()
            {
                ProfilePictureFilePath = "~/img/default-profile-picture.png",
                FirstName = "John",
                LastName = "Doe",
                Username = "johndoe",
                Email = "john.doe@gmail.com",
                PhoneNumber = "123-456-7890",
                Password = "password123",
                Bookshelves = new List<Bookshelf>
                {
                    new Bookshelf()
                    {
                        Name = "Want To Read",
                        Books = new List<Book>()
                        {
                            theGreatGatsby,
                            theCatcherInTheRye
                        }
                    },
                    new Bookshelf()
                    {
                        Name = "Reading",
                        Books = new List<Book>()
                        {
                            new Book()  
                            {
                                Title = "The Book of Reading",
                                Authors = new List<User>()
                                {
                                    new User()
                                    {
                                        FirstName = "Present",
                                        LastName = "Reader"
                                    },
                                },
                                Genres = new List<Genre>()
                                {
                                    new Genre() { Name = "Non-fiction" },
                                    new Genre() { Name = "Self-help" },
                                    new Genre() { Name = "Motivational" }
                                },
                                Description = "The Book of Reading is a book about reading books. It is a self-help book that aims to motivate readers to read more books.",
                                PageCount = 300,
                                ISBN = "978-0-000-00000-0",
                                Format = new BookFormat() { Name = "E-book" },
                                Publisher = "Reading Books Inc.",
                                PubDate = new DateTime(2021, 1, 1),
                                Language = "English",
                            }
                        }
                    },
                    new Bookshelf()
                    {
                        Name = "Read",
                        Books = new List<Book>()
                        {
                            theBookOfThePast
                        }
                    },
                    new Bookshelf()
                    {
                        Name = "Favorites",
                        Books = new List<Book>()
                        {
                            new Book()
                            {
                                Title = "The Best Book",
                                Authors = new List<User>()
                                {
                                    new User()
                                    {
                                        FirstName = "Favorite",
                                        LastName = "Author"
                                    },
                                },
                                Genres = new List<Genre>()
                                {
                                    new Genre() { Name = "Non-fiction" },
                                    new Genre() { Name = "Self-help" },
                                    new Genre() { Name = "Motivational" }
                                },
                                Description = "The Best Book is a book about the best book. It is a self-help book that aims to motivate readers to read the best book.",
                                PageCount = 300,
                                ISBN = "978-0-000-00000-2",
                                Format = new BookFormat() { Name = "E-book" },
                                Publisher = "Reading Books Inc.",
                                PubDate = new DateTime(2021, 1, 1),
                                Language = "English",
                            }
                        }
                    }
                },
                Posts = new List<IUserPost>()
                {
                    new Review()
                    {
                        Title = "This is a review of the Book of the Past.",
                        Body = "This book is amazing! I highly recommend it to everyone.",
                        BookRating = 5,
                        PostDate = new DateTime(2021, 1, 1),
                        Poster = CurrentUser,
                        SourceBook = theBookOfThePast,
                        UpvoteCount = 420,
                        DownvoteCount = 69
                    },
                    new Review() { BookRating = 4 },
                    new Review() { BookRating = 3 },
                    new Review() { BookRating = 1 },
                    new Review() { BookRating = 5 }
                },
            };
        }
    }
}
