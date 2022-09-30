using System;

namespace DomainArchitecture.Domain.Users.Events {
    public class LoggedIn {
        public User User { get; set; }
    }
}