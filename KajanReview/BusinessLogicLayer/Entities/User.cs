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

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Password { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public List<Bookshelf> Bookshelves { get; set; } = new List<Bookshelf>();
        public List<Role> Roles { get; set; } = new List<Role>();
        public List<IUserPost> Posts { get; set; } = new List<IUserPost>();
    }
}
