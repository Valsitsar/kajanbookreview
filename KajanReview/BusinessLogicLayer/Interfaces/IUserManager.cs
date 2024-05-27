using BusinessLogicLayer.Entities;

namespace BusinessLogicLayer.Interfaces
{
    public interface IUserManager
    {
        public void CreateUser(User newUser);
        public User GetUserByID(int userID);
        public List<User> GetAllUsers();
        public void UpdateUser(User newUser);
        public void DeleteUserByID(int userID);
    }
}
