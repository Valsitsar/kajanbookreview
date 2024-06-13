using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTOs
{
    public class UserDTO
    {
        public int ID { get; set; }
        public string? ProfilePictureFilePath { get; set; }

        public string? FirstName { get; set; }
        public string? MiddleNames { get; set; }

        public string? LastName { get; set; }

        [Required(ErrorMessage = "CurrentUsername is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public Role Role { get; set; } = new Role();
        public List<Review> Reviews { get; set; } = new List<Review>();
        public List<Bookshelf> Bookshelves { get; set; } = new List<Bookshelf>();
    }
}
