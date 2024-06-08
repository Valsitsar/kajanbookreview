using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class PasswordVerifier
    {
        private PasswordHasher _passwordHasher;

        public PasswordVerifier(PasswordHasher passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        public bool VerifyPassword(string inputPassword, string storedHash, string storedSalt)
        {
            var (hashedInputPassword, _) = _passwordHasher.HashPassword(inputPassword, storedSalt);
            return hashedInputPassword == storedHash;
        }
    }
}
