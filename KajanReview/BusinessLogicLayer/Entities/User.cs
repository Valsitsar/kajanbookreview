using BusinessLogicLayer.Interfaces;

namespace BusinessLogicLayer.Entities
{
    public class User
    {
        public int ID { get; set; }
        public string? ProfilePicturePath { get; set; } = string.Empty; // TODO: Add to DB
        public string FirstName { get; set; } = string.Empty;
        public string? MiddleNames { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public List<Bookshelf> Bookshelves { get; set; } = new List<Bookshelf>();
        public List<Role> Roles { get; set; } = new List<Role>();
        public List<IUserPost>? Posts { get; set; } = new List<IUserPost>(); // Maybe don't need to init
    }
}
