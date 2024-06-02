using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;

namespace BusinessLogicLayer.ManagerClasses
{
    public class BookshelfManager : IBookshelfManager
    {
        private readonly IBookshelfDataAccess _bookshelfDataAccess;

        public BookshelfManager(IBookshelfDataAccess bookshelfDataAccess)
        {
            _bookshelfDataAccess = bookshelfDataAccess ?? throw new ArgumentNullException(nameof(_bookshelfDataAccess));
        }

        public void CreateBookshelf(Bookshelf newBookshelf)
        {
            _bookshelfDataAccess.CreateBookshelf(newBookshelf);
        }

        public Bookshelf GetBookshelfByID(int bookshelfID)
        {
            return _bookshelfDataAccess.GetBookshelfByID(bookshelfID);
        }

        public List<Bookshelf> GetAllBookshelvesForUser(int userID)
        {
            return _bookshelfDataAccess.GetAllBookshelvesForUser(userID);
        }

        public void UpdateBookshelf(Bookshelf bookshelf)
        {
            _bookshelfDataAccess.UpdateBookshelf(bookshelf);
        }

        public void DeleteBookshelfByID(int bookshelfID)
        {
            _bookshelfDataAccess.DeleteBookshelfByID(bookshelfID);
        }
    }
}