using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.EntityManagers
{
    public class UserManager : IUserManager
    {
        private IUserDataAccess _userDataAccess;

        public UserManager(IUserDataAccess userDataAccess)
        {
            _userDataAccess = userDataAccess;
        }

        public void CreateUser(User newUser)
        {
            try { _userDataAccess.CreateUser(newUser); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        public User GetUserByID(int userID)
        {
            try {  return _userDataAccess.GetUserByID(userID); }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<User> GetAllUsers()
        {
            try { return _userDataAccess.GetAllUsers(); }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public void UpdateUser(User user)
        {
            try { _userDataAccess.UpdateUser(user); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        public void DeleteUserByID(int userID)
        {
            try { _userDataAccess.DeleteUserByID(userID); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
    }
}