using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessLayer
{
    public class BookshelfDataAccess : DataAccessBase, IBookshelfDataAccess
    {
        public BookshelfDataAccess() { }

        public async Task CreateBookshelfAsync(Bookshelf newBookshelf)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    INSERT INTO Bookshelves (Name, OwnerID) 
                    VALUES (@Name, @OwnerID); ";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@Title", newBookshelf.Name);
                    command.Parameters.AddWithValue("@Description", newBookshelf.Owner.ID);

                    try
                    {
                        await connection.OpenAsync();
                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        if (rowsAffected == 0)
                        {
                            throw new Exception("No rows were inserted. The Bookshelf may not have been created.");
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Handle SQL exceptions (e.g., query syntax errors, constraint violations)
                        throw new IOException("Failed to update the Bookshelf.", ex);
                    }
                    catch (InvalidOperationException ex)
                    {
                        // Handle exceptions related to the connection (e.g., not open)
                        throw new IOException("Failed to open the database connection.", ex);
                    }
                    catch (Exception ex)
                    {
                        // Handle any other exceptions
                        throw new IOException("An unexpected error occurred.", ex);
                    }
                }
            }
        }

        public async Task<Bookshelf> GetBookshelfByIDAsync(int bookshelfID)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    SELECT Bookshelves.ID, Name, OwnerID, Users.Username AS OwnerUsername 
                    FROM Bookshelves 
                    INNER JOIN Users 
                    ON Bookshelves.OwnerID = Users.ID 
                    WHERE Bookshelves.ID = @ID; ";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@ID", bookshelfID);

                    try
                    {
                        await connection.OpenAsync();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
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
                            else { return new Bookshelf(); }
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Handle SQL exceptions (e.g., query syntax errors)
                        throw new IOException("Failed to retrieve the Bookshelf.", ex);
                    }
                    catch (InvalidOperationException ex)
                    {
                        // Handle exceptions related to the connection (e.g., not open)
                        throw new IOException("Failed to open the database connection.", ex);
                    }
                    catch (Exception ex)
                    {
                        // Handle any other exceptions
                        throw new IOException("An unexpected error occurred.", ex);
                    }
                }
            }
        }

        public async Task<List<Bookshelf>> GetAllBookshelvesForUserAsync(int userID)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    SELECT Bookshelves.ID, Name, OwnerID, Users.Username AS OwnerUsername 
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

                        await connection.OpenAsync();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
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
                                _bookshelves.Add(bookshelf);
                            }

                            if (_bookshelves.Count > 0) { return _bookshelves; }
                            else { return []; }
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Handle SQL exceptions (e.g., query syntax errors)
                        throw new IOException("Failed to retrieve the Bookshelf.", ex);
                    }
                    catch (InvalidOperationException ex)
                    {
                        // Handle exceptions related to the connection (e.g., not open)
                        throw new IOException("Failed to open the database connection.", ex);
                    }
                    catch (Exception ex)
                    {
                        // Handle any other exceptions
                        throw new IOException("An unexpected error occurred.", ex);
                    }
                }
            }
        }

        public async Task UpdateBookshelfAsync(Bookshelf bookshelf)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    UPDATE Bookshelves 
                    SET Name = @Name, OwnerID = @OwnerID 
                    WHERE ID = @ID; ";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@ID", bookshelf.ID);
                    command.Parameters.AddWithValue("@Title", bookshelf.Name);
                    command.Parameters.AddWithValue("@Description", bookshelf.Owner.ID);

                    try
                    {
                        await connection.OpenAsync();
                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        if (rowsAffected == 0)
                        {
                            throw new Exception("No rows were inserted. The Bookshelf may not have been updated.");
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Handle SQL exceptions (e.g., query syntax errors, constraint violations)
                        throw new IOException("Failed to update the Bookshelf.", ex);
                    }
                    catch (InvalidOperationException ex)
                    {
                        // Handle exceptions related to the connection (e.g., not open)
                        throw new IOException("Failed to open the database connection.", ex);
                    }
                    catch (Exception ex)
                    {
                        // Handle any other exceptions
                        throw new IOException("An unexpected error occurred.", ex);
                    }
                }
            }
        }

        public async Task DeleteBookshelfByIDAsync(int bookshelfID)
        {
            // I'm not sure if I should allow full deletion of a bookshelf;
            // It might be better to keep it archived or something
            throw new NotImplementedException();
        }
    }
}
