using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessLayer
{
    public class CommentDataAccess : DataAccessBase, ICommentDataAccess
    {
        public async Task CreateCommentAsync(Comment newComment)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    INSERT INTO Comments (UserID, Body, UpvoteCount, DownvoteCount, PostDate, SourceReviewID) 
                    VALUES (@UserID, @Body, @UpvoteCount, @DownvoteCount, @PostDate, @SourceReviewID); ";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@UserID", newComment.Poster);
                    command.Parameters.AddWithValue("@Body", newComment.Body);
                    command.Parameters.AddWithValue("@UpvoteCount", newComment.UpvoteCount);
                    command.Parameters.AddWithValue("@DownvoteCount", newComment.DownvoteCount);
                    command.Parameters.AddWithValue("@PostDate", newComment.PostDate);
                    command.Parameters.AddWithValue("@SourceReviewID", newComment.SourceReview.ID);

                    try
                    {
                        await connection.OpenAsync();
                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        if (rowsAffected == 0)
                        {
                            throw new Exception("No rows were inserted. The Comment may not have been created.");
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Handle SQL exceptions (e.g., query syntax errors, constraint violations)
                        throw new IOException("Failed to update the Comment.", ex);
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

        public async Task<Comment> GetCommentByIDAsync(int commentID)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    SELECT Comments.ID as CommentID, Comments_Users.ID AS PosterID, Comments_Users.Username AS PosterUsername, 
                    Comments.Body, Comments.UpvoteCount, Comments.DownvoteCount, Comments.PostDate, 
                    Reviews.ID AS SourceReviewID, Reviews.Body AS SourceReviewBody, 
                    Reviews.UserID AS SourceReviewPosterID, Reviews_Users.Username AS SourceReviewPosterUsername

                    FROM Users AS Comments_Users
                    INNER JOIN Comments
                    ON Comments_Users.ID = Comments.UserID
                    INNER JOIN Reviews
                    ON Comments.SourceReviewID = Reviews.ID
                    INNER JOIN Users AS Reviews_Users
                    ON Reviews.UserID = Reviews_Users.ID; 
                    WHERE Comments.ID = @ID";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@ID", commentID);

                    try
                    {
                        await connection.OpenAsync();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.Read())
                            {
                                string body = !reader.IsDBNull("Body") ? reader.GetString("Body") : "";

                                Comment comment = new Comment()
                                {
                                    ID = reader.GetInt32("CommentID"),
                                    Poster = new User()
                                    {
                                        ID = reader.GetInt32("PosterID"),
                                        Username = reader.GetString("PosterUsername")
                                    },
                                    Body = body,
                                    UpvoteCount = reader.GetInt32("Comments.UpvoteCount"),
                                    DownvoteCount = reader.GetInt32("Comments.DownvoteCount"),
                                    PostDate = reader.GetDateTime("Comments.PostDate"),
                                    SourceReview = new Review()
                                    {
                                        ID = reader.GetInt32("SourceReviewID"),
                                        Body = reader.GetString("SourceReviewBody"),
                                        Poster = new User()
                                        {
                                            ID = reader.GetInt32("SourceReviewPosterID"),
                                            Username = reader.GetString("SourceReviewPosterUsername")
                                        }
                                    }
                                };
                                return comment;
                            }
                            else { return new Comment(); }
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Handle SQL exceptions (e.g., query syntax errors)
                        throw new IOException("Failed to retrieve the Comment.", ex);
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

        public async Task<List<Comment>> GetAllCommentsAsync()
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    SELECT Comments.ID as CommentID, Comments_Users.ID AS PosterID, Comments_Users.Username AS PosterUsername, 
                    Comments.Body, Comments.UpvoteCount, Comments.DownvoteCount, Comments.PostDate, 
                    Reviews.ID AS SourceReviewID, Reviews.Body AS SourceReviewBody, 
                    Reviews.UserID AS SourceReviewPosterID, Reviews_Users.Username AS SourceReviewPosterUsername

                    FROM Users AS Comments_Users
                    INNER JOIN Comments
                    ON Comments_Users.ID = Comments.UserID
                    INNER JOIN Reviews
                    ON Comments.SourceReviewID = Reviews.ID
                    INNER JOIN Users AS Reviews_Users
                    ON Reviews.UserID = Reviews_Users.ID; ";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    try
                    {
                        List<Comment> _comments = [];

                        await connection.OpenAsync();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                string body = !reader.IsDBNull("Body") ? reader.GetString("Body") : "";

                                Comment comment = new Comment()
                                {
                                    ID = reader.GetInt32("CommentID"),
                                    Poster = new User()
                                    {
                                        ID = reader.GetInt32("PosterID"),
                                        Username = reader.GetString("PosterUsername")
                                    },
                                    Body = body,
                                    UpvoteCount = reader.GetInt32("Comments.UpvoteCount"),
                                    DownvoteCount = reader.GetInt32("Comments.DownvoteCount"),
                                    PostDate = reader.GetDateTime("Comments.PostDate"),
                                    SourceReview = new Review()
                                    {
                                        ID = reader.GetInt32("SourceReviewID"),
                                        Body = reader.GetString("SourceReviewBody"),
                                        Poster = new User()
                                        {
                                            ID = reader.GetInt32("SourceReviewPosterID"),
                                            Username = reader.GetString("SourceReviewPosterUsername")
                                        }
                                    }
                                };
                                _comments.Add(comment);
                            }

                            if (_comments.Count > 0) { return _comments; }
                            else { return []; }
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Handle SQL exceptions (e.g., query syntax errors)
                        throw new IOException("Failed to retrieve the Comment.", ex);
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

        public async Task UpdateCommentAsync(Comment comment)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    UPDATE Comments
                    SET UserID = @UserID, Body = @Body, UpvoteCount = @UpvoteCount, DownvoteCount = @DownvoteCount, 
                    PostDate = @PostDate, SourceReviewID = @SourceReviewID)
                    WHERE ID = @ID;";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@ID", comment.ID);
                    command.Parameters.AddWithValue("@UserID", comment.Poster.ID);
                    command.Parameters.AddWithValue("@Body", comment.Body);
                    command.Parameters.AddWithValue("@UpvoteCount", comment.UpvoteCount);
                    command.Parameters.AddWithValue("@DownvoteCount", comment.DownvoteCount);
                    command.Parameters.AddWithValue("@PostDate", comment.PostDate);
                    command.Parameters.AddWithValue("@BookID", comment.SourceReview.ID);

                    try
                    {
                        await connection.OpenAsync();
                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        if (rowsAffected == 0)
                        {
                            throw new Exception("No rows were inserted. The Comment may not have been updated.");
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Handle SQL exceptions (e.g., query syntax errors, constraint violations)
                        throw new IOException("Failed to update the Comment.", ex);
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

        public async Task DeleteCommentByIDAsync(int commentID)
        {
            // I'm not sure if I should allow full deletion of a Comment;
            // It might be better to keep it archived or something
            throw new NotImplementedException();
        }
    }
}
