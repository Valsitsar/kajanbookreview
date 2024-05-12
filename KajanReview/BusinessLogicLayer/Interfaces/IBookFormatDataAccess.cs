using BusinessLogicLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
