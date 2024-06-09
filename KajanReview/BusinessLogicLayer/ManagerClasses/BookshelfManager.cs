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

        public async Task CreateBookshelfAsync(Bookshelf newBookshelf)
        {
            await _bookshelfDataAccess.CreateBookshelfAsync(newBookshelf);
        }

        public async Task<Bookshelf> GetBookshelfByIDAsync(int bookshelfID)
        {
            return await _bookshelfDataAccess.GetBookshelfByIDAsync(bookshelfID);
        }

        public async Task<List<Bookshelf>> GetAllBookshelvesForUserAsync(int userID)
        {
            return await _bookshelfDataAccess.GetAllBookshelvesForUserAsync(userID);
        }

        public async Task UpdateBookshelfAsync(Bookshelf bookshelf)
        {
            await _bookshelfDataAccess.UpdateBookshelfAsync(bookshelf);
        }

        public async Task DeleteBookshelfByIDAsync(int bookshelfID)
        {
            await _bookshelfDataAccess.DeleteBookshelfByIDAsync(bookshelfID);
        }
    }
}