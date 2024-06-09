using BusinessLogicLayer.Entities;

namespace BusinessLogicLayer.Interfaces
{
    public interface IBookManager
    {
        public Task CreateBookAsync(Book newBook);
        public Task<Book> GetBookByIDAsync(int bookID);
        public Task<List<Book>> GetAllBooksAsync();
        public Task UpdateBookAsync(Book book);
        public Task DeleteBookAsync(int bookID);
    }
}
