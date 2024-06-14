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

        public async Task<bool> TryAddBookToBookshelfAsync(int bookID, int bookshelfID)
        {
            return await _bookDataAccess.TryAddBookToBookshelfAsync(bookID, bookshelfID);
        }

        public async Task<Book> GetBookByIDAsync(int bookID)
        {
            return await _bookDataAccess.GetBookByIDAsync(bookID);
        }
        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await _bookDataAccess.GetAllBooksAsync();
        }

        public async Task<List<Book>> GetAllBooksWithDetailsAsync()
        {
            return await _bookDataAccess.GetAllBooksWithDetailsAsync();
        }

        public async Task<int> GetTotalBooksCountAsync(string searchQuery)
        {
            return await _bookDataAccess.GetTotalBooksCountAsync(searchQuery);
        }

        public async Task<List<Book>> GetBooksByPageAsync(int pageNumber, int pageSize, string searchQuery)
        {
            return await _bookDataAccess.GetBooksByPageAsync(pageNumber, pageSize, searchQuery);
        }

        public async Task<List<User>> GetAuthorsForBookAsync(int bookID)
        {
            return await _bookDataAccess.GetAuthorsForBookAsync(bookID);
        }

        public async Task<List<Genre>> GetGenresForBookAsync(int bookID)
        {
            return await _bookDataAccess.GetGenresForBookAsync(bookID);
        }
        public async Task<int> GetMaxPageCountAsync()
        {
            return await _bookDataAccess.GetMaxPageCountAsync();
        }
        public async Task UpdateBookAsync(Book book)
        {
            await _bookDataAccess.UpdateBookAsync(book);
        }

        public async Task UpdateAuthorsForBookAsync(int bookID, List<int> authorIDs)
        {
            await _bookDataAccess.UpdateAuthorsForBookAsync(bookID, authorIDs);
        }

        public async Task UpdateGenresForBookAsync(int bookID, List<int> genreIDs)
        {
            await _bookDataAccess.UpdateGenresForBookAsync(bookID, genreIDs);
        }

        public async Task DeleteBookAsync(int bookID)
        {
            await _bookDataAccess.DeleteBookByIDAsync(bookID);
        }
    }
}
