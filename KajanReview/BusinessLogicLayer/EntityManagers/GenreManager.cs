using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.EntityManagers
{
    public class GenreManager : IGenreManager
    {
        private IGenreDataAccess _genreDataAccess;

        public GenreManager(IGenreDataAccess genreDataAccess)
        {
            _genreDataAccess = genreDataAccess;
        }

        public void CreateGenre(Genre newGenre)
        {
            try { _genreDataAccess.CreateGenre(newGenre); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        public Genre GetGenreByID(int genreID)
        {
            try { return _genreDataAccess.GetGenreByID(genreID); }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<Genre> GetAllGenres()
        {
            try { return _genreDataAccess.GetAllGenres(); }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return [];
            }
        }

        public void UpdateGenre(Genre genre)
        {
            try { _genreDataAccess.UpdateGenre(genre); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        public void DeleteGenreByID(int genreID)
        {
            try { _genreDataAccess.DeleteGenreByID(genreID); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
    }
}