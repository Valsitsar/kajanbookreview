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

        public async Task<(string? hashedPassword, string? salt)> GetPasswordHashAndSaltByUsernameAsync(string username)
        {
            return await _userDataAccess.GetPasswordHashAndSaltByUsernameAsync(username);
        }

        public async Task<(string? hashedPassword, string? salt)> GetPasswordHashAndSaltByUserIDAsync(int userID)
        {
            return await _userDataAccess.GetPasswordHashAndSaltByUserIDAsync(userID);
        }

        public async Task UpdatePasswordHashAndSaltByUserIDAsync(int userID, string hashedPassword, string salt)
        {
            await _userDataAccess.UpdatePasswordHashAndSaltByUserIDAsync(userID, hashedPassword, salt);
        }

        public async Task UpdateUserAsync(UserDTO userDTO)
        {
            await _userDataAccess.UpdateUserAsync(userDTO);
        }

        public async Task DeleteUserByIDAsync(int userID)
        {
            await _userDataAccess.DeleteUserByIDAsync(userID);
        }

        public async Task<bool> TryPasswordChangeAsync(int userID, string currentPassword, string newPassword)
        {
            (string? hashedPassword, string? salt) = await GetPasswordHashAndSaltByUserIDAsync(userID);

            if (hashedPassword == null || salt == null)
            {
                return false;
            }

            bool passwordIsValid = _passwordAuthenticator.IsPasswordHashValid(currentPassword, hashedPassword, salt);

            if (!passwordIsValid)
            {
                return false;
            }

            (string newHashedPassword, string newSalt) = _passwordHasher.HashAndSaltPassword(newPassword);

            await UpdatePasswordHashAndSaltByUserIDAsync(userID, newHashedPassword, newSalt);

            return true;
        }
    }
}