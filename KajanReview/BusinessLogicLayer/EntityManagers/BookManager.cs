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
            try { _bookDataAccess.CreateBook(book); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
        public Book GetBookByID(int bookID)
        {
            try { return _bookDataAccess.GetBookByID(bookID); }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public List<Book> GetAllBooks()
        {
            try { return _bookDataAccess.GetAllBooks(); }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.Message); 
                return [];
            }
        }
        public void UpdateBook(Book book)
        {
            try { _bookDataAccess.UpdateBook(book); } 
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        public void DeleteBook(int bookID)
        {
            try { _bookDataAccess.DeleteBookByID(bookID); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }


    }
}
