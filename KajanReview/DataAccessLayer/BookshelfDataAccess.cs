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
    public class BookshelfDataAccess : DataAccessBase, IBookshelfDataAccess
    {
        public BookshelfDataAccess() { }

        public void CreateBookshelf(Bookshelf newBookshelf)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery =
                    @"INSERT INTO Bookshelves (Name, OwnerID) 
                    VALUES (@Name, @OwnerID); ";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@Title", newBookshelf.Name);
                    command.Parameters.AddWithValue("@Description", newBookshelf.Owner.ID);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        throw new IOException("Failed to create the bookshelf.", ex);
                    }
                }
            }
        }

        public Bookshelf GetBookshelfByID(int bookshelfID)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery =
                    @"SELECT Bookshelves.ID, Name, OwnerID, Users.Username AS OwnerUsername 
                    FROM Bookshelves 
                    INNER JOIN Users 
                    ON Bookshelves.OwnerID = Users.ID 
                    WHERE Bookshelves.ID = @ID; ";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@ID", bookshelfID);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            Bookshelf bookshelf = new Bookshelf()
                            {
                                ID = reader.GetInt32("ID"),
                                Name = reader.GetString("Name"),
                                Owner = new User()
                                {
                                    ID = reader.GetInt32("Users.ID"),
                                    Username = reader.GetString("Users.Username")
                                }
                            };
                            return bookshelf;
                        }
                        else { return null; }
                    }
                    catch (SqlException ex)
                    {
                        throw new IOException("Failed to get the Bookshelf.", ex);
                    }
                }
            }
        }

        public List<Bookshelf> GetAllBookshelvesForUser(int userID)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery =
                    @"SELECT Bookshelves.ID, Name, OwnerID, Users.Username AS OwnerUsername 
                    FROM Bookshelves 
                    INNER JOIN Users 
                    ON Bookshelves.OwnerID = Users.ID 
                    WHERE Users.ID = @ID; ";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@ID", userID);

                    try
                    {
                        List<Bookshelf> _bookshelves = [];

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            Bookshelf bookshelf = new Bookshelf()
                            {
                                ID = reader.GetInt32("ID"),
                                Name = reader.GetString("Name"),
                                Owner = new User()
                                {
                                    // ASK GUI IF THIS IS GOOD OR IF I'M SUPPOSED TO ASSIGN VALUES TO ALL USER PROPERTIES
                                    ID = reader.GetInt32("Users.ID"),
                                    Username = reader.GetString("Users.Username")
                                }
                            };
                            _bookshelves.Add(bookshelf);
                        }

                        if (_bookshelves.Count > 0) { return _bookshelves; }
                        else { return null; }
                    }
                    catch (SqlException ex)
                    {
                        throw new IOException("Failed to get the Bookshelves.", ex);
                    }
                }
            }
        }

        public void UpdateBookshelf(Bookshelf bookshelf)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery =
                    @"UPDATE Bookshelves 
                    SET Name = @Name, OwnerID = @OwnerID 
                    WHERE ID = @ID; ";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@ID", bookshelf.ID);
                    command.Parameters.AddWithValue("@Title", bookshelf.Name);
                    command.Parameters.AddWithValue("@Description", bookshelf.Owner.ID);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        throw new IOException("Failed to update the Bookshelf.", ex);
                    }
                }
            }
        }

        public void DeleteBookshelfByID(int bookshelfID)
        {
            // I'm not sure if I should allow full deletion of a bookshelf;
            // It might be better to keep it archived or something
            throw new NotImplementedException();
        }
    }
}
