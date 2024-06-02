using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessLayer
{
    public class BookFormatDataAccess : DataAccessBase, IBookFormatDataAccess
    {
        public void CreateBookFormat(BookFormat newBookFormat)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    INSERT INTO BookFormats (Name) 
                    VALUES (@Name); ";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@Name", newBookFormat.Name);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        throw new IOException("Failed to create the BookFormat.", ex);
                    }
                }
            }
        }

        public BookFormat GetBookFormatByID(int bookFormatId)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    SELECT ID, Name 
                    FROM BookFormats 
                    WHERE ID = @ID; ";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@ID", bookFormatId);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            BookFormat bookFormat = new BookFormat()
                            {
                                ID = reader.GetInt32("ID"),
                                Name = reader.GetString("Name")
                            };
                            return bookFormat;
                        }
                        else { return new BookFormat(); }
                    }
                    catch (Exception ex)
                    {
                        throw new IOException("Failed to get the BookFormat.", ex);
                    }
                }
            }
        }

        public List<BookFormat> GetAllBookFormats()
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    SELECT ID, Name 
                    FROM BookFormats; ";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    try
                    {
                        List<BookFormat> _bookFormats = [];

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            BookFormat bookFormat = new BookFormat()
                            {
                                ID = reader.GetInt32("ID"),
                                Name = reader.GetString("Name")
                            };
                            _bookFormats.Add(bookFormat);
                        }
                        if (_bookFormats.Count > 0) { return _bookFormats; }
                        else { return []; }
                    }
                    catch (Exception ex)
                    {
                        throw new IOException("Failed to get the BookFormats.", ex);
                    }
                }
            }
        }

        public void UpdateBookFormat(BookFormat bookFormat)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    UPDATE BookFormats 
                    SET Name = @Name 
                    WHERE ID = @ID; ";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@ID", bookFormat.ID);
                    command.Parameters.AddWithValue("@Name", bookFormat.Name);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        throw new IOException("Failed to update the BookFormat.", ex);
                    }
                }
            }
        }

        public void DeleteBookFormatByID(int bookFormatId)
        {
            // I'm not sure if I should allow full deletion of a Book Format;
            // It might be better to keep it archived or something
            throw new NotImplementedException();
        }
    }
}
