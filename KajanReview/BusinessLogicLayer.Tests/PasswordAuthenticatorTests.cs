using NUnit.Framework;
using BusinessLogicLayer;
using Moq;
using NUnit.Framework.Legacy;

namespace BusinessLogicLayer.Tests
{
    [TestFixture]
    public class PasswordAuthenticatorTests
    {
        [Test]
        public void TestIsPasswordHashValid_WithValidCredentials()
        {
            // Arrange
            var mockPasswordHasher = new Mock<PasswordHasher>();
            string inputPassword = "TestPassword";
            string storedHash = "hashedTestPassword";
            string storedSalt = "TestSalt";
            mockPasswordHasher.Setup(p => p.HashAndSaltPassword(inputPassword, storedSalt))
                              .Returns((storedHash, storedSalt));
            var passwordAuthenticator = new PasswordAuthenticator(mockPasswordHasher.Object);

            // Act
            bool isValid = passwordAuthenticator.IsPasswordHashValid(inputPassword, storedHash, storedSalt);

            // Assert
            ClassicAssert.IsTrue(isValid);
        }

        [Test]
        public void TestIsPasswordHashValid_WithInvalidPassword()
        {
            // Arrange
            var mockPasswordHasher = new Mock<PasswordHasher>();
            string inputPassword = "WrongPassword";
            string storedHash = "hashedTestPassword";
            string storedSalt = "TestSalt";
            mockPasswordHasher.Setup(p => p.HashAndSaltPassword(inputPassword, storedSalt))
                              .Returns(("wrongHash", storedSalt));
            var passwordAuthenticator = new PasswordAuthenticator(mockPasswordHasher.Object);

            // Act
            bool isValid = passwordAuthenticator.IsPasswordHashValid(inputPassword, storedHash, storedSalt);

            // Assert
            ClassicAssert.IsFalse(isValid);
        }

        [Test]
        public void TestIsPasswordHashValid_WithInvalidSalt()
        {
            // Arrange
            var mockPasswordHasher = new Mock<PasswordHasher>();
            string inputPassword = "TestPassword";
            string storedHash = "hashedTestPassword";
            string wrongSalt = "WrongSalt";
            mockPasswordHasher.Setup(p => p.HashAndSaltPassword(inputPassword, wrongSalt))
                              .Returns(("wrongHash", wrongSalt));
            var passwordAuthenticator = new PasswordAuthenticator(mockPasswordHasher.Object);

            // Act
            bool isValid = passwordAuthenticator.IsPasswordHashValid(inputPassword, storedHash, wrongSalt);

            // Assert
            ClassicAssert.IsFalse(isValid);
        }
    }
}
