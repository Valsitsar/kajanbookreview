using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessLayer
{
    public class UserDataAccess : DataAccessBase, IUserDataAccess
    {
        public void CreateUser(User newUser)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    INSERT INTO Users (FirstName, MiddleNames, LastName, Username, Email, PhoneNumber, Password) 
                    VALUES (@FirstName, @MiddleNames, @LastName, @Username, @Email, @PhoneNumber, @Password); ";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", newUser.FirstName);
                    command.Parameters.AddWithValue("@MiddleNames", newUser.MiddleNames);
                    command.Parameters.AddWithValue("@LastName", newUser.LastName);
                    command.Parameters.AddWithValue("@Username", newUser.Username);
                    command.Parameters.AddWithValue("@Email", newUser.Email);
                    command.Parameters.AddWithValue("@PhoneNumber", newUser.PhoneNumber);
                    command.Parameters.AddWithValue("@Password", newUser.Password);

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
                    SELECT ID, FirstName, MiddleNames, LastName, Username, Email, PhoneNumber, Password 
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
                                Password = reader.GetString("Password")
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
                    SELECT ID, FirstName, MiddleNames, LastName, Username, Email, PhoneNumber, Password 
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
                                Password = reader.GetString("Password")
                            };
                            _users.Add(user);
                        }

                        if (_users.Count > 0) { return _users; }
                        else { return []; }
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
                    Username = @Username, Email = @Email, PhoneNumber = @PhoneNumber, Password = @Password) 
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
                    command.Parameters.AddWithValue("@Password", user.Password);

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
    }
}
