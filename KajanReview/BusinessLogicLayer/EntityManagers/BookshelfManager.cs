using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.EntityManagers
{
    public class BookshelfManager : IBookshelfManager
    {
        private IBookshelfDataAccess _bookshelfDataAccess;

        public BookshelfManager(IBookshelfDataAccess bookshelfDataAccess)
        {
            _bookshelfDataAccess = bookshelfDataAccess;
        }

        public void CreateBookshelf(Bookshelf newBookshelf)
        {
            try { _bookshelfDataAccess.CreateBookshelf(newBookshelf); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        public Bookshelf GetBookshelfByID(int bookshelfID)
        {
            try { return _bookshelfDataAccess.GetBookshelfByID(bookshelfID); }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.Message); 
                return null;
            }
        }

        public List<Bookshelf> GetAllBookshelvesForUser(int userID)
        {
            try { return _bookshelfDataAccess.GetAllBookshelvesForUser(userID); }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.Message);
                return [];
            }
        }

        public void UpdateBookshelf(Bookshelf bookshelf)
        {
            try { _bookshelfDataAccess.UpdateBookshelf(bookshelf); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        public void DeleteBookshelfByID(int bookshelfID)
        {
            try { _bookshelfDataAccess.DeleteBookshelfByID(bookshelfID); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
    }
}