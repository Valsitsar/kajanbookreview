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
                    INSERT INTO 
                        Bookshelves (Name, OwnerID) 
                    VALUES 
                        (@Name, @OwnerID); ";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@Name", newBookshelf.Name);
                    command.Parameters.AddWithValue("@OwnerID", newBookshelf.Owner.ID);

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
                                    ID = reader.GetInt32("Bookshelf.ID"),
                                    Name = reader.GetString("Name"),
                                    Owner = new User()
                                    {
                                        ID = reader.GetInt32("OwnerID"),
                                        Username = reader.GetString("OwnerUsername")
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

        #region Get all data about bookshelves by userID
        public async Task<List<Bookshelf>> GetAllBookshelvesForUserAsync(int userID)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    SELECT ID, Name
                    FROM Bookshelves
                    WHERE OwnerID = @UserID; ";

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
                                    ID = reader.GetInt32("ID"),
                                    Name = reader.GetString("Name"),
                                    Books = await GetBooksByBookshelfIDAsync(reader.GetInt32("ID"))
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

        private async Task<List<Book>> GetBooksByBookshelfIDAsync(int bookshelfID)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    SELECT 
                        Books.ID as BookID, Title, Description, PageCount, 
                        Publisher, PubDate, Language, ISBN, BookFormatID, CoverFilePath
                    FROM 
                        Books_Bookshelves
                    INNER JOIN 
                        Books ON Books_Bookshelves.BookID = Books.ID
                    WHERE 
                        Books_Bookshelves.BookshelfID = @BookshelfID; ";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@BookshelfID", bookshelfID);

                    try
                    {
                        List<Book> books = [];

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
                                    CoverFilePath = reader.IsDBNull("CoverFilePath") ? null : reader.GetString("CoverFilePath"),
                                    Reviews = await GetReviewsByBookIDAsync(reader.GetInt32("BookID")),
                                    Authors = await GetAuthorsByBookIDAsync(reader.GetInt32("BookID"))
                                };
                                books.Add(book);
                            }
                        }
                        return books;
                    }
                    catch (SqlException ex)
                    {
                        // Handle SQL exceptions (e.g., query syntax errors)
                        throw new IOException("Failed to retrieve the books.", ex);
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

        private async Task<List<Review>> GetReviewsByBookIDAsync(int bookID)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    SELECT 
	                    BookID, ID AS ReviewID, UserID, Title, Body, UpvoteCount, DownvoteCount, PostDate, BookRating
                    FROM 
	                    Reviews
                    WHERE
	                    BookID = @BookID; ";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("BookID", bookID);

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
                                    ID = reader.GetInt32("ReviewID"),
                                    SourceBook = new Book() { ID = reader.GetInt32("BookID") },
                                    Poster = new User() { ID = reader.GetInt32("UserID") },
                                    Title = reader.GetString("Title"),
                                    Body = reader.GetString("Body"),
                                    UpvoteCount = reader.GetInt32("UpvoteCount"),
                                    DownvoteCount = reader.GetInt32("DownvoteCount"),
                                    PostDate = reader.GetDateTime("PostDate"),
                                    BookRating = reader.GetInt32("BookRating")
                                };
                                reviews.Add(review);
                            }
                            return reviews;
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Handle SQL exceptions (e.g., query syntax errors)
                        throw new IOException("Failed to retrieve the reviews.", ex);
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

        private async Task<List<User>> GetAuthorsByBookIDAsync(int bookID)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    SELECT Users.ID as UserID, FirstName, MiddleNames, LastName
                    FROM Books_Authors
                    INNER JOIN Users
                    ON Books_Authors.UserID = Users.ID
                    WHERE Books_Authors.BookID = @BookID; ";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@BookID", bookID);

                    try
                    {
                        List<User> authors = [];

                        await connection.OpenAsync();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                User author = new User()
                                {
                                    ID = reader.GetInt32("UserID"),
                                    FirstName = reader.GetString("FirstName"),
                                    MiddleNames = reader.IsDBNull("MiddleNames") ? null : reader.GetString("MiddleNames"),
                                    LastName = reader.GetString("LastName")
                                };
                                authors.Add(author);
                            }
                        }
                        return authors;
                    }
                    catch (SqlException ex)
                    {
                        // Handle SQL exceptions (e.g., query syntax errors)
                        throw new IOException("Failed to retrieve the authors.", ex);
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
        #endregion

        #region Pagination queries
        public async Task<List<Book>> GetPagedBooksByBookshelfIDAsync(int bookshelfID, int pageNumber, int pageSize)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    SELECT 
                        Books.ID as BookID, Title, Description, PageCount, 
                        Publisher, PubDate, Language, ISBN, BookFormatID, CoverFilePath
                    FROM 
                        Books_Bookshelves
                    INNER JOIN 
                        Books ON Books_Bookshelves.BookID = Books.ID
                    WHERE 
                        Books_Bookshelves.BookshelfID = @BookshelfID
                    ORDER BY
                        Books_Bookshelves.DateAdded
                    OFFSET @Offset ROWS
                    FETCH NEXT @PageSize ROWS ONLY; ";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@BookshelfID", bookshelfID);
                    command.Parameters.AddWithValue("@Offset", (pageNumber - 1) * pageSize);
                    command.Parameters.AddWithValue("@PageSize", pageSize);

                    try
                    {
                        List<Book> books = [];

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
                                    CoverFilePath = reader.IsDBNull("CoverFilePath") ? null : reader.GetString("CoverFilePath"),
                                    Reviews = await GetReviewsByBookIDAsync(reader.GetInt32("BookID")),
                                    Authors = await GetAuthorsByBookIDAsync(reader.GetInt32("BookID"))
                                };
                                books.Add(book);
                            }
                        }
                        return books;
                    }
                    catch (SqlException ex)
                    {
                        // Handle SQL exceptions (e.g., query syntax errors)
                        throw new IOException("Failed to retrieve the books.", ex);
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

        public async Task<int> GetTotalBooksCountByBookshelfIDAsync(int bookshelfID)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                SELECT
                    COUNT(*)
                FROM
                    Books_Bookshelves
                WHERE
                    BookshelfID = @BookshelfID; ";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@BookshelfID", bookshelfID);

                    try
                    {
                        int count = 0;

                        await connection.OpenAsync();
                        count = (int)await command.ExecuteNonQueryAsync();

                        return count;
                    }
                    catch (SqlException ex)
                    {
                        // Handle SQL exceptions (e.g., query syntax errors)
                        throw new IOException("Failed to get the book count.", ex);
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

        public async Task<List<Book>> GetPagedBooksAcrossAllShelvesAsync(int userID, int pageNumber, int pageSize)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    SELECT 
                        Books.ID AS BookID, Title, Description, PageCount, 
                        Publisher, PubDate, Language, ISBN, BookFormatID, CoverFilePath
                    FROM 
                        Books
                    INNER JOIN 
                        Books_Bookshelves ON Books.ID = Books_Bookshelves.BookID
                    INNER JOIN 
                        Bookshelves ON Books_Bookshelves.BookshelfID = Bookshelves.ID
                    WHERE 
                        Bookshelves.OwnerID = @UserID
                    ORDER BY 
                        Books_Bookshelves.DateAdded
                    OFFSET @Offset ROWS
                    FETCH NEXT @PageSize ROWS ONLY;";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@Offset", (pageNumber - 1) * pageSize);
                    command.Parameters.AddWithValue("@PageSize", pageSize);

                    try
                    {
                        List<Book> books = [];

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
                                    CoverFilePath = reader.IsDBNull("CoverFilePath") ? null : reader.GetString("CoverFilePath"),
                                    Reviews = await GetReviewsByBookIDAsync(reader.GetInt32("BookID")),
                                    Authors = await GetAuthorsByBookIDAsync(reader.GetInt32("BookID"))
                                };
                                books.Add(book);
                            }
                        }
                        return books;
                    }
                    catch (SqlException ex)
                    {
                        // Handle SQL exceptions (e.g., query syntax errors)
                        throw new IOException("Failed to retrieve the books.", ex);
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

        public async Task<int> GetTotalBooksCountAcrossAllShelvesAsync(int userID)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    SELECT 
                        COUNT(*)
                    FROM 
                        Books
                    INNER JOIN 
                        Books_Bookshelves ON Books.ID = Books_Bookshelves.BookID
                    INNER JOIN 
                        Bookshelves ON Books_Bookshelves.BookshelfID = Bookshelves.ID
                    WHERE 
                        Bookshelves.OwnerID = @UserID; ";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);

                    try
                    {
                        int count = 0;

                        await connection.OpenAsync();
                        count = (int)await command.ExecuteNonQueryAsync();

                        return count;
                    }
                    catch (SqlException ex)
                    {
                        // Handle SQL exceptions (e.g., query syntax errors)
                        throw new IOException("Failed to get the book count.", ex);
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
        #endregion

        public async Task<List<Book>> GetBooksByAuthorAsync(int userID)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    SELECT Books.ID as BookID, Title, Description, PageCount, Publisher, PubDate, Language, ISBN, BookFormatID, CoverFilePath
                    FROM Books_Authors
                    INNER JOIN Books
                    ON Books_Authors.BookID = Books.ID
                    WHERE Books_Authors.UserID = @UserID; ";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);

                    try
                    {
                        List<Book> books = [];

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
                                    CoverFilePath = reader.IsDBNull("CoverFilePath") ? null : reader.GetString("CoverFilePath"),
                                    Authors = await GetAuthorsByBookIDAsync(reader.GetInt32("BookID"))
                                };
                                books.Add(book);
                            }
                        }
                        return books;
                    }
                    catch (SqlException ex)
                    {
                        // Handle SQL exceptions (e.g., query syntax errors)
                        throw new IOException("Failed to retrieve the books.", ex);
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
                    command.Parameters.AddWithValue("@Name", bookshelf.Name);
                    command.Parameters.AddWithValue("@OwnerID", bookshelf.Owner.ID);

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
