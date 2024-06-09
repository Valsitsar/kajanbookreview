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

        public void CreateUser(User newUser, string hashedPassword, string salt)
        {
            _userDataAccess.CreateUser(newUser, hashedPassword, salt);
        }

        public async Task<UserDTO> GetUserByIDAsync(int userID)
        {
            return await _userDataAccess.GetUserByIDAsync(userID);
        }

        public User GetUserByUsernameForLogin(string username)
        {
            return _userDataAccess.GetUserByUsernameForLogin(username);
        }

        public User GetUserByEmailForLogin(string email)
        {
            return _userDataAccess.GetUserByEmailForLogin(email);
        }

        public List<UserDTO> GetAllUsers()
        {
            return _userDataAccess.GetAllUsers();
        }

        public async Task UpdateUserAsync(UserDTO userDTO)
        {
            await _userDataAccess.UpdateUserAsync(userDTO);
        }

        public void DeleteUserByID(int userID)
        {
            _userDataAccess.DeleteUserByID(userID);
        }

        public (string? hashedPassword, string? salt) GetPasswordHashAndSaltByUsername(string username)
        {
            return _userDataAccess.GetPasswordHashAndSaltByUsername(username);
        }
        public async Task<(string? hashedPassword, string? salt)> GetPasswordHashAndSaltByUserIDAsync(int userID)
        {
            return await _userDataAccess.GetPasswordHashAndSaltByUserIDAsync(userID);
        }

        public void UpdatePasswordHashAndSaltByUserID(int userID, string hashedPassword, string salt)
        {
            _userDataAccess.UpdatePasswordHashAndSaltByUserID(userID, hashedPassword, salt);
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

            UpdatePasswordHashAndSaltByUserID(userID, newHashedPassword, newSalt);

            return true;
        }
    }
}