using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;
using Microsoft.SqlServer.Server;
using Microsoft.VisualBasic;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Text;

namespace DataAccessLayer
{
    public class UserDataAccess : DataAccessBase, IUserDataAccess
    {
        public async Task CreateUserAsync(UserDTO newUser, string hashedPassword, string salt)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    INSERT INTO 
                        Users (FirstName, MiddleNames, LastName, Username, Email, PhoneNumber, 
                               PasswordHash, PasswordSalt, ProfilePictureFilePath, Role) 
                    VALUES 
                        (@FirstName, @MiddleNames, @LastName, @Username, @Email, 
                        @PhoneNumber, @PasswordHash, @PasswordSalt, @ProfilePictureFilePath, @Role); ";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", newUser.FirstName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@MiddleNames", newUser.MiddleNames ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@LastName", newUser.LastName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Username", newUser.Username);
                    command.Parameters.AddWithValue("@Email", newUser.Email);
                    command.Parameters.AddWithValue("@PhoneNumber", newUser.PhoneNumber ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@PasswordHash", hashedPassword);
                    command.Parameters.AddWithValue("@PasswordSalt", salt);
                    command.Parameters.AddWithValue("@ProfilePictureFilePath", newUser.ProfilePictureFilePath ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Role", newUser.Role.ID);

                    try
                    {
                        await connection.OpenAsync();
                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        if (rowsAffected == 0)
                        {
                            throw new Exception("No rows were inserted. The User may not have been created.");
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Handle SQL exceptions (e.g., query syntax errors, constraint violations)
                        throw new IOException("Failed to create the User.", ex);
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

        public async Task CreateDefaultBookshelvesForUserAsync(int userID)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    INSERT INTO
                        Bookshelves (OwnerID, Name)
                    VALUES
                        (@OwnerID, 'Want to Read'),
                        (@OwnerID, 'Reading'),
                        (@OwnerID, 'Read'),
                        (@OwnerID, 'Favorites'); ";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@OwnerID", userID);

                    try
                    {
                        await connection.OpenAsync();
                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        if (rowsAffected == 0)
                        {
                            throw new Exception("No rows were inserted. The Bookshelves may not have been created.");
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Handle SQL exceptions (e.g., query syntax errors, constraint violations)
                        throw new IOException("Failed to create the Bookshelves.", ex);
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

        public async Task<int> GetLastUserID()
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    SELECT TOP 1 ID
                    FROM Users
                    ORDER BY ID DESC; ";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    try
                    {
                        await connection.OpenAsync();
                        return await command.ExecuteScalarAsync() as int? ?? 0;
                    }
                    catch (SqlException ex)
                    {
                        // Handle SQL exceptions (e.g., query syntax errors)
                        throw new IOException("Failed to retrieve the last User ID.", ex);
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

        public async Task<UserDTO> GetUserByIDAsync(int userID)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    SELECT Users.ID AS UserID, FirstName, MiddleNames, LastName, Username, 
                    Email, PhoneNumber, ProfilePictureFilePath, Roles.Name AS Role
                    FROM Users 
                    JOIN Roles ON Users.Role = Roles.ID
                    WHERE Users.ID = @ID; ";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@ID", userID);

                    try
                    {
                        await connection.OpenAsync();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                UserDTO userDTO = new UserDTO()
                                {
                                    FirstName = reader.IsDBNull(reader.GetOrdinal("FirstName")) ? null : reader.GetString("FirstName"),
                                    MiddleNames = reader.IsDBNull(reader.GetOrdinal("MiddleNames")) ? null : reader.GetString("MiddleNames"),
                                    LastName = reader.IsDBNull(reader.GetOrdinal("LastName")) ? null : reader.GetString("LastName"),
                                    Username = reader.GetString("Username"),
                                    Email = reader.GetString("Email"),
                                    PhoneNumber = reader.IsDBNull(reader.GetOrdinal("PhoneNumber")) ? null : reader.GetString("PhoneNumber"),
                                    ProfilePictureFilePath = reader.IsDBNull(reader.GetOrdinal("ProfilePictureFilePath")) ? null : reader.GetString("ProfilePictureFilePath"),
                                    Role = new Role() { Name = reader.GetString("Role") }
                                };
                                return userDTO;
                            }
                            else { return new UserDTO(); }
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Handle SQL exceptions (e.g., query syntax errors)
                        throw new IOException("Failed to retrieve the User.", ex);
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

        public async Task<User> GetUserByUsernameForLoginAsync(string username)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    SELECT Users.ID, Username, PasswordHash, PasswordSalt, Roles.Name AS Role
                    FROM Users 
                    JOIN Roles ON Users.Role = Roles.ID
                    WHERE Username = @Username; ";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    try
                    {
                        await connection.OpenAsync();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.Read())
                            {
                                User user = new User()
                                {
                                    ID = reader.GetInt32("ID"),
                                    Username = reader.GetString("Username"),
                                    PasswordHash = reader.GetString("PasswordHash"),
                                    PasswordSalt = reader.GetString("PasswordSalt"),
                                    Role = new Role() { Name = reader.GetString("Role") }
                                };
                                return user;
                            }
                            else { return new User(); }
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Handle SQL exceptions (e.g., query syntax errors)
                        throw new IOException("Failed to retrieve the User.", ex);
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

        public async Task<User> GetUserByEmailForLoginAsync(string email)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    SELECT Users.ID, Email, PasswordHash, PasswordSalt, Roles.Name AS Role
                    FROM Users 
                    JOIN Roles ON Users.Role = Roles.ID
                    WHERE Email = @Email; ";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);

                    try
                    {
                        await connection.OpenAsync();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.Read())
                            {
                                User user = new User()
                                {
                                    ID = reader.GetInt32("ID"),
                                    Email = reader.GetString("Email"),
                                    PasswordHash = reader.GetString("PasswordHash"),
                                    PasswordSalt = reader.GetString("PasswordSalt"),
                                    Role = new Role() { Name = reader.GetString("Role") }
                                };
                                return user;
                            }
                            else { return new User(); }
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Handle SQL exceptions (e.g., query syntax errors)
                        throw new IOException("Failed to retrieve the User.", ex);
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

        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    SELECT Users.ID, FirstName, MiddleNames, LastName, Username, 
                    Email, PhoneNumber, ProfilePictureFilePath, Roles.Name AS Role
                    FROM Users
                    JOIN Roles ON Users.Role = Roles.ID; ";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    try
                    {
                        List<UserDTO> _userDTOs = [];

                        await connection.OpenAsync();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                string middleNames = !reader.IsDBNull("MiddleNames") ? reader.GetString("MiddleNames") : "";

                                UserDTO userDTO = new UserDTO()
                                {
                                    ID = reader.GetInt32("ID"),
                                    FirstName = reader.GetString("FirstName"),
                                    MiddleNames = middleNames,
                                    LastName = reader.GetString("LastName"),
                                    Username = reader.GetString("Username"),
                                    Email = reader.GetString("Email"),
                                    PhoneNumber = reader.GetString("PhoneNumber"),
                                    ProfilePictureFilePath = reader.GetString("ProfilePictureFilePath"),
                                    Role = new Role() { Name = reader.GetString("Role") }
                                };
                                _userDTOs.Add(userDTO);
                            }

                            return _userDTOs;
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Handle SQL exceptions (e.g., query syntax errors)
                        throw new IOException("Failed to retrieve the user.", ex);
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

        public async Task<List<Review>> GetReviewsByUserIDAsync(int userID)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    SELECT ID, Title, Body, UpvoteCount, DownvoteCount, PostDate, BookRating, BookID
                    FROM Reviews
                    WHERE UserID = @UserID; ";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);

                    try
                    {
                        List<Review> reviews = [];

                        await connection.OpenAsync();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                Review review = new Review()
                                {
                                    ID = reader.GetInt32("ID"),
                                    Title = reader.IsDBNull("Title") ? "" : reader.GetString("Title"),
                                    Body = reader.IsDBNull("Body") ? "" : reader.GetString("Body"),
                                    UpvoteCount = reader.GetInt32("UpvoteCount"),
                                    DownvoteCount = reader.GetInt32("DownvoteCount"),
                                    PostDate = reader.GetDateTime("PostDate"),
                                    BookRating = reader.GetInt32("BookRating"),
                                    SourceBook = new Book() { ID = reader.GetInt32("BookID") }
                                };
                                reviews.Add(review);
                            }

                            return reviews;
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Handle SQL exceptions (e.g., query syntax errors)
                        throw new IOException("Failed to retrieve the ratings.", ex);
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

        public async Task<(List<Bookshelf>, List<int>)> GetBookshelfNamesAndCountsForUserAsync(int userID)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    SELECT 
	                    Bookshelves.ID AS BookshelfID, 
	                    Name, 
                        COUNT(*) AS BookCount
                    FROM 
	                    Bookshelves
                    INNER JOIN 
	                    Books_Bookshelves ON Bookshelves.ID = Books_Bookshelves.BookshelfID
                    INNER JOIN 
	                    Books ON Books_Bookshelves.BookID = Books.ID
                    WHERE 
	                    OwnerID = @UserID
                    GROUP BY 
	                    Bookshelves.ID, Bookshelves.Name
                    ORDER BY
	                    Bookshelves.ID; ";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);

                    try
                    {
                        List<Bookshelf> bookshelves = [];
                        List<int> bookCounts = [];

                        await connection.OpenAsync();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                Bookshelf bookshelf = new Bookshelf()
                                {
                                    ID = reader.GetInt32("BookshelfID"),
                                    Name = reader.GetString("Name"),
                                };
                                bookshelves.Add(bookshelf);
                                bookCounts.Add(reader.GetInt32("BookCount"));
                            }

                            return (bookshelves, bookCounts);
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Handle SQL exceptions (e.g., query syntax errors)
                        throw new IOException("Failed to retrieve the bookshelves.", ex);
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

        public async Task<List<Bookshelf>> GetBookshelvesForUserAsync(int userID)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    SELECT 
	                    Bookshelves.ID AS BookshelfID, 
	                    Name
                    FROM 
	                    Bookshelves
                    INNER JOIN 
	                    Books_Bookshelves ON Bookshelves.ID = Books_Bookshelves.BookshelfID
                    INNER JOIN 
	                    Books ON Books_Bookshelves.BookID = Books.ID
                    WHERE 
	                    OwnerID = @UserID
                    GROUP BY 
	                    Bookshelves.ID, Bookshelves.Name
                    ORDER BY
	                    Bookshelves.ID; ";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);

                    try
                    {
                        List<Bookshelf> bookshelves = [];

                        await connection.OpenAsync();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                Bookshelf bookshelf = new Bookshelf()
                                {
                                    ID = reader.GetInt32("BookshelfID"),
                                    Name = reader.GetString("Name"),
                                };
                                bookshelves.Add(bookshelf);
                            }

                            return bookshelves;
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Handle SQL exceptions (e.g., query syntax errors)
                        throw new IOException("Failed to retrieve the bookshelves.", ex);
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

        public async Task<List<Book>> GetFavoritesByUserAsync(int userID)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    SELECT Books.ID as BookID, Title, Description, PageCount, Publisher, PubDate, Language, ISBN, BookFormatID, CoverFilePath
                    FROM Bookshelves
                    INNER JOIN Books_Bookshelves
                    ON Bookshelves.ID = Books_Bookshelves.BookshelfID
                    INNER JOIN Books
                    ON Books_Bookshelves.BookID = Books.ID
                    WHERE Bookshelves.OwnerID = 3 AND
                    Bookshelves.Name = 'Favorites'; ";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);

                    try
                    {
                        List<Book> favorites = [];

                        await connection.OpenAsync();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                Book book = new Book()
                                {
                                    ID = reader.GetInt32("BookID"),
                                    Title = reader.GetString("Title"),
                                    Description = reader.GetString("Description"),
                                    PageCount = reader.GetInt32("PageCount"),
                                    Publisher = reader.GetString("Publisher"),
                                    PubDate = reader.GetDateTime("PubDate"),
                                    Language = reader.GetString("Language"),
                                    ISBN = reader.GetString("ISBN"),
                                    Format = new BookFormat() { ID = reader.GetInt32("BookFormatID") },
                                    CoverFilePath = reader.IsDBNull("CoverFilePath") ? "" : reader.GetString("CoverFilePath"),
                                };
                                favorites.Add(book);
                            }

                            return favorites;
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Handle SQL exceptions (e.g., query syntax errors)
                        throw new IOException("Failed to retrieve the favorites.", ex);
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

        public async Task<(string? hashedPassword, string? salt)> GetPasswordHashAndSaltByUsernameAsync(string username)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    SELECT PasswordHash, Salt 
                    FROM Users 
                    WHERE Username = @Username; ";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    try
                    {
                        await connection.OpenAsync();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.Read())
                            {
                                string hashedPassword = reader.GetString("PasswordHash");
                                string salt = reader.GetString("Salt");
                                return (hashedPassword, salt);
                            }
                            else
                            {
                                // User doesn't exist
                                return (null, null);
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Handle SQL exceptions (e.g., query syntax errors)
                        throw new IOException("Failed to retrieve the Hash and Salt.", ex);
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

        public async Task<(string? hashedPassword, string? salt)> GetPasswordHashAndSaltByUserIDAsync(int userID)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    SELECT PasswordHash, Salt 
                    FROM Users 
                    WHERE ID = @ID; ";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@ID", userID);

                    try
                    {
                        await connection.OpenAsync();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                string hashedPassword = reader.GetString("PasswordHash");
                                string salt = reader.GetString("Salt");
                                return (hashedPassword, salt);
                            }
                            else
                            {
                                // User doesn't exist
                                return (null, null);
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Handle SQL exceptions (e.g., query syntax errors)
                        throw new IOException("Failed to retrieve the user.", ex);
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

        public async Task UpdatePasswordHashAndSaltByUserIDAsync(int userID, string hashedPassword, string salt)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    UPDATE Users 
                    SET PasswordHash = @PasswordHash, Salt = @Salt 
                    WHERE ID = @ID; ";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@Username", userID);
                    command.Parameters.AddWithValue("@PasswordHash", hashedPassword);
                    command.Parameters.AddWithValue("@Salt", salt);

                    try
                    {
                        await connection.OpenAsync();
                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        if (rowsAffected == 0)
                        {
                            throw new Exception("No rows were inserted. The Hash and Salt may not have been updated.");
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Handle SQL exceptions (e.g., query syntax errors, constraint violations)
                        throw new IOException("Failed to update the Hash and Salt.", ex);
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

        public async Task UpdateUserAsync(UserDTO userDTO)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    UPDATE Users 
                    SET FirstName = @FirstName, MiddleNames = @MiddleNames, LastName = @LastName, 
                    Username = @Username, Email = @Email, PhoneNumber = @PhoneNumber, 
                    ProfilePictureFilePath = @ProfilePictureFilePath, Role = @Role 
                    WHERE ID = @ID; ";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@ID", userDTO.ID);
                    command.Parameters.AddWithValue("@FirstName", userDTO.FirstName);
                    command.Parameters.AddWithValue("@MiddleNames", userDTO.MiddleNames);
                    command.Parameters.AddWithValue("@LastName", userDTO.LastName);
                    command.Parameters.AddWithValue("@Username", userDTO.Username);
                    command.Parameters.AddWithValue("@Email", userDTO.Email);
                    command.Parameters.AddWithValue("@PhoneNumber", userDTO.PhoneNumber);
                    command.Parameters.AddWithValue("@ProfilePictureFilePath", userDTO.ProfilePictureFilePath);
                    command.Parameters.AddWithValue("@Role", userDTO.Role.ID);

                    try
                    {
                        await connection.OpenAsync();
                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        if (rowsAffected == 0)
                        {
                            throw new Exception("No rows were inserted. The user may not have been updated.");
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Handle SQL exceptions (e.g., query syntax errors, constraint violations)
                        throw new IOException("Failed to update the user.", ex);
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

        public async Task DeleteUserByIDAsync(int userID)
        {
            // I'm not sure if I should allow full deletion of a user;
            // It might be better to keep it archived or something
            throw new NotImplementedException();
        }
    }
}
