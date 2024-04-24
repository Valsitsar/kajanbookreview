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
        public Book ReadBooks();
        public Book ReadBook(int id);
        public void UpdateBook(Book newBook);
        public void DeleteBook(int id);
    }
}
