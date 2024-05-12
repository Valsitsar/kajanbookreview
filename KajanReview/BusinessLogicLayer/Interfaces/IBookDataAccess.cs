using BusinessLogicLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
