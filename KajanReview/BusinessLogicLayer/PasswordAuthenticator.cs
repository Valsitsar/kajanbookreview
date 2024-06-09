using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class PasswordAuthenticator
    {
        private PasswordHasher _passwordHasher;

        public PasswordAuthenticator(PasswordHasher passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        public bool IsPasswordHashValid(string inputPassword, string storedHash, string storedSalt)
        {
            var (hashedInputPassword, _) = _passwordHasher.HashAndSaltPassword(inputPassword, storedSalt);
            return hashedInputPassword == storedHash;
        }
    }
}
