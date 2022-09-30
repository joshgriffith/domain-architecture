using Moq;
using NUnit.Framework;
using DomainArchitecture.Domain.Users;
using DomainArchitecture.Domain.Orders;
using DomainArchitecture.Domain.Orders.Events;
using DomainArchitecture.Domain.Products;
using DomainArchitecture.Infrastructure.Events;
using DomainArchitecture.Services;
using DomainArchitecture.Infrastructure.Email;

namespace DomainArchitecture.Tests
{
    public class EmailTests {

        [Test]
        public void OrderCreatedEmail() {
            WithDispatcher(dispatcher => dispatcher.Handle(new Created<Order>(TestOrder)));
        }

        [Test]
        public void OrderShippedEmail() {
            WithDispatcher(dispatcher => dispatcher.Handle(new OrderShipped(TestOrder)));
        }

        [Test]
        public void UserRegisteredEmail() {
            WithDispatcher(dispatcher => dispatcher.Handle(new Created<User>(TestUser)));
        }

        private void WithDispatcher(Action<EmailDispatcher> action) {
            var client = new Mock<IEmailClient>();
            client.Setup(x => x.SendEmail(TestUser.Email, It.IsAny<string>(), It.IsAny<string>()));
            action(new EmailDispatcher(client.Object));
            client.VerifyAll();
        }

        private static readonly User TestUser = new() {
            Email = "test@gmail.com"
        };

        private static readonly Order TestOrder = new() {
            Product = new Product(),
            User = TestUser
        }; 
    }
}