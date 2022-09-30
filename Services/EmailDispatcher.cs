using DomainArchitecture.Domain.Orders;
using DomainArchitecture.Domain.Orders.Events;
using DomainArchitecture.Domain.Users;
using DomainArchitecture.Infrastructure.Email;
using DomainArchitecture.Infrastructure.Events;

namespace DomainArchitecture.Services
{
    public class EmailDispatcher : IsObserver<Created<Order>>, IsObserver<OrderShipped>, IsObserver<Created<User>> {
        private readonly IEmailClient _email;

        public EmailDispatcher(IEmailClient email) {
            _email = email;
        }

        public void Handle(Created<Order> e) {
            _email.SendEmail(e.Entity.User.Email, "Thank you for your purchase", $"You purchased {e.Entity.Quantity} {e.Entity.Product.Name}. Your total price is {e.Entity.TotalPrice}.");
        }

        public void Handle(OrderShipped e) {
            _email.SendEmail(e.Order.User.Email, "Your order has shipped!", $"Your {e.Order.Product.Name} has shipped.");
        }

        public void Handle(Created<User> e) {
            _email.SendEmail(e.Entity.Email, "Welcome!", "Thank you for registering.");
        }
    }
}