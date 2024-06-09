using BusinessLogicLayer.Entities;

namespace BusinessLogicLayer.Interfaces
{
    public interface IBookFormatDataAccess
    {
        public Task CreateBookFormatAsync(BookFormat newBookFormat);
        public Task<BookFormat> GetBookFormatByIDAsync(int bookFormatId);
        public Task<List<BookFormat>> GetAllBookFormatsAsync();
        public Task UpdateBookFormatAsync(BookFormat bookFormat);
        public Task DeleteBookFormatByIDAsync(int bookFormatId);
    }
}
