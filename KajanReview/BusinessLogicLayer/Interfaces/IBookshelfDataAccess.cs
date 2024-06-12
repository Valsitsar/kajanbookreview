using BusinessLogicLayer.Entities;

namespace BusinessLogicLayer.Interfaces
{
    public interface IBookshelfDataAccess
    {
        public Task CreateBookshelfAsync(Bookshelf newBookshelf);
        public Task<Bookshelf> GetBookshelfByIDAsync(int bookshelfID);
        public Task<List<Bookshelf>> GetAllBookshelvesForUserAsync(int userID);
        public Task<List<Book>> GetPagedBooksByBookshelfIDAsync(int bookshelfID, int pageNumber, int pageSize);
        public Task<int> GetTotalBooksCountByBookshelfIDAsync(int bookshelfID);
        public Task<List<Book>> GetPagedBooksAcrossAllShelvesAsync(int userID, int pageNumber, int pageSize);
        public Task<int> GetTotalBooksCountAcrossAllShelvesAsync(int userID);
        public Task<List<Book>> GetBooksByAuthorAsync(int userID);
        public Task UpdateBookshelfAsync(Bookshelf bookshelf);
        public Task DeleteBookshelfByIDAsync(int bookshelfID);
    }
}
