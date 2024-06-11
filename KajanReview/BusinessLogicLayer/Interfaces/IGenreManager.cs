using BusinessLogicLayer.Entities;

namespace BusinessLogicLayer.Interfaces
{
    public interface IGenreManager
    {
        public Task CreateGenreAsync(Genre newGenre);
        public Task<Genre> GetGenreByIDAsync(int genreID);
        public Task<List<Genre>> GetAllGenresAsync();
        public Task<List<Genre>> GetGenresForBookAsync(int bookID);
        public Task UpdateGenreAsync(Genre genre);
        public Task DeleteGenreByIDAsync(int genreID);
    }
}
