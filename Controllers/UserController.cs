using DomainArchitecture.Domain.Users;
using DomainArchitecture.Infrastructure.Authentication;
using DomainArchitecture.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace DomainArchitecture.Controllers
{

    [Route("users")]
    public class UserController : ControllerBase {

        private readonly IPasswordValidator _validator;
        private readonly Repository<User> _users;

        public UserController(IPasswordValidator validator, Repository<User> users) {
            _validator = validator;
            _users = users;
        }

        [HttpPost]
        public object RegisterUser(UserRegistration registration) {
            var user = registration.TryRegister(_users.Get(), _validator);
            _users.Add(user);
            return user;
        }

        [HttpPost("login")]
        public User? Login(string email, string password) {
            var user = _users.Get().ByEmail(email);

            if (user == null || !user.TryLogin(_validator, password))
                return null;

            return user;
        }
    }
}