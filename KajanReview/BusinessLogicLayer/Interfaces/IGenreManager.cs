using BusinessLogicLayer.Entities;

namespace BusinessLogicLayer.Interfaces
{
    public interface IGenreManager
    {
        public void CreateGenre(Genre newGenre);
        public Genre GetGenreByID(int genreID);
        public List<Genre> GetAllGenres();
        public void UpdateGenre(Genre genre);
        public void DeleteGenreByID(int genreID);
    }
}
