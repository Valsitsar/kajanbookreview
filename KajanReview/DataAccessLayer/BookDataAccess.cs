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

        public void CreateBook(Book newBook)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery =
                    @"INSERT INTO Books (Title, Description, PageCount, Publisher, PubDate, Language, ISBN, BookFormatID, CoverFilePath)
                    VALUES (@Title, @Description, @NoOfPages, @Publisher, @PubDate, @Language, @ISBN, @BookFormatID, @CoverFilePath);";
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
                    command.Parameters.AddWithValue("@CoverFilePath", newBook.CoverFilePath);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {

                        throw new IOException("Failed to create the Book.", ex);
                    }
                }
            }
        }

        public Book GetBookByID(int bookID)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery =
                    @"SELECT Books.ID, Title, Description, PageCount, Publisher, PubDate, Language, ISBN,
                    BookFormatID, BookFormats.Name AS BookFormatName, CoverFilePath,
                    FROM Books
                    INNER JOIN BookFormats
                    ON Books.BookFormatID = BookFormats.ID
                    WHERE Books.ID = @ID; ";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@ID", bookID);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
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
                                    Name = reader.GetString("FormatName")
                                },
                                CoverFilePath = reader.GetString("CoverFilePath")
                            };
                            return book;
                        }
                        else { return null; }
                    }
                    catch (SqlException ex)
                    {
                        throw new IOException("Failed to get the Book.", ex);
                    }
                }
            }
        }

        public List<Book> GetAllBooks()
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery =
                    @"SELECT Books.ID, Title, Description, PageCount, Publisher, PubDate, Language, ISBN,
                    BookFormatID, BookFormats.Name AS BookFormatName, CoverFilePath,
                    FROM Books
                    INNER JOIN BookFormats
                    ON Books.BookFormatID = BookFormats.ID; ";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    try
                    {
                        List<Book> _books = [];

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

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
                                    Name = reader.GetString("FormatName")
                                },
                                CoverFilePath = reader.GetString("CoverFilePath")
                            };
                            _books.Add(book);
                        }

                        if (_books.Count > 0) { return _books; }
                        else { return null; }
                    }
                    catch (SqlException ex)
                    {
                        throw new IOException("Failed to get Books.", ex);
                    }
                }
            }
        }

        public void UpdateBook(Book book)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery =
                    @"UPDATE Books
                    SET Title = @Title, Description = @Description, PageCount = @PageCount, Publisher = @Publisher,
                    PubDate = @PubDate, Language = @Language, ISBN = @ISBN, BookFormatID = @Format.ID, CoverFilePath = @CoverFilePath
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
                    command.Parameters.AddWithValue("@CoverFilePath", book.CoverFilePath);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        throw new IOException("Failed to update the Book.", ex);
                    }
                }
            }
        }

        public void DeleteBookByID(int id)
        {
            // I'm not sure if I should allow full deletion of a book;
            // It might be better to keep it archived or something
            throw new NotImplementedException();
        }
    }
}
