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
        public string? ProfilePictureFilePath { get; set; } = "~/img/default-profile-picture.png";

        public string? FirstName { get; set; }
        public string? MiddleNames { get; set; }

        public string? LastName { get; set; }

        [Required(ErrorMessage = "CurrentUsername is required.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
    }
}
