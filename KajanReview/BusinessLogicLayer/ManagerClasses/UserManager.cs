using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;

namespace BusinessLogicLayer.ManagerClasses
{
    public class UserManager : IUserManager
    {
        private readonly IUserDataAccess _userDataAccess;
        private readonly PasswordHasher _passwordHasher;
        private readonly PasswordAuthenticator _passwordAuthenticator;

        public UserManager(IUserDataAccess userDataAccess)
        {
            _userDataAccess = userDataAccess ?? throw new ArgumentNullException(nameof(_userDataAccess));
            _passwordHasher = new PasswordHasher();
            _passwordAuthenticator = new PasswordAuthenticator(_passwordHasher);
        }

        public async Task CreateUserAsync(UserDTO newUser, string hashedPassword, string salt)
        {
            await _userDataAccess.CreateUserAsync(newUser, hashedPassword, salt);
        }

        public async Task<UserDTO> GetUserByIDAsync(int userID)
        {
            return await _userDataAccess.GetUserByIDAsync(userID);
        }

        public async Task<User> GetUserByUsernameForLoginAsync(string username)
        {
            return await _userDataAccess.GetUserByUsernameForLoginAsync(username);
        }

        public async Task<User> GetUserByEmailForLoginAsync(string email)
        {
            return await _userDataAccess.GetUserByEmailForLoginAsync(email);
        }

        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            return await _userDataAccess.GetAllUsersAsync();
        }

        public async Task<List<Review>> GetRatingsByUserAsync(int userID)
        {
            return await _userDataAccess.GetRatingsByUserAsync(userID);
        }

        public async Task<List<Book>> GetFavoritesByUserAsync(int userID)
        {
            return await _userDataAccess.GetFavoritesByUserAsync(userID);
        }

        public async Task<(string? hashedPassword, string? salt)> GetPasswordHashAndSaltByUsernameAsync(string username)
        {
            return await _userDataAccess.GetPasswordHashAndSaltByUsernameAsync(username);
        }

        public async Task<(string? hashedPassword, string? salt)> GetPasswordHashAndSaltByUserIDAsync(int userID)
        {
            return await _userDataAccess.GetPasswordHashAndSaltByUserIDAsync(userID);
        }

        public async Task UpdatePasswordHashAndSaltByUserIDAsync(int userID, string inputtedPassword, string storedSalt)
        {
            // Validate the current password and get the new hashed password and salt
            var result = await PrepareNewPasswordIfValid(userID, inputtedPassword);

            if (result == null)
            {
                // If the password is not valid, throw an exception
                throw new ArgumentException("The inputted password is invalid.");
            }

            // Deconstruct the tuple to extract the new hashed password and salt
            var (newHashedPassword, newSalt) = result.Value;

            // Update the user's password hash and salt in the database
            await _userDataAccess.UpdatePasswordHashAndSaltByUserIDAsync(userID, newHashedPassword, newSalt);
        }

        public async Task UpdateUserAsync(UserDTO userDTO)
        {
            await _userDataAccess.UpdateUserAsync(userDTO);
        }

        public async Task DeleteUserByIDAsync(int userID)
        {
            await _userDataAccess.DeleteUserByIDAsync(userID);
        }

        public async Task<(string? newHashedPassword, string? newSalt)?> PrepareNewPasswordIfValid(int userID, string inputtedPassword)
        {
            // Retrieve the current hashed password and salt for the user from the DB
            (string? storedHashedPassword, string? storedSalt) = await GetPasswordHashAndSaltByUserIDAsync(userID);

            // If either the hashed password or salt could not be retrieved, return false
            if (storedHashedPassword == null || storedSalt == null)
            {
                return null;
            }

            // Check if the hash of the inputted current password matches the stored hashed password
            bool passwordIsValid = _passwordAuthenticator.IsPasswordHashValid(inputtedPassword, storedHashedPassword, storedSalt);

            if (!passwordIsValid)
            {
                return null;
            }

            // If the password is valid, hash and salt the new password
            (string newHashedPassword, string newSalt) = _passwordHasher.HashAndSaltPassword(inputtedPassword);

            return (newHashedPassword, newSalt);
        }
    }
}