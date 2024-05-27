using BusinessLogicLayer.Entities;

namespace BusinessLogicLayer.Interfaces
{
    public interface IBookFormatDataAccess
    {
        public void CreateBookFormat(BookFormat newBookFormat);
        public BookFormat GetBookFormatByID(int bookFormatId);
        public List<BookFormat> GetAllBookFormats();
        public void UpdateBookFormat(BookFormat bookFormat);
        public void DeleteBookFormatByID(int bookFormatId);
    }
}
