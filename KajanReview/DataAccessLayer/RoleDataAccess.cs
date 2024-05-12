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
    public class RoleDataAccess : DataAccessBase, IRoleDataAccess
    {
        public void CreateRole(Role newRole)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery =
                    @"INSERT INTO Roles (Name) 
                    VALUES (@Name); ";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@Name", newRole.Name);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        throw new IOException("Failed to create the Role.", ex);
                    }
                }
            }
        }

        public Role GetRoleByID(int roleID)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery =
                    @"SELECT ID, Name 
                    FROM Roles 
                    WHERE ID = @ID; ";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@ID", roleID);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            Role role = new Role()
                            {
                                ID = reader.GetInt32("ID"),
                                Name = reader.GetString("Name")
                            };
                            return role;
                        }
                        else { return null; }
                    }
                    catch (Exception ex)
                    {
                        throw new IOException("Failed to get the Role.", ex);
                    }
                }
            }
        }

        public List<Role> GetAllRoles()
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery =
                    @"SELECT ID, Name 
                    FROM Roles ";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    try
                    {
                        List<Role> _roles = [];

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

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
                        else { return null; }
                    }
                    catch (Exception ex)
                    {
                        throw new IOException("Failed to get the Roles.", ex);
                    }
                }
            }
        }

        public void UpdateRole(Role role)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery =
                    @"UPDATE Roles 
                    SET Name = @Name 
                    WHERE ID = @ID";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@ID", role.ID);
                    command.Parameters.AddWithValue("@Name", role.Name);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        throw new IOException("Failed to update the Role.", ex);
                    }
                }
            }
        }

        public void DeleteRoleByID(int roleID)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string sqlQuery =
                    @"DELETE FROM Roles 
                    WHERE ID = @ID; ";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@ID", roleID);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        throw new IOException("Failed to delete the Role.", ex);
                    }
                }
            }
        }
    }
}
