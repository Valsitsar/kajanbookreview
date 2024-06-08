using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class PasswordHasher
    {
        public (string hashedPassword, string salt) HashPassword(string password, string salt = null)
        {
            // Generate a random salt if one is not provided
            if (salt == null)
            {
                byte[] saltBytes = new byte[32];
                RandomNumberGenerator.Fill(saltBytes);
                salt = Convert.ToBase64String(saltBytes);
            }

            // Combine the salt and password
            string saltAndPassword = salt + password;

            // Hash the salt and password
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(saltAndPassword));

                // Convert the bytes to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return (builder.ToString(), salt);
            }
        }
    }
}
