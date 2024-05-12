using BusinessLogicLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IBookshelfManager
    {
        public void CreateBookshelf(Bookshelf newBookshelf);
        public Bookshelf GetBookshelfByID(int bookshelfID);
        public List<Bookshelf> GetAllBookshelvesForUser(int userID);
        public void UpdateBookshelf(Bookshelf bookshelf);
        public void DeleteBookshelfByID(int bookshelfID);
    }
}
