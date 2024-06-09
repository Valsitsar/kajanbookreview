using BusinessLogicLayer.Entities;

namespace BusinessLogicLayer.Interfaces
{
    public interface IBookshelfDataAccess
    {
        public Task CreateBookshelfAsync(Bookshelf newBookshelf);
        public Task<Bookshelf> GetBookshelfByIDAsync(int bookshelfID);
        public Task<List<Bookshelf>> GetAllBookshelvesForUserAsync(int userID);
        public Task UpdateBookshelfAsync(Bookshelf bookshelf);
        public Task DeleteBookshelfByIDAsync(int bookshelfID);
    }
}
