using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Entities;

namespace BusinessLogicLayer.Interfaces
{
    public interface IUserManager
    {
        public Task CreateUserAsync(UserDTO newUser, string hashedPassword, string salt);
        public Task CreateDefaultBookshelvesForUserAsync(int userID);
        public Task<int> GetLastUserID();
        public Task<UserDTO> GetUserByIDAsync(int userID);
        public Task<User> GetUserByUsernameForLoginAsync(string username); //not UserDTO because it handles passwords
        public Task<User> GetUserByEmailForLoginAsync(string email); //not UserDTO because it handles passwords
        public Task<List<User>> GetAllUsersAsync();
        public Task<List<Review>> GetReviewsByUserAsync(int userID);
        public Task<(List<Bookshelf>, List<int>)> GetBookshelfNamesAndCountsForUserAsync(int userID);
        public Task<List<Bookshelf>> GetBookshelvesForUserAsync(int userID);
        public Task<List<Book>> GetFavoritesByUserAsync(int userID);
        public Task<(string? hashedPassword, string? salt)> GetPasswordHashAndSaltByUsernameAsync(string username);
        public Task<(string? hashedPassword, string? salt)> GetPasswordHashAndSaltByUserIDAsync(int userID);
        public Task UpdatePasswordHashAndSaltByUserIDAsync(int userID, string hashedPassword, string salt);
        public Task UpdateUserAsync(UserDTO newUserDTO);
        public Task DeleteUserByIDAsync(int userID);
    }
}
