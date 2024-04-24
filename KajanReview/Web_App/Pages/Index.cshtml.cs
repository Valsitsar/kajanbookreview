using BusinessLogicLayer.Entities;
using Microsoft.AspNetCore.Mvc;
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
            string loremIpsum = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean scelerisque ex odio, eget aliquet diam finibus id. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Maecenas commodo elementum turpis mollis lacinia. Nunc quis laoreet lorem. In risus eros, rhoncus nec nisi eu, rutrum dignissim dolor. Vivamus a leo orci. Sed auctor faucibus maximus. Proin semper justo ac tortor vulputate ultrices. Nunc pulvinar ex non malesuada aliquam. Pellentesque a lacus purus. Maecenas imperdiet ligula vel dictum laoreet. Aenean commodo vulputate eleifend. Curabitur ut urna erat. Maecenas aliquam ante id felis condimentum convallis. Nam mauris odio, sollicitudin sit amet neque non, placerat pulvinar nisl.";

            Books =
            [
                new Book("1", "Book 1", $"{loremIpsum}", 50, "1111111111", "Publisher 1", DateOnly.Parse("01 / 01 / 2001"), "English"),
                new Book("2", "Book 2", $"{loremIpsum}", 100, "2222222222", "Publisher 2", DateOnly.Parse("02 / 02 / 2002"), "Dutch"),
                new Book("3", "Book 3", $"{loremIpsum}", 150, "3333333333", "Publisher 3", DateOnly.Parse("03 / 03 / 2024"), "Slovak"),
                new Book("4", "Book 4", $"{loremIpsum}", 200, "4444444444", "Publisher 4", DateOnly.Parse("04 / 04 / 2024"), "French"),
                new Book("5", "Book 5", $"{loremIpsum}", 250, "5555555555", "Publisher 5", DateOnly.Parse("05 / 05 / 2024"), "German"),
            ];
        }
    }
}
