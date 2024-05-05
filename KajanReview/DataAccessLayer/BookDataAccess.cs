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
    public class BookDataAccess : DataAccessBase, IBookDataAccess
    {
        public BookDataAccess() { }

        public void CreateBook(Book newBook)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery =
                    "INSERT INTO Books (Title, Description, NoOfPages, Publisher, PubDate, Language, ISBN) " +
                    "VALUES (@Title, @Description, @NoOfPages, @Publisher, @PubDate, @Language, @ISBN)";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@Title", newBook.Title);
                    command.Parameters.AddWithValue("@Description", newBook.Description);
                    command.Parameters.AddWithValue("@NoOfPages", newBook.NoOfPages);
                    command.Parameters.AddWithValue("@Publisher", newBook.Publisher);
                    command.Parameters.AddWithValue("@PubDate", newBook.PubDate);
                    command.Parameters.AddWithValue("@Language", newBook.Language);
                    command.Parameters.AddWithValue("@ISBN", newBook.ISBN);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        
                        throw new IOException("Failed to create the book.", ex);
                    }
                }
                
            }
        }

        public void DeleteBook(int id)
        {
            throw new NotImplementedException();
        }

        public Book GetBook(int id)
        {
            throw new NotImplementedException();
        }

        public List<Book> GetAllBooks()
        {
            throw new NotImplementedException();
        }

        public void UpdateBook(Book newBook)
        {
            throw new NotImplementedException();
        }
    }
}
