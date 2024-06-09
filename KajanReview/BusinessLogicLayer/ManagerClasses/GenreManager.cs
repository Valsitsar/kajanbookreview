using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;

namespace BusinessLogicLayer.ManagerClasses
{
    public class GenreManager : IGenreManager
    {
        private readonly IGenreDataAccess _genreDataAccess;

        public GenreManager(IGenreDataAccess genreDataAccess)
        {
            _genreDataAccess = genreDataAccess ?? throw new ArgumentNullException(nameof(_genreDataAccess));
        }

        public async Task CreateGenreAsync(Genre newGenre)
        {
            await _genreDataAccess.CreateGenreAsync(newGenre);
        }

        public async Task<Genre> GetGenreByIDAsync(int genreID)
        {
            return await _genreDataAccess.GetGenreByIDAsync(genreID);
        }

        public async Task<List<Genre>> GetAllGenresAsync()
        {
            return await _genreDataAccess.GetAllGenresAsync();
        }

        public async Task UpdateGenreAsync(Genre genre)
        {
            await _genreDataAccess.UpdateGenreAsync(genre);
        }

        public async Task DeleteGenreByIDAsync(int genreID)
        {
            await _genreDataAccess.DeleteGenreByIDAsync(genreID);
        }
    }
}