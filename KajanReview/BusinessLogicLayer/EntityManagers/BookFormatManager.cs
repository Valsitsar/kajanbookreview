using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;

namespace BusinessLogicLayer.EntityManagers
{
    public class BookFormatManager : IBookFormatManager
    {
        private IBookFormatDataAccess _bookFormatDataAccess;

        public BookFormatManager(IBookFormatDataAccess bookFormatDataAccess)
        {
            _bookFormatDataAccess = bookFormatDataAccess ?? throw new ArgumentNullException(nameof(_bookFormatDataAccess));
        }

        public void CreateBookFormat(BookFormat newBookFormat)
        {
            _bookFormatDataAccess.CreateBookFormat(newBookFormat);
        }

        public BookFormat GetBookFormatByID(int bookFormatID)
        {
            return _bookFormatDataAccess.GetBookFormatByID(bookFormatID);
        }

        public List<BookFormat> GetAllBookFormats()
        {
            return _bookFormatDataAccess.GetAllBookFormats();
        }

        public void UpdateBookFormat(BookFormat bookFormat)
        {
            _bookFormatDataAccess.UpdateBookFormat(bookFormat);
        }

        public void DeleteBookFormatByID(int bookFormatID)
        {
            _bookFormatDataAccess.DeleteBookFormatByID(bookFormatID);
        }
    }
}
