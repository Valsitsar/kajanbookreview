using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Entities;

namespace BusinessLogicLayer.Interfaces
{
    public interface IUserDataAccess
    {
        public Task CreateUserAsync(UserDTO newUser, string hashedPassword, string salt);
        public Task<UserDTO> GetUserByIDAsync(int userID);
        public Task<User> GetUserByUsernameForLoginAsync(string username); //not UserDTO because it handles passwords
        public Task<User> GetUserByEmailForLoginAsync(string email); //not UserDTO because it handles passwords
        public Task<List<UserDTO>> GetAllUsersAsync();
        public Task<(string? hashedPassword, string? salt)> GetPasswordHashAndSaltByUsernameAsync(string username);
        public Task<(string? hashedPassword, string? salt)> GetPasswordHashAndSaltByUserIDAsync(int userID);
        public Task UpdatePasswordHashAndSaltByUserIDAsync(int userID, string hashedPassword, string salt);
        public Task UpdateUserAsync(UserDTO newUserDTO);
        public Task DeleteUserByIDAsync(int userID);
    }
}
