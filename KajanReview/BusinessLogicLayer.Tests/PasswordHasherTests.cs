using NUnit.Framework;
using BusinessLogicLayer;
using System;
using NUnit.Framework.Legacy;

namespace BusinessLogicLayer.Tests
{
    [TestFixture]
    public class PasswordHasherTests
    {
        [Test]
        public void TestHashAndSaltPassword_WithProvidedSalt()
        {
            // Arrange
            var passwordHasher = new PasswordHasher();
            string password = "TestPassword";
            string salt = "TestSalt";

            // Act
            var result = passwordHasher.HashAndSaltPassword(password, salt);

            // ClassicAssert
            ClassicAssert.IsNotNull(result.hashedPassword);
            ClassicAssert.AreEqual(salt, result.salt);
            ClassicAssert.AreNotEqual(password, result.hashedPassword);
        }

        [Test]
        public void TestHashAndSaltPassword_WithGeneratedSalt()
        {
            // Arrange
            var passwordHasher = new PasswordHasher();
            string password = "TestPassword";

            // Act
            var resultWithSalt1 = passwordHasher.HashAndSaltPassword(password);
            var resultWithSalt2 = passwordHasher.HashAndSaltPassword(password);

            // ClassicAssert
            ClassicAssert.IsNotNull(resultWithSalt1.hashedPassword);
            ClassicAssert.IsNotNull(resultWithSalt1.salt);
            ClassicAssert.AreNotEqual(password, resultWithSalt1.hashedPassword);
            ClassicAssert.AreNotEqual(resultWithSalt1.salt, resultWithSalt2.salt); // Ensure different salts are generated
        }

        [Test]
        public void TestHashAndSaltPassword_DifferentPasswordsDifferentHashes()
        {
            // Arrange
            var passwordHasher = new PasswordHasher();
            string password1 = "TestPassword1";
            string password2 = "TestPassword2";
            string salt = "TestSalt";

            // Act
            var result1 = passwordHasher.HashAndSaltPassword(password1, salt);
            var result2 = passwordHasher.HashAndSaltPassword(password2, salt);

            // ClassicAssert
            ClassicAssert.AreNotEqual(result1.hashedPassword, result2.hashedPassword); // Hashes should be different
        }
    }
}
