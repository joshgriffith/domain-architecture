using DomainArchitecture.Domain.Users;
using DomainArchitecture.Infrastructure.Authentication;
using NUnit.Framework;

namespace DomainArchitecture.Tests
{
    public class UserTests {

        [Test]
        public void Register() {

            var registration = new UserRegistration {
                Email = "test@email.com",
                Password = "testpassword"
            };

            var validator = GetValidator();
            var user = registration.TryRegister(new List<User>().AsQueryable(), validator);

            Assert.AreEqual("test@email.com", user.Email);
            Assert.AreEqual(validator.Hash("testpassword"), user.PasswordHash);
        }

        [Test]
        public void ExistingUserRegistered() {
            
            var users = new List<User> {
                new() { Email = "test@email.com" }
            };

            var registration = new UserRegistration {
                Email = "test@email.com",
                Password = "testpassword"
            };

            Assert.Throws<InvalidOperationException>(() => {
                registration.TryRegister(users.AsQueryable(), GetValidator());
            });
        }

        [Test]
        public void Login() {
            var validator = GetValidator();

            var user = new User {
                PasswordHash = validator.Hash("test")
            };

            Assert.True(user.TryLogin(validator, "test"));
        }

        [Test]
        public void InvalidPassword() {

            var user = new User {
                PasswordHash = "test"
            };

            Assert.False(user.TryLogin(GetValidator(), "test"));
        }

        private IPasswordValidator GetValidator() {
            return new PasswordValidator("secret");
        }
    }
}