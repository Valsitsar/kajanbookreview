using BusinessLogicLayer.Entities;

namespace BusinessLogicLayer.Interfaces
{
    public interface IUserManager
    {
        public void CreateUser(User newUser, string hashedPassword, string salt);
        public User GetUserByID(int userID);
        public List<User> GetAllUsers();
        public void UpdateUser(User newUser);
        public void DeleteUserByID(int userID);
        public (string hashedPassword, string salt) GetPasswordAndSaltByUsername(string username);
        public void UpdatePasswordAndSaltByUserID(int userID, string hashedPassword, string salt);
    }
}
