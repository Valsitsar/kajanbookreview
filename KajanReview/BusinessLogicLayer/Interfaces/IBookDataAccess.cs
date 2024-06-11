using BusinessLogicLayer.Entities;

namespace BusinessLogicLayer.Interfaces
{
    public interface IBookDataAccess
    {
        public Task CreateBookAsync(Book newBook);
        public Task<Book> GetBookByIDAsync(int bookID);
        public Task<List<Book>> GetAllBooksAsync();
        public Task<List<User>> GetAuthorsForBookAsync(int bookID);
        public Task<int> GetMaxPageCountAsync();
        public Task UpdateBookAsync(Book book);
        public Task DeleteBookByIDAsync(int bookID);
    }
}
