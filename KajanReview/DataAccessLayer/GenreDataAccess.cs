using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class GenreDataAccess : DataAccessBase, IGenreDataAccess
    {
        public void CreateGenre(Genre newGenre)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery =
                    @"INSERT INTO Genres (Name) 
                    VALUES (@Name); ";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@Name", newGenre.Name);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        throw new IOException("Failed to create the Genre.", ex);
                    }
                }
            }
        }

        public Genre GetGenreByID(int genreID)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery =
                    @"SELECT ID, Name 
                    FROM Genres 
                    WHERE ID = @ID; ";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@ID", genreID);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            Genre genre = new Genre()
                            {
                                ID = reader.GetInt32("ID"),
                                Name = reader.GetString("Name")
                            };
                            return genre;
                        }
                        else { return null; }
                    }
                    catch (Exception ex)
                    {
                        throw new IOException("Failed to get the Genre.", ex);
                    }
                }
            }
        }

        public List<Genre> GetAllGenres()
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery =
                    @"SELECT ID, Name 
                    FROM Genres; ";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    try
                    {
                        List<Genre> _genres= [];

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            Genre genre = new Genre()
                            {
                                ID = reader.GetInt32("ID"),
                                Name = reader.GetString("Name")
                            };
                            _genres.Add(genre);
                        }
                        if (_genres.Count > 0) { return _genres; }
                        else { return null; }
                    }
                    catch (Exception ex)
                    {
                        throw new IOException("Failed to get the Genres.", ex);
                    }
                }
            }
        }

        public void UpdateGenre(Genre genre)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery =
                    @"UPDATE Genres 
                    SET Name = @Name 
                    WHERE ID = @ID; ";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@ID", genre.ID);
                    command.Parameters.AddWithValue("@Name", genre.Name);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        throw new IOException("Failed to update the Genre.", ex);
                    }
                }
            }
        }

        public void DeleteGenreByID(int genreID)
        {
            // I'm not sure if I should allow full deletion of a Genre;
            // It might be better to keep it archived or something
            throw new NotImplementedException();
        }
    }
}
