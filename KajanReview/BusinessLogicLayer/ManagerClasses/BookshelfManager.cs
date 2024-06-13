using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;

namespace BusinessLogicLayer.ManagerClasses
{
    public class BookshelfManager : IBookshelfManager
    {
        private readonly IBookshelfDataAccess _bookshelfDataAccess;

        public BookshelfManager(IBookshelfDataAccess bookshelfDataAccess)
        {
            _bookshelfDataAccess = bookshelfDataAccess ?? throw new ArgumentNullException(nameof(_bookshelfDataAccess));
        }

        public async Task CreateBookshelfAsync(Bookshelf newBookshelf)
        {
            await _bookshelfDataAccess.CreateBookshelfAsync(newBookshelf);
        }

        public async Task<Bookshelf> GetBookshelfByIDAsync(int bookshelfID)
        {
            return await _bookshelfDataAccess.GetBookshelfByIDAsync(bookshelfID);
        }

        public async Task<List<Bookshelf>> GetAllBookshelvesForUserAsync(int userID)
        {
            return await _bookshelfDataAccess.GetAllBookshelvesForUserAsync(userID);
        }

        public async Task<List<Book>> GetPagedBooksByBookshelfIDAsync(int bookshelfID, int pageNumber, int pageSize)
        {
            return await _bookshelfDataAccess.GetPagedBooksByBookshelfIDAsync(bookshelfID, pageNumber, pageSize);
        }

        public async Task<int> GetTotalBooksCountByBookshelfIDAsync(int bookshelfID)
        {
            return await _bookshelfDataAccess.GetTotalBooksCountByBookshelfIDAsync(bookshelfID);
        }

        public async Task<List<Book>> GetPagedBooksAcrossAllShelvesAsync(int userID, int pageNumber, int pageSize)
        {
            return await _bookshelfDataAccess.GetPagedBooksAcrossAllShelvesAsync(userID, pageNumber, pageSize);
        }

        public async Task<int> GetTotalBooksCountAcrossAllShelvesAsync(int userID)
        {
            return await _bookshelfDataAccess.GetTotalBooksCountAcrossAllShelvesAsync(userID);
        }

        public async Task<List<Book>> GetBooksByAuthorAsync(int userID)
        {
            return await _bookshelfDataAccess.GetBooksByAuthorAsync(userID);
        }

        public async Task UpdateBookshelfAsync(Bookshelf bookshelf)
        {
            await _bookshelfDataAccess.UpdateBookshelfAsync(bookshelf);
        }

        public async Task DeleteBookshelfByIDAsync(int bookshelfID)
        {
            await _bookshelfDataAccess.DeleteBookshelfByIDAsync(bookshelfID);
        }
    }
}