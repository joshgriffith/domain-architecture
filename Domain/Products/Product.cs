using DomainArchitecture.Domain.Orders;
using DomainArchitecture.Domain.Products.Events;
using DomainArchitecture.Domain.Users;
using DomainArchitecture.Infrastructure.Data.Entities;

namespace DomainArchitecture.Domain.Products {
    public class Product : Entity, IsDeletable {
        public string Name { get; set; }
        public float Price { get; set; }
        public int QuantityAvailable { get; set; }
        public int PurchaseLimit { get; set; }
        public bool IsFromExternalVendor { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastPurchaseDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public DateTime? DeletionDate { get; set; }

        public bool CanPurchase(User user, int quantity) {
            if (QuantityAvailable < quantity)
                return false;

            var orders = user.Orders.ByProduct(Id).Count();

            if (PurchaseLimit > 0 && PurchaseLimit < orders + quantity)
                return false;

            return true;
        }

        public void UpdateAvailability(int quantity) {
            QuantityAvailable = quantity;

            if (quantity == 0)
                Publish(new InventoryExhausted { Product = this });
        }
    }
}