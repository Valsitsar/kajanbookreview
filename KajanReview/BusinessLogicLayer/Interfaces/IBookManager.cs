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
        public void UpdateBook(Book book);
        public void DeleteBook(int id);
        public Book GetBook(int id);
        public List<Book> GetAllBooks();
    }
}
