using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessLayer
{
    public class UserDataAccess : DataAccessBase, IUserDataAccess
    {
        public void CreateUser(User newUser, string hashedPassword, string salt)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    INSERT INTO Users (FirstName, MiddleNames, LastName, Username, Email, 
                    PhoneNumber, PasswordHash, Salt, ProfilePictureFilePath) 
                    VALUES (@FirstName, @MiddleNames, @LastName, @Username, @Email, 
                    @PhoneNumber, @PasswordHash, @Salt, @ProfilePictureFilePath); ";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", newUser.FirstName);
                    command.Parameters.AddWithValue("@MiddleNames", newUser.MiddleNames);
                    command.Parameters.AddWithValue("@LastName", newUser.LastName);
                    command.Parameters.AddWithValue("@Username", newUser.Username);
                    command.Parameters.AddWithValue("@Email", newUser.Email);
                    command.Parameters.AddWithValue("@PhoneNumber", newUser.PhoneNumber);
                    command.Parameters.AddWithValue("@PasswordHash", hashedPassword);
                    command.Parameters.AddWithValue("@Salt", salt);
                    command.Parameters.AddWithValue("@ProfilePictureFilePath", newUser.ProfilePictureFilePath);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {

                        throw new IOException("Failed to create the User.", ex);
                    }
                }
            }
        }

        public User GetUserByID(int userID)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    SELECT ID, FirstName, MiddleNames, LastName, Username, 
                    Email, PhoneNumber, ProfilePictureFilePath
                    FROM Users 
                    WHERE ID = @ID; ";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@ID", userID);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            string middleNames = !reader.IsDBNull("MiddleNames") ? reader.GetString("MiddleNames") : "";

                            User user = new User()
                            {
                                ID = reader.GetInt32("ID"),
                                FirstName = reader.GetString("FirstName"),
                                MiddleNames = middleNames,
                                LastName = reader.GetString("LastName"),
                                Username = reader.GetString("Username"),
                                Email = reader.GetString("Email"),
                                PhoneNumber = reader.GetString("PhoneNumber"),
                                ProfilePictureFilePath = reader.GetString("ProfilePictureFilePath")
                            };
                            return user;
                        }
                        else { return new User(); }
                    }
                    catch (SqlException ex)
                    {

                        throw new IOException("Failed to get the User.", ex);
                    }
                }
            }
        }

        public List<User> GetAllUsers()
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    SELECT ID, FirstName, MiddleNames, LastName, Username, 
                    Email, PhoneNumber, ProfilePictureFilePath
                    FROM Users; ";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    try
                    {
                        List<User> _users = [];

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            string middleNames = !reader.IsDBNull("MiddleNames") ? reader.GetString("MiddleNames") : "";

                            User user = new User()
                            {
                                ID = reader.GetInt32("ID"),
                                FirstName = reader.GetString("FirstName"),
                                MiddleNames = middleNames,
                                LastName = reader.GetString("LastName"),
                                Username = reader.GetString("Username"),
                                Email = reader.GetString("Email"),
                                PhoneNumber = reader.GetString("PhoneNumber"),
                                ProfilePictureFilePath = reader.GetString("ProfilePictureFilePath")
                            };
                            _users.Add(user);
                        }

                        return _users;
                    }
                    catch (SqlException ex)
                    {

                        throw new IOException("Failed to get the Users.", ex);
                    }
                }
            }
        }

        public void UpdateUser(User user)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    UPDATE Users 
                    SET FirstName = @FirstName, MiddleNames = @MiddleNames, LastName = @LastName, 
                    Username = @Username, Email = @Email, PhoneNumber = @PhoneNumber) 
                    WHERE ID = @ID; ";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@ID", user.ID);
                    command.Parameters.AddWithValue("@FirstName", user.FirstName);
                    command.Parameters.AddWithValue("@MiddleNames", user.MiddleNames);
                    command.Parameters.AddWithValue("@LastName", user.LastName);
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {

                        throw new IOException("Failed to update the User.", ex);
                    }
                }
            }
        }

        public void DeleteUserByID(int userID)
        {
            // I'm not sure if I should allow full deletion of a user;
            // It might be better to keep it archived or something
            throw new NotImplementedException();
        }

        public (string hashedPassword, string salt) GetPasswordAndSaltByUsername(string username)
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
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

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
                    catch (SqlException ex)
                    {

                        throw new IOException("Failed to get the User's PasswordHash and Salt.", ex);
                    }
                }
            }
        }

        public void UpdatePasswordAndSaltByUserID(int userID, string hashedPassword, string salt)
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
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {

                        throw new IOException("Failed to update the User's PasswordHash and Salt.", ex);
                    }
                }
            }
        }
    }
}
