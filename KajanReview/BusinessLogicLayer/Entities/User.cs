using BusinessLogicLayer.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.Entities
{
    public class User
    {
        public int ID { get; set; }
        public string? ProfilePicturePath { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;
        public string? MiddleNames { get; set; }

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public List<Bookshelf> Bookshelves { get; set; } = new List<Bookshelf>();
        public List<Role> Roles { get; set; } = new List<Role>();
        public List<IUserPost> Posts { get; set; } = new List<IUserPost>();
    }
}
