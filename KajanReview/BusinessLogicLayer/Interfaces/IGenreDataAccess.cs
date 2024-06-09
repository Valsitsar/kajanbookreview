using BusinessLogicLayer.Entities;

namespace BusinessLogicLayer.Interfaces
{
    public interface IGenreDataAccess
    {
        public Task CreateGenreAsync(Genre newGenre);
        public Task<Genre> GetGenreByIDAsync(int genreID);
        public Task<List<Genre>> GetAllGenresAsync();
        public Task UpdateGenreAsync(Genre genre);
        public Task DeleteGenreByIDAsync(int genreID);
    }
}
