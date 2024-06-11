using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;

namespace BusinessLogicLayer.ManagerClasses
{
    public class BookManager : IBookManager
    {
        private readonly IBookDataAccess _bookDataAccess;

        public BookManager(IBookDataAccess bookDataAccess)
        {
            _bookDataAccess = bookDataAccess ?? throw new ArgumentNullException(nameof(bookDataAccess));
        }

        public async Task CreateBookAsync(Book book)
        {
            await _bookDataAccess.CreateBookAsync(book);
        }
        public async Task<Book> GetBookByIDAsync(int bookID)
        {
            return await _bookDataAccess.GetBookByIDAsync(bookID);
        }
        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await _bookDataAccess.GetAllBooksAsync();
        }
        public async Task<List<User>> GetAuthorsForBookAsync(int bookID)
        {
            return await _bookDataAccess.GetAuthorsForBookAsync(bookID);
        }
        public async Task<int> GetMaxPageCountAsync()
        {
            return await _bookDataAccess.GetMaxPageCountAsync();
        }
        public async Task UpdateBookAsync(Book book)
        {
            await _bookDataAccess.UpdateBookAsync(book);
        }

        public async Task DeleteBookAsync(int bookID)
        {
            await _bookDataAccess.DeleteBookByIDAsync(bookID);
        }
    }
}
