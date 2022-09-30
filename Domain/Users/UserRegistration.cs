using DomainArchitecture.Infrastructure.Authentication;

namespace DomainArchitecture.Domain.Users {
    public class UserRegistration {

        public string Email { get; set; }
        public string Password { get; set; }

        public User TryRegister(IQueryable<User> users, IPasswordValidator validator) {
            if (!IsEmailValid(Email))
                throw new ArgumentException("You must provide a valid email.", nameof(Email));

            if (!IsPasswordValid(Password))
                throw new ArgumentException("Your password must be at least 8 characters long.", nameof(Password));

            var user = users.ByEmail(Email);

            if (user != null)
                throw new InvalidOperationException("User already exists.");

            return new User {
                Email = Email,
                PasswordHash = validator.Hash(Password)
            };
        }

        private bool IsEmailValid(string email) {
            return !string.IsNullOrEmpty(email) && email.Contains("@") && email.Length > 5;
        }

        private bool IsPasswordValid(string password) {
            return !string.IsNullOrEmpty(password) && password.Length >= 8;
        }
    }
}