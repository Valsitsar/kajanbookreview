using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Entities;

namespace BusinessLogicLayer.Interfaces
{
    public interface IUserManager
    {
        public void CreateUser(User newUser, string hashedPassword, string salt);
        public Task<UserDTO> GetUserByIDAsync(int userID);
        public User GetUserByUsernameForLogin(string username);
        public User GetUserByEmailForLogin(string email);
        public List<UserDTO> GetAllUsers();
        public (string? hashedPassword, string? salt) GetPasswordHashAndSaltByUsername(string username);
        public Task<(string? hashedPassword, string? salt)> GetPasswordHashAndSaltByUserIDAsync(int userID);
        public void UpdatePasswordHashAndSaltByUserID(int userID, string hashedPassword, string salt);
        public Task UpdateUserAsync(UserDTO newUserDTO);
        public void DeleteUserByID(int userID);
    }
}
