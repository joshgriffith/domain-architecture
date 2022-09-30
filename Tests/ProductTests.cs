using DomainArchitecture.Domain.Products;
using DomainArchitecture.Domain.Users;
using NUnit.Framework;

namespace DomainArchitecture.Tests {

    public class ProductTests {
        
        [Test]
        public void CanPurchase() {
            var product = new Product { QuantityAvailable = 1 };
            Assert.True(product.CanPurchase(new User(), 1));
        }

        [Test]
        public void QuantityExceeded() {
            var product = new Product { QuantityAvailable = 3 };
            Assert.False(product.CanPurchase(new User(), 4));
        }

        [Test]
        public void PurchaseLimitExceeded() {
            var product = new Product { QuantityAvailable = 3, PurchaseLimit = 2 };
            Assert.False(product.CanPurchase(new User(), 3));
        }
    }
}