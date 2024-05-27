using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;

namespace BusinessLogicLayer.EntityManagers
{
    public class UserManager : IUserManager
    {
        private IUserDataAccess _userDataAccess;

        public UserManager(IUserDataAccess userDataAccess)
        {
            _userDataAccess = userDataAccess ?? throw new ArgumentNullException(nameof(_userDataAccess));
        }

        public void CreateUser(User newUser)
        {
            _userDataAccess.CreateUser(newUser);
        }

        public User GetUserByID(int userID)
        {
            return _userDataAccess.GetUserByID(userID);
        }

        public List<User> GetAllUsers()
        {
            return _userDataAccess.GetAllUsers();
        }

        public void UpdateUser(User user)
        {
            _userDataAccess.UpdateUser(user);
        }

        public void DeleteUserByID(int userID)
        {
            _userDataAccess.DeleteUserByID(userID);
        }
    }
}