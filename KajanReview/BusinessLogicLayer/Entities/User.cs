using BusinessLogicLayer.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.Entities
{
    public class User
    {
        public int ID { get; set; }
        public string? ProfilePictureFilePath { get; set; } = "~/img/default-profile-picture.png";
        public string? FirstName { get; set; }
        public string? MiddleNames { get; set; }
        public string? LastName { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string PasswordHash { get; set; } = string.Empty;
        public string PasswordSalt { get; set; } = string.Empty;
        public Role Role { get; set; } = new Role() { Name = "Reader" };
        public List<Bookshelf> Bookshelves { get; set; } = new List<Bookshelf>();
        public List<Role> Roles { get; set; } = new List<Role>();
        public List<Review> Reviews { get; set; } = new List<Review>();
    }
}
