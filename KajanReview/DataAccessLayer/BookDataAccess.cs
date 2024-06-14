using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessLayer
{
    public class BookDataAccess : DataAccessBase, IBookDataAccess
    {
        public BookDataAccess() { }

        public async Task CreateBookAsync(Book newBook)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    INSERT INTO Books (Title, Description, PageCount, Publisher, PubDate, Language, ISBN, BookFormatID)
                    VALUES (@Title, @Description, @PageCount, @Publisher, @PubDate, @Language, @ISBN, @BookFormatID);";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@Title", newBook.Title);
                    command.Parameters.AddWithValue("@Description", newBook.Description);
                    command.Parameters.AddWithValue("@PageCount", newBook.PageCount);
                    command.Parameters.AddWithValue("@Publisher", newBook.Publisher);
                    command.Parameters.AddWithValue("@PubDate", newBook.PubDate);
                    command.Parameters.AddWithValue("@Language", newBook.Language);
                    command.Parameters.AddWithValue("@ISBN", newBook.ISBN);
                    command.Parameters.AddWithValue("@BookFormatID", newBook.Format.ID);

                    try
                    {
                        await connection.OpenAsync();
                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        if (rowsAffected == 0)
                        {
                            throw new Exception("No rows were inserted. The Book may not have been created.");
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Handle SQL exceptions (e.g., query syntax errors, constraint violations)
                        throw new IOException("Failed to create the Book.", ex);
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

        public async Task<bool> TryAddBookToBookshelfAsync(int bookID, int bookshelfID)
        {
           using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    IF NOT EXISTS (
                        SELECT 1
                            FROM Books_Bookshelves
                        WHERE
                            BookID = @BookID AND BookshelfID = @BookshelfID)
                    BEGIN
                        INSERT INTO 
                            Books_Bookshelves (BookID, BookshelfID, DateAdded)
                        VALUES 
                            (@BookID, @BookshelfID, GETDATE() );
                        SELECT 1; -- Success
                    END
                    ELSE
                    BEGIN
                        SELECT 0; -- Already exists
                    END";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@BookID", bookID);
                    command.Parameters.AddWithValue("@BookshelfID", bookshelfID);

                    try
                    {
                        await connection.OpenAsync();
                        var result = await command.ExecuteScalarAsync();
                        return (int)result == 1;
                    }
                    catch (SqlException ex)
                    {
                        return false;
                        // Handle SQL exceptions (e.g., query syntax errors, constraint violations)
                        throw new IOException("Failed to add the Book to the Bookshelf.", ex);
                    }
                    catch (InvalidOperationException ex)
                    {
                        return false;
                        // Handle exceptions related to the connection (e.g., not open)
                        throw new IOException("Failed to open the database connection.", ex);
                    }
                    catch (Exception ex)
                    {
                        return false;
                        // Handle any other exceptions
                        throw new IOException("An unexpected error occurred.", ex);
                    }
                }
            }
        }

        public async Task<Book> GetBookByIDAsync(int bookID)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    SELECT 
                        b.ID, b.Title, b.Description, b.PageCount, b.Publisher, b.PubDate, b.Language, b.ISBN, 
	                    bf.ID as BookFormatID, bf.Name as BookFormatName, b.CoverFilePath,
                        u.ID as AuthorID, u.FirstName AS AuthorFirstName, u.MiddleNames AS AuthorMiddleNames, u.LastName AS AuthorLastName,
                        g.ID as GenreID, g.Name as GenreName,
                        r.ID as ReviewID, r.BookRating, r.Title as ReviewTitle, r.Body as ReviewBody, r.PostDate, r.UpvoteCount, r.DownvoteCount, 
                        ru.ID as ReviewerID, ru.Username as ReviewerUsername, ru.ProfilePictureFilePath as ReviewerProfilePicture
                    FROM 
	                    Books b
                    INNER JOIN 
	                    BookFormats bf ON b.BookFormatID = bf.ID
                    LEFT JOIN 
	                    Books_Authors ba ON b.ID = ba.BookID
                    LEFT JOIN 
	                    Users u ON ba.UserID = u.ID
                    LEFT JOIN 
	                    Books_Genres bg ON b.ID = bg.BookID
                    LEFT JOIN 
	                    Genres g ON bg.GenreID = g.ID
                    LEFT JOIN 
	                    Reviews r ON b.ID = r.BookID
                    LEFT JOIN 
	                    Users ru ON r.UserID = ru.ID
                    WHERE 
	                    b.ID = @ID
                    ORDER BY 
                        r.PostDate DESC; ";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@ID", bookID);

                    try
                    {
                        await connection.OpenAsync();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            Book book = null;
                            var authors = new List<User>();
                            var genres = new List<Genre>();
                            var reviews = new List<Review>();

                            while (await reader.ReadAsync())
                            {
                                if (book == null)
                                {
                                    book = new Book()
                                    {
                                        ID = reader.GetInt32("ID"),
                                        Title = reader.GetString("Title"),
                                        Description = reader.GetString("Description"),
                                        PageCount = reader.GetInt32("PageCount"),
                                        Publisher = reader.GetString("Publisher"),
                                        PubDate = reader.GetDateTime("PubDate"),
                                        Language = reader.GetString("Language"),
                                        ISBN = reader.GetString("ISBN"),
                                        Format = new BookFormat()
                                        {
                                            ID = reader.GetInt32("BookFormatID"),
                                            Name = reader.GetString("BookFormatName")
                                        },
                                        CoverFilePath = reader.IsDBNull("CoverFilePath") ? "" : reader.GetString("CoverFilePath"),
                                        Authors = new List<User>(),
                                        Genres = new List<Genre>(),
                                        Reviews = new List<Review>()
                                    };
                                }

                                if (!reader.IsDBNull("AuthorID"))
                                {
                                    int authorID = reader.GetInt32("AuthorID");
                                    if (!authors.Any(a => a.ID == authorID))
                                    {
                                        var author = new User()
                                        {
                                            ID = authorID,
                                            FirstName = reader.IsDBNull("AuthorFirstName") ? "" : reader.GetString("AuthorFirstName"),
                                            MiddleNames = reader.IsDBNull("AuthorMiddleNames") ? "" : reader.GetString("AuthorMiddleNames"),
                                            LastName = reader.IsDBNull("AuthorLastName") ? "" : reader.GetString("AuthorLastName")
                                        };
                                        authors.Add(author);
                                    }
                                }

                                if (!reader.IsDBNull("GenreID"))
                                {
                                    int genreID = reader.GetInt32("GenreID");
                                    if (!genres.Any(g => g.ID == genreID))
                                    {
                                        var genre = new Genre()
                                        {
                                            ID = genreID,
                                            Name = reader.GetString("GenreName")
                                        };
                                        genres.Add(genre);
                                    }
                                }

                                if (!reader.IsDBNull("ReviewID"))
                                {
                                    int reviewID = reader.GetInt32("ReviewID");
                                    if (!reviews.Any(r => r.ID == reviewID))
                                    {
                                        var review = new Review()
                                        {
                                            ID = reviewID,
                                            BookRating = reader.GetInt32("BookRating"),
                                            Title = reader.IsDBNull("ReviewTitle") ? "" : reader.GetString("ReviewTitle"),
                                            Body = reader.IsDBNull("ReviewBody") ? "" : reader.GetString("ReviewBody"),
                                            PostDate = reader.GetDateTime("PostDate"),
                                            UpvoteCount = reader.GetInt32("UpvoteCount"),
                                            DownvoteCount = reader.GetInt32("DownvoteCount"),
                                            Poster = new User()
                                            {
                                                ID = reader.GetInt32("ReviewerID"),
                                                Username = reader.GetString("ReviewerUsername"),
                                                ProfilePictureFilePath = reader.IsDBNull("ReviewerProfilePicture") ? "" : reader.GetString("ReviewerProfilePicture")
                                            }
                                        };
                                        reviews.Add(review);
                                    }
                                }
                            }

                            if (book != null)
                            {
                                book.Authors = authors;
                                book.Genres = genres;
                                book.Reviews = reviews;
                            }

                            return book;
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

        public async Task<List<Book>> GetAllBooksAsync()
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    SELECT Books.ID, Title, Description, PageCount, Publisher, PubDate, Language, ISBN,
                    BookFormatID, BookFormats.Name AS BookFormatName, CoverFilePath
                    FROM Books
                    INNER JOIN BookFormats
                    ON Books.BookFormatID = BookFormats.ID; ";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    try
                    {
                        List<Book> _books = [];

                        await connection.OpenAsync();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                Book book = new Book()
                                {
                                    ID = reader.GetInt32("ID"),
                                    Title = reader.GetString("Title"),
                                    Description = reader.GetString("Description"),
                                    PageCount = reader.GetInt32("PageCount"),
                                    Publisher = reader.GetString("Publisher"),
                                    PubDate = reader.GetDateTime("PubDate"),
                                    Language = reader.GetString("Language"),
                                    ISBN = reader.GetString("ISBN"),
                                    Format = new BookFormat()
                                    {
                                        ID = reader.GetInt32("BookFormatID"),
                                        Name = reader.GetString("BookFormatName")
                                    },
                                    CoverFilePath = reader.IsDBNull("CoverFilePath") ? "" : reader.GetString("CoverFilePath")
                                };
                                _books.Add(book);
                            }
                        }

                        if (_books.Count > 0) { return _books; }
                        else { return []; }
                    }
                    catch (SqlException ex)
                    {
                        // Handle SQL exceptions (e.g., query syntax errors)
                        throw new IOException("Failed to retrieve the Book.", ex);
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

        public async Task<List<Book>> GetAllBooksWithDetailsAsync()
        {
            var books = new Dictionary<int, Book>();

            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    SELECT 
                        b.ID AS BookID, b.Title, b.Description, b.PageCount, b.Publisher, b.PubDate, b.CoverFilePath,
                        a.ID AS AuthorID, a.FirstName, a.MiddleNames, a.LastName,
                        g.ID AS GenreID, g.Name AS GenreName,
                        r.ID AS ReviewID, r.BookRating, r.UserID
                    FROM 
                        Books b
                    LEFT JOIN 
                        Books_Authors ba ON b.ID = ba.BookID
                    LEFT JOIN 
                        Users a ON ba.UserID = a.ID
                    LEFT JOIN 
                        Books_Genres bg ON b.ID = bg.BookID
                    LEFT JOIN 
                        Genres g ON bg.GenreID = g.ID
                    LEFT JOIN 
                        Reviews r ON b.ID = r.BookID;";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    try
                    {
                        await connection.OpenAsync();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                int bookID = reader.GetInt32("BookID");
                                if (!books.TryGetValue(bookID, out var book))
                                {
                                    book = new Book()
                                    {
                                        ID = bookID,
                                        Title = reader.GetString("Title"),
                                        Description = reader.GetString("Description"),
                                        PageCount = reader.GetInt32("PageCount"),
                                        Publisher = reader.GetString("Publisher"),
                                        PubDate = reader.GetDateTime("PubDate"),
                                        CoverFilePath = reader.IsDBNull("CoverFilePath") ? "" : reader.GetString("CoverFilePath"),
                                        Authors = new List<User>(),
                                        Genres = new List<Genre>(),
                                        Reviews = new List<Review>()
                                    };
                                    books.Add(bookID, book);
                                }

                                // Only add unique authors to the book
                                if (!reader.IsDBNull("AuthorID"))
                                {
                                    int authorID = reader.GetInt32("AuthorID");
                                    if (!book.Authors.Any(a => a.ID == authorID))
                                    {
                                        book.Authors.Add(new User()
                                        {
                                            ID = authorID,
                                            FirstName = reader.IsDBNull("FirstName") ? "" : reader.GetString("FirstName"),
                                            MiddleNames = reader.IsDBNull("MiddleNames") ? "" : reader.GetString("MiddleNames"),
                                            LastName = reader.IsDBNull("LastName") ? "" : reader.GetString("LastName")
                                        });
                                    }
                                }

                                // Only add unique genres to the book
                                if (!reader.IsDBNull("GenreID"))
                                {
                                    int genreID = reader.GetInt32("GenreID");
                                    if (!book.Genres.Any(g => g.ID == genreID))
                                    {
                                        book.Genres.Add(new Genre()
                                        {
                                            ID = genreID,
                                            Name = reader.IsDBNull("GenreName") ? "" : reader.GetString("GenreName")
                                        });
                                    }
                                }

                                // Only add unique reviews to the book
                                if (!reader.IsDBNull("ReviewID"))
                                {
                                    int reviewID = reader.GetInt32("ReviewID");
                                    if (!book.Reviews.Any(r => r.ID == reviewID))
                                    {
                                        book.Reviews.Add(new Review()
                                        {
                                            ID = reviewID,
                                            BookRating = reader.GetInt32("BookRating")
                                        });
                                    }
                                }
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Handle SQL exceptions (e.g., query syntax errors)
                        throw new IOException("Failed to retrieve the list of books with details.", ex);
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
            return books.Values.ToList();
        }

        #region Pagination queries
        public async Task<int> GetTotalBooksCountAsync(string searchQuery = null)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    SELECT 
                        COUNT(DISTINCT b.ID) AS TotalBooks
                    FROM 
                        Books AS b
                    LEFT JOIN
                        Books_Authors AS ba ON b.ID = ba.BookID
                    LEFT JOIN
                        Users AS u ON ba.UserID = u.ID
                    WHERE
                        (@SearchQuery IS NULL OR 
	                    LOWER(b.Title) LIKE '%' + LOWER(@SearchQuery) + '%' OR 
	                    LOWER(b.ISBN) LIKE '%' + LOWER(@SearchQuery) + '%' OR 
	                    LOWER(u.FirstName) LIKE '%' + LOWER(@SearchQuery) + '%' OR
	                    LOWER(u.MiddleNames) LIKE '%' + LOWER(@SearchQuery) + '%' OR
	                    LOWER(u.LastName) LIKE '%' + LOWER(@SearchQuery) + '%');";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@SearchQuery", (object)searchQuery ?? DBNull.Value);

                    try
                    {
                        await connection.OpenAsync();
                        return (int)await command.ExecuteScalarAsync();
                    }
                    catch (SqlException ex)
                    {
                        // Handle SQL exceptions (e.g., query syntax errors)
                        throw new IOException("Failed to retrieve the total number of Books.", ex);
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

        public async Task<List<Book>> GetBooksByPageAsync(int pageNumber, int pageSize, string searchQuery = null)
        {
            var books = new Dictionary<int, Book>();

            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    WITH PaginatedBooks AS (
                        SELECT 
                            b.Id, b.Title, b.Description, b.PageCount, b.Publisher, b.PubDate, b.Language, b.ISBN, b.CoverFilePath
                        FROM 
                            Books b
                        LEFT JOIN 
                            Books_Authors ba ON b.Id = ba.BookId
                        LEFT JOIN 
                            Users a ON ba.UserID = a.Id
	                    LEFT JOIN
		                    Books_Genres AS bg ON b.ID = bg.BookID
	                    LEFT JOIN
		                    Genres AS g ON bg.GenreID = g.ID
                        WHERE 
                            @SearchQuery IS NULL OR 
                            LOWER(b.Title) LIKE '%' + LOWER(@SearchQuery) + '%' OR 
                            LOWER(b.ISBN) LIKE '%' + LOWER(@SearchQuery) + '%' OR 
                            LOWER(a.FirstName) LIKE '%' + LOWER(@SearchQuery) + '%' OR 
                            LOWER(a.MiddleNames) LIKE '%' + LOWER(@SearchQuery) + '%' OR 
                            LOWER(a.LastName) LIKE '%' + LOWER(@SearchQuery) + '%'
                        GROUP BY 
                            b.Id, b.Title, b.Description, b.PageCount, b.Publisher, b.PubDate, b.Language, b.ISBN, b.CoverFilePath
                        ORDER BY 
                            b.Title
                        OFFSET 0 ROWS
                        FETCH NEXT 10 ROWS ONLY
                    )
                    SELECT 
                        pb.Id AS BookID, pb.Title, pb.Description, pb.PageCount, pb.Publisher, pb.PubDate, pb.Language, pb.ISBN, pb.CoverFilePath, 
                        a.Id AS AuthorId, a.FirstName, a.MiddleNames, a.LastName,
                        r.Id AS ReviewId, r.BookRating,
	                    g.ID AS GenreID, g.Name AS GenreName
                    FROM 
                        PaginatedBooks pb
                    LEFT JOIN 
                        Books_Authors ba ON pb.Id = ba.BookId
                    LEFT JOIN 
                        Users a ON ba.UserID = a.Id
                    LEFT JOIN 
                        Reviews r ON pb.Id = r.BookId
                    LEFT JOIN
	                    Books_Genres AS bg ON pb.ID = bg.BookID
                    LEFT JOIN
	                    Genres AS g ON bg.GenreID = g.ID
                    ORDER BY
                        BookID;";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@Offset", (pageNumber - 1) * pageSize);
                    command.Parameters.AddWithValue("@PageSize", pageSize);
                    command.Parameters.AddWithValue("@SearchQuery", (object)searchQuery ?? DBNull.Value);

                    try
                    {
                        await connection.OpenAsync();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                int bookID = reader.GetInt32("BookID");
                                if (!books.ContainsKey(bookID))
                                {
                                    books[bookID] = new Book()
                                    {
                                        ID = bookID,
                                        Title = reader.GetString("Title"),
                                        Description = reader.GetString("Description"),
                                        PageCount = reader.GetInt32("PageCount"),
                                        Publisher = reader.GetString("Publisher"),
                                        PubDate = reader.GetDateTime("PubDate"),
                                        Language = reader.GetString("Language"),
                                        ISBN = reader.GetString("ISBN"),
                                        CoverFilePath = reader.IsDBNull("CoverFilePath") ? "" : reader.GetString("CoverFilePath)"),
                                        Authors = new List<User>(),
                                        Reviews = new List<Review>(),
                                        Genres = new List<Genre>()
                                    };
                                }

                                var book = books[bookID];

                                // Only add unique authors to the book
                                if (!reader.IsDBNull("AuthorID"))
                                {
                                    int authorID = reader.GetInt32("AuthorID");
                                    if (!book.Authors.Any(a => a.ID == authorID))
                                    {
                                        book.Authors.Add(new User()
                                        {
                                            ID = authorID,
                                            FirstName = reader.IsDBNull("FirstName") ? "" : reader.GetString("FirstName"),
                                            MiddleNames = reader.IsDBNull("MiddleNames") ? "" : reader.GetString("MiddleNames"),
                                            LastName = reader.IsDBNull("LastName") ? "" : reader.GetString("LastName")
                                        });
                                    }
                                }


                                // Only add unique reviews to the book
                                if (!reader.IsDBNull("ReviewID"))
                                {
                                    int reviewID = reader.GetInt32("ReviewID");
                                    if (!book.Reviews.Any(r => r.ID == reviewID))
                                    {
                                        book.Reviews.Add(new Review()
                                        {
                                            ID = reviewID,
                                            BookRating = reader.GetInt32("BookRating")
                                        });
                                    }
                                }

                                // Only add unique genres to the book
                                if (!reader.IsDBNull("GenreID"))
                                {
                                    int genreID = reader.GetInt32("GenreID");
                                    if (!book.Genres.Any(g => g.ID == genreID))
                                    {
                                        book.Genres.Add(new Genre()
                                        {
                                            ID = genreID,
                                            Name = reader.GetString("GenreName")
                                        });
                                    }
                                }
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Handle SQL exceptions (e.g., query syntax errors)
                        throw new IOException("Failed to retrieve the paginated list of books due to a SQL exception.", ex);
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
            return new List<Book>(books.Values);
        }
        #endregion

        public async Task<List<User>> GetAuthorsForBookAsync(int bookID)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    SELECT Users.ID as UserID, Users.FirstName, Users.MiddleNames, Users.LastName
                    FROM Books
                    INNER JOIN Books_Authors
                    ON Books.ID = Books_Authors.BookID
                    INNER JOIN Users
                    ON Books_Authors.UserID = Users.ID
                    WHERE Books.ID = @BookID; ";

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
                                };
                                authors.Add(author);
                            }
                        }

                        if (authors.Count > 0) { return authors; }
                        else { return []; }
                    }
                    catch (SqlException ex)
                    {
                        // Handle SQL exceptions (e.g., query syntax errors)
                        throw new IOException("Failed to retrieve the Authors.", ex);
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

        public async Task<List<Genre>> GetGenresForBookAsync(int bookID)
        {
            List<Genre> genres = new List<Genre>();
            string query = @"
                SELECT 
                    Genres.* 
                FROM 
                    Genres 
                INNER JOIN 
                    Books_Genres ON Genres.ID = Books_Genres.GenreID 
                WHERE 
                    Books_Genres.BookID = @BookID";

            using (SqlConnection connection = OpenConnection())
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@BookID", bookID);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Genre genre = new Genre
                            {
                                // Assuming Genre has ID and Name properties. Adjust according to your actual Genre class.
                                ID = reader.GetInt32(reader.GetOrdinal("ID")),
                                Name = reader.GetString(reader.GetOrdinal("Name"))
                            };
                            genres.Add(genre);
                        }
                    }
                }
            }
            return genres;
        }

        public async Task<int> GetMaxPageCountAsync()
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    SELECT MAX(PageCount) AS MaxPageCount
                    FROM Books; ";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    try
                    {
                        await connection.OpenAsync();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.Read())
                            {
                                return reader.GetInt32("MaxPageCount");
                            }
                            else { return 0; }
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Handle SQL exceptions (e.g., query syntax errors)
                        throw new IOException("Failed to retrieve the maximum PageCount.", ex);
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

        public async Task UpdateBookAsync(Book book)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    UPDATE Books
                    SET Title = @Title, Description = @Description, PageCount = @PageCount, Publisher = @Publisher,
                    PubDate = @PubDate, Language = @Language, ISBN = @ISBN, BookFormatID = @BookFormatID
                    WHERE ID = @ID; ";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@ID", book.ID);
                    command.Parameters.AddWithValue("@Title", book.Title);
                    command.Parameters.AddWithValue("@Description", book.Description);
                    command.Parameters.AddWithValue("@PageCount", book.PageCount);
                    command.Parameters.AddWithValue("@Publisher", book.Publisher);
                    command.Parameters.AddWithValue("@PubDate", book.PubDate);
                    command.Parameters.AddWithValue("@Language", book.Language);
                    command.Parameters.AddWithValue("@ISBN", book.ISBN);
                    command.Parameters.AddWithValue("@BookFormatID", book.Format.ID);

                    try
                    {
                        await connection.OpenAsync();
                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        if (rowsAffected == 0)
                        {
                            throw new Exception("No rows were inserted. The Book may not have been updated.");
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Handle SQL exceptions (e.g., query syntax errors, constraint violations)
                        throw new IOException("Failed to update the Book.", ex);
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

        public async Task UpdateAuthorsForBookAsync(int bookID, List<int> authorIDs)
        {
            using (SqlConnection connection = OpenConnection())
            {
                await connection.OpenAsync();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Delete existing authors for the book
                        string deleteQuery = @"
                            DELETE FROM Books_Authors 
                            WHERE BookID = @BookID; ";

                        using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection, transaction))
                        {
                            deleteCommand.Parameters.AddWithValue("@BookID", bookID);
                            await deleteCommand.ExecuteNonQueryAsync();
                        }

                        // Insert new authors for the book
                        string insertQuery = @"
                            INSERT INTO Books_Authors (BookID, UserID) 
                            VALUES (@BookID, @UserID); ";

                        foreach (var authorID in authorIDs)
                        {
                            using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection, transaction))
                            {
                                insertCommand.Parameters.AddWithValue("@BookID", bookID);
                                insertCommand.Parameters.AddWithValue("@UserID", authorID);
                                await insertCommand.ExecuteNonQueryAsync();
                            }
                        }

                        // Commit the transaction
                        transaction.Commit();
                    }

                    catch (SqlException ex)
                    {
                        transaction.Rollback();
                        // Handle SQL exceptions (e.g., query syntax errors, constraint violations)
                        throw new IOException("Failed to update the Book.", ex);
                    }
                    catch (InvalidOperationException ex)
                    {
                        transaction.Rollback();
                        // Handle exceptions related to the connection (e.g., not open)
                        throw new IOException("Failed to open the database connection.", ex);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        // Handle any other exceptions
                        throw new IOException("An unexpected error occurred.", ex);
                    }
                }
            }
        }

        public async Task UpdateGenresForBookAsync(int bookID, List<int> genreIDs)
        {
            using (SqlConnection connection = OpenConnection())
            {
                await connection.OpenAsync();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Delete existing genres for the book
                        string deleteQuery = @"
                            DELETE FROM Books_Genres 
                            WHERE BookID = @BookID; ";

                        using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection, transaction))
                        {
                            deleteCommand.Parameters.AddWithValue("@BookID", bookID);
                            await deleteCommand.ExecuteNonQueryAsync();
                        }

                        // Insert new genres for the book
                        string insertQuery = @"
                            INSERT INTO Books_Genres (BookID, GenreID) 
                            VALUES (@BookID, @GenreID); ";

                        foreach (var genreID in genreIDs)
                        {
                            using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection, transaction))
                            {
                                insertCommand.Parameters.AddWithValue("@BookID", bookID);
                                insertCommand.Parameters.AddWithValue("@GenreID", genreID);
                                await insertCommand.ExecuteNonQueryAsync();
                            }
                        }

                        // Commit the transaction
                        transaction.Commit();
                    }

                    catch (SqlException ex)
                    {
                        transaction.Rollback();
                        // Handle SQL exceptions (e.g., query syntax errors, constraint violations)
                        throw new IOException("Failed to update the Book.", ex);
                    }
                    catch (InvalidOperationException ex)
                    {
                        transaction.Rollback();
                        // Handle exceptions related to the connection (e.g., not open)
                        throw new IOException("Failed to open the database connection.", ex);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        // Handle any other exceptions
                        throw new IOException("An unexpected error occurred.", ex);
                    }
                }
            }
        }

        public async Task DeleteBookByIDAsync(int id)
        {
            // I'm not sure if I should allow full deletion of a book;
            // It might be better to keep it archived or something
            throw new NotImplementedException();
        }
    }
}
 