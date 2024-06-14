using BusinessLogicLayer.Entities;

namespace BusinessLogicLayer.Interfaces
{
    public interface IBookDataAccess
    {
        public Task CreateBookAsync(Book newBook);

        public Task<bool> TryAddBookToBookshelfAsync(int bookID, int bookshelfID);

        public Task<Book> GetBookByIDAsync(int bookID);
        public Task<List<Book>> GetAllBooksAsync();
        public Task<List<Book>> GetAllBooksWithDetailsAsync();

        public Task<int> GetTotalBooksCountAsync(string searchQuery);
        public Task<List<Book>> GetBooksByPageAsync(int pageNumber, int pageSize, string searchQuery);

        public Task<List<User>> GetAuthorsForBookAsync(int bookID);
        public Task<List<Genre>> GetGenresForBookAsync(int bookID);
        public Task<int> GetMaxPageCountAsync();
        public Task UpdateBookAsync(Book book);
        public Task UpdateAuthorsForBookAsync(int bookID, List<int> authorIDs);
        public Task UpdateGenresForBookAsync(int bookID, List<int> genreIDs);
        public Task DeleteBookByIDAsync(int bookID);
    }
}
