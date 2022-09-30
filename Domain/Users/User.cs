using System;
using DomainArchitecture.Domain.Orders;
using DomainArchitecture.Domain.Products;
using DomainArchitecture.Infrastructure.Authentication;
using DomainArchitecture.Infrastructure.Data.Entities;

namespace DomainArchitecture.Domain.Users {
    public class User : Entity, IsDeletable {
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime? DeletionDate { get; set; }
        public List<Order> Orders { get; set; } = new();

        public User() {
            CreationDate = DateTime.UtcNow;
        }

        public bool TryLogin(IPasswordValidator validator, string password) {
            if (validator.IsValid(PasswordHash, password)) {
                LastLoginDate = DateTime.UtcNow;
                return true;
            }

            return false;
        }

        public Order Purchase(Product product, int quantity) {
            var order = new Order(this, product, quantity);

            Orders.Add(order);

            var canPurchase = product.CanPurchase(this, quantity);

            if (!canPurchase)
                throw new InvalidOperationException("Unable to complete purchase.");

            product.QuantityAvailable -= quantity;
            product.LastPurchaseDate = DateTime.UtcNow;

            return order;
        }
    }

    public static class UserExtensions {
        public static User? ByEmail(this IQueryable<User> query, string email) {
            return query.FirstOrDefault(each => each.Email == email);
        }
    }
}