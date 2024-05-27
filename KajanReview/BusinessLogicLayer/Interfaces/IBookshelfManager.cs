using BusinessLogicLayer.Entities;

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
