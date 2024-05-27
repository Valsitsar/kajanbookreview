using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessLayer
{
    public class ReviewDataAccess : DataAccessBase, IReviewDataAccess
    {
        public void CreateReview(Review newReview)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery =
                    @"INSERT INTO Reviews (UserID, Body, UpvoteCount, DownvoteCount, PostDate, BookRating, BookID) 
                    VALUES (@UserID, @Body, @UpvoteCount, @DownvoteCount, @PostDate, @BookRating, @BookID); ";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@UserID", newReview.Poster);
                    command.Parameters.AddWithValue("@Body", newReview.Body);
                    command.Parameters.AddWithValue("@UpvoteCount", newReview.UpvoteCount);
                    command.Parameters.AddWithValue("@DownvoteCount", newReview.DownvoteCount);
                    command.Parameters.AddWithValue("@PostDate", newReview.PostDate);
                    command.Parameters.AddWithValue("@BookRating", newReview.BookRating);
                    command.Parameters.AddWithValue("@BookID", newReview.SourceBook.ID);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {

                        throw new IOException("Failed to create the Review.", ex);
                    }
                }
            }
        }

        public Review GetReviewByID(int reviewID)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery =
                    @"SELECT Reviews.ID as ReviewID, Users.ID AS PosterID, Users.Username AS PosterUsername,
                    Body, UpvoteCount, DownvoteCount, PostDate, BookRating,
                    Books.ID AS SourceBookID, Books.Title AS SourceBookTitle
                    FROM Users
                    INNER JOIN Reviews
                    ON Users.ID = Reviews.UserID
                    INNER JOIN Books
                    ON Reviews.BookID = Books.ID
                    WHERE Reviews.ID = @ID;";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@ID", reviewID);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            string body = !reader.IsDBNull("Body") ? reader.GetString("Body") : "";

                            Review review = new Review()
                            {
                                ID = reader.GetInt32("ID"),
                                Poster = new User()
                                {
                                    ID = reader.GetInt32("PosterID"),
                                    Username = reader.GetString("PosterUsername")
                                },
                                Body = body,
                                UpvoteCount = reader.GetInt32("UpvoteCount"),
                                DownvoteCount = reader.GetInt32("DownvoteCount"),
                                PostDate = reader.GetDateTime("PostDate"),
                                BookRating = reader.GetInt32("BookRating"),
                                SourceBook = new Book()
                                {
                                    ID = reader.GetInt32("SourceBookID"),
                                    Title = reader.GetString("SourceBookTitle")
                                }
                            };
                            return review;
                        }
                        else { return null; }
                    }
                    catch (SqlException ex)
                    {
                        throw new IOException("Failed to get the Review.", ex);
                    }
                }
            }
        }

        public List<Review> GetAllReviews()
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery =
                    @"SELECT Reviews.ID as ReviewID, Users.ID AS PosterID, Users.Username AS PosterUsername,
                    Body, UpvoteCount, DownvoteCount, PostDate, BookRating,
                    Books.ID AS SourceBookID, Books.Title AS SourceBookTitle
                    FROM Users
                    INNER JOIN Reviews
                    ON Users.ID = Reviews.UserID
                    INNER JOIN Books
                    ON Reviews.BookID = Books.ID";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    try
                    {
                        List<Review> _reviews = [];

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            string body = !reader.IsDBNull("Body") ? reader.GetString("Body") : "";

                            Review review = new Review()
                            {
                                ID = reader.GetInt32("ID"),
                                Poster = new User()
                                {
                                    ID = reader.GetInt32("PosterID"),
                                    Username = reader.GetString("PosterUsername")
                                },
                                Body = body,
                                UpvoteCount = reader.GetInt32("UpvoteCount"),
                                DownvoteCount = reader.GetInt32("DownvoteCount"),
                                PostDate = reader.GetDateTime("PostDate"),
                                BookRating = reader.GetInt32("BookRating"),
                                SourceBook = new Book()
                                {
                                    ID = reader.GetInt32("SourceBookID"),
                                    Title = reader.GetString("SourceBookTitle")
                                }
                            };
                            _reviews.Add(review);
                        }

                        if (_reviews.Count > 0) { return _reviews; }
                        else { return null; }
                    }
                    catch (SqlException ex)
                    {
                        throw new IOException("Failed to get the Reviews.", ex);
                    }
                }
            }
        }

        public void UpdateReview(Review review)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery =
                    @"UPDATE Reviews
                    SET UserID = @UserID, Body = @Body, UpvoteCount = @UpvoteCount, DownvoteCount = @DownvoteCount, 
                    PostDate = @PostDate, BookRating = @BookRating, BookID = @BookID)
                    WHERE ID = @ID;";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@ID", review.ID);
                    command.Parameters.AddWithValue("@UserID", review.Poster.ID);
                    command.Parameters.AddWithValue("@Body", review.Body);
                    command.Parameters.AddWithValue("@UpvoteCount", review.UpvoteCount);
                    command.Parameters.AddWithValue("@DownvoteCount", review.DownvoteCount);
                    command.Parameters.AddWithValue("@PostDate", review.PostDate);
                    command.Parameters.AddWithValue("@BookRating", review.BookRating);
                    command.Parameters.AddWithValue("@BookID", review.SourceBook.ID);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {

                        throw new IOException("Failed to update the Review.", ex);
                    }
                }
            }
        }

        public void DeleteReviewByID(int reviewID)
        {
            // I'm not sure if I should allow full deletion of a user;
            // It might be better to keep it archived or something
            throw new NotImplementedException();
        }
    }
}
