using BusinessLogicLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IGenreDataAccess
    {
        public void CreateGenre(Genre newGenre);
        public Genre GetGenreByID(int genreID);
        public List<Genre> GetAllGenres();
        public void UpdateGenre(Genre genre);
        public void DeleteGenreByID(int genreID);
    }
}
