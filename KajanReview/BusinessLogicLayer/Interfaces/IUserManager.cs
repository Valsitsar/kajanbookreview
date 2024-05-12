using BusinessLogicLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
