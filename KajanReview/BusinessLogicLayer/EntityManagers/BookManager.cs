using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.EntityManagers
{
    public class BookManager : IBookManager
    {
        private readonly IBookDataAccess _bookDataAccess;

        public BookManager(IBookDataAccess bookDataAccess)
        {
            _bookDataAccess = bookDataAccess ?? throw new ArgumentNullException(nameof(bookDataAccess));
        }

        public void CreateBook(Book book)
        {
            _bookDataAccess.CreateBook(book);
        }
        public Book GetBookByID(int bookID)
        {
            return _bookDataAccess.GetBookByID(bookID);
        }
        public List<Book> GetAllBooks()
        {
            return _bookDataAccess.GetAllBooks();
        }
        public void UpdateBook(Book book)
        {
            _bookDataAccess.UpdateBook(book);
        }

        public void DeleteBook(int bookID)
        {
            _bookDataAccess.DeleteBookByID(bookID);
        }
    }
}
