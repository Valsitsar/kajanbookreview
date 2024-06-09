using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;

namespace BusinessLogicLayer.ManagerClasses
{
    public class BookFormatManager : IBookFormatManager
    {
        private readonly IBookFormatDataAccess _bookFormatDataAccess;

        public BookFormatManager(IBookFormatDataAccess bookFormatDataAccess)
        {
            _bookFormatDataAccess = bookFormatDataAccess ?? throw new ArgumentNullException(nameof(_bookFormatDataAccess));
        }

        public async Task CreateBookFormatAsync(BookFormat newBookFormat)
        {
            await _bookFormatDataAccess.CreateBookFormatAsync(newBookFormat);
        }

        public async Task<BookFormat> GetBookFormatByIDAsync(int bookFormatID)
        {
            return await _bookFormatDataAccess.GetBookFormatByIDAsync(bookFormatID);
        }

        public async Task<List<BookFormat>> GetAllBookFormatsAsync()
        {
            return await _bookFormatDataAccess.GetAllBookFormatsAsync();
        }

        public async Task UpdateBookFormatAsync(BookFormat bookFormat)
        {
            await _bookFormatDataAccess.UpdateBookFormatAsync(bookFormat);
        }

        public async Task DeleteBookFormatByIDAsync(int bookFormatID)
        {
            await _bookFormatDataAccess.DeleteBookFormatByIDAsync(bookFormatID);
        }
    }
}
