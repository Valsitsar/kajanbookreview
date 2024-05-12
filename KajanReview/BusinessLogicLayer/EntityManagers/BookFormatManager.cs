using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.EntityManagers
{
    public class BookFormatManager : IBookFormatManager
    {
        private IBookFormatDataAccess _bookFormatDataAccess;

        public BookFormatManager(IBookFormatDataAccess bookFormatDataAccess)
        {
            _bookFormatDataAccess = bookFormatDataAccess;
        }

        public void CreateBookFormat(BookFormat newBookFormat)
        {
            try { _bookFormatDataAccess.CreateBookFormat(newBookFormat); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        public BookFormat GetBookFormatByID(int bookFormatID)
        {
            try { return _bookFormatDataAccess.GetBookFormatByID(bookFormatID); }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<BookFormat> GetAllBookFormats()
        {
            try { return _bookFormatDataAccess.GetAllBookFormats(); }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return [];
            }
        }

        public void UpdateBookFormat(BookFormat bookFormat)
        {
            try { _bookFormatDataAccess.UpdateBookFormat(bookFormat); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        public void DeleteBookFormatByID(int bookFormatID)
        {
            try { _bookFormatDataAccess.DeleteBookFormatByID(bookFormatID); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
    }
}
