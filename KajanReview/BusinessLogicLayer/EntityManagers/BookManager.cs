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
        private IBookDataAccess _bookDataAccess;
        
        public BookManager(IBookDataAccess bookDataAccess)
        {
            _bookDataAccess = bookDataAccess;
        }
        public void CreateBook(Book book)
        {
            try
            {
                _bookDataAccess.CreateBook(book);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DeleteBook(int id)
        {
            throw new NotImplementedException();
        }

        public List<Book> GetAllBooks()
        {
            throw new NotImplementedException();
        }

        public Book GetBook(int id)
        {
            throw new NotImplementedException();
        }
        public void UpdateBook(Book book)
        {
            throw new NotImplementedException();
        }
    }
}
