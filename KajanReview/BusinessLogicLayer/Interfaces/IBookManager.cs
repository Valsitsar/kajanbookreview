using BusinessLogicLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IBookManager
    {
        public void CreateBook(Book newBook);
        public Book GetBook(int bookID);
        public List<Book> GetAllBooks();
        public void UpdateBook(Book book);
        public void DeleteBook(int bookID);
    }
}
