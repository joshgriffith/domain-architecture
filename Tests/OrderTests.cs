using System;
using DomainArchitecture.Domain.Orders;
using DomainArchitecture.Domain.Orders.Events;
using DomainArchitecture.Domain.Products;
using DomainArchitecture.Domain.Users;
using DomainArchitecture.Infrastructure.Events;
using NUnit.Framework;

namespace DomainArchitecture.Tests {

    public class OrderTests {

        [Test]
        public void Purchase() {
            var user = new User();
            var product = new Product { QuantityAvailable = 50 };
            var order = user.Purchase(product, 1);

            Assert.AreEqual(1, order.Quantity);
            Assert.AreEqual(product, order.Product);
            Assert.AreEqual(user, order.User);
            Assert.NotNull(order.GetEvents().Any(e => e is Created<Order>));
        }

        [Test]
        public void Ship() {
            var order = new Order();
            order.Ship();

            Assert.AreEqual(Order.StatusTypes.Shipped, order.Status);
            Assert.NotNull(order.GetEvents().Any(e => e is OrderShipped));
        }

        [Test]
        public void Deliver() {
            var order = new Order();
            order.Ship();
            order.Deliver();

            Assert.AreEqual(Order.StatusTypes.Delivered, order.Status);
            Assert.NotNull(order.GetEvents().Any(e => e is OrderDelivered));
        }

        [Test]
        public void Cancel() {
            var order = new Order();
            order.Cancel();

            Assert.AreEqual(Order.StatusTypes.Cancelled, order.Status);
            Assert.NotNull(order.GetEvents().Any(e => e is Deleted<Order>));
        }
    }
}