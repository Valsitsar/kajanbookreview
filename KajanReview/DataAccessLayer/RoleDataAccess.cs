using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessLayer
{
    public class RoleDataAccess : DataAccessBase, IRoleDataAccess
    {
        public async Task CreateRoleAsync(Role newRole)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    INSERT INTO Roles (Name) 
                    VALUES (@Name); ";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@Name", newRole.Name);

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

        public async Task<Role> GetRoleByIDAsync(int roleID)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    SELECT ID, Name 
                    FROM Roles 
                    WHERE ID = @ID; ";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@ID", roleID);

                    try
                    {
                        await connection.OpenAsync();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.Read())
                            {
                                Role role = new Role()
                                {
                                    ID = reader.GetInt32("ID"),
                                    Name = reader.GetString("Name")
                                };
                                return role;
                            }
                            else { return new Role(); }
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

        public async Task<List<Role>> GetAllRolesAsync()
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    SELECT ID, Name 
                    FROM Roles ";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    try
                    {
                        List<Role> _roles = [];

                        await connection.OpenAsync();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                Role role = new Role()
                                {
                                    ID = reader.GetInt32("ID"),
                                    Name = reader.GetString("Name")
                                };
                                _roles.Add(role);
                            }
                            if (_roles.Count > 0) { return _roles; }
                            else { return []; }
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

        public async Task UpdateRoleAsync(Role role)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    UPDATE Roles 
                    SET Name = @Name 
                    WHERE ID = @ID";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@ID", role.ID);
                    command.Parameters.AddWithValue("@Name", role.Name);

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

        public async Task DeleteRoleByIDAsync(int roleID)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery = @"
                    DELETE FROM Roles 
                    WHERE ID = @ID; ";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@ID", roleID);

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
    }
}
