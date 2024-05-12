using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Interfaces;

namespace BusinessLogicLayer.Entities
{
    public class User
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string? MiddleNames { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<Bookshelf> Bookshelves { get; set; }
        public List<Role> Roles { get; set; }
        public List<IUserPost>? Posts { get; set; }
    }
}
