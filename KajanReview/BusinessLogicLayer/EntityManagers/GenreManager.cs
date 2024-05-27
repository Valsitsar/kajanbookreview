using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;

namespace BusinessLogicLayer.EntityManagers
{
    public class GenreManager : IGenreManager
    {
        private IGenreDataAccess _genreDataAccess;

        public GenreManager(IGenreDataAccess genreDataAccess)
        {
            _genreDataAccess = genreDataAccess ?? throw new ArgumentNullException(nameof(_genreDataAccess));
        }

        public void CreateGenre(Genre newGenre)
        {
            _genreDataAccess.CreateGenre(newGenre);
        }

        public Genre GetGenreByID(int genreID)
        {
            return _genreDataAccess.GetGenreByID(genreID);
        }

        public List<Genre> GetAllGenres()
        {
            return _genreDataAccess.GetAllGenres();
        }

        public void UpdateGenre(Genre genre)
        {
            _genreDataAccess.UpdateGenre(genre);
        }

        public void DeleteGenreByID(int genreID)
        {
            _genreDataAccess.DeleteGenreByID(genreID);
        }
    }
}