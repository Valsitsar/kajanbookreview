using BusinessLogicLayer.Entities;

namespace BusinessLogicLayer.Interfaces
{
    public interface IBookDataAccess
    {
        public void CreateBook(Book newBook);
        public Book GetBookByID(int bookID);
        public List<Book> GetAllBooks();
        public void UpdateBook(Book book);
        public void DeleteBookByID(int bookID);
    }
}
