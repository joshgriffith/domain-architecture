using DomainArchitecture.Domain.Orders.Events;
using DomainArchitecture.Domain.Products;
using DomainArchitecture.Domain.Users;
using DomainArchitecture.Infrastructure.Data.Entities;

namespace DomainArchitecture.Domain.Orders {

    public class Order : Entity, IsDeletable {
        public User User { get; set; }
        public Product Product { get; set; }
        public float TotalPrice { get; set; }
        public int Quantity { get; set; }
        public StatusTypes Status { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public DateTime? DeletionDate { get; set; }

        public Order() {
            CreationDate = DateTime.UtcNow;
        }

        public Order(User user, Product product, int quantity) : this() {
            User = user;
            Product = product;
            Quantity = quantity;
            TotalPrice = product.Price * quantity;
        }

        public void Ship() {
            if (Status == StatusTypes.Pending) {
                Status = StatusTypes.Shipped;
                ShippedDate = DateTime.UtcNow;
                Publish(new OrderShipped(this));
            }
        }

        public void Deliver() {
            if (Status == StatusTypes.Shipped) {
                Status = StatusTypes.Delivered;
                Publish(new OrderDelivered(this));
            }
        }

        public void Cancel() {
            if (Status != StatusTypes.Cancelled) {
                Status = StatusTypes.Cancelled;
                this.Delete();
            }
        }

        public enum StatusTypes {
            Pending = 0,
            Shipped = 1,
            Delivered = 2,
            Cancelled = 3
        }
    }

    public static class OrderExtensions {

        public static IQueryable<ProductSales> ToProductSales(this IQueryable<Order> query) {
            return query.Where(each => each.DeletionDate == null)
                .GroupBy(order => order.Product)
                .Select(orders => new ProductSales {
                    Product = orders.Key,
                    Units = orders.Count(),
                    Total = orders.Sum(order => order.TotalPrice)
                })
                .Where(each => each.Product.DeletionDate == null);
        }

        public static IQueryable<Order> ByUser(this IQueryable<Order> orders, int userId) {
            return orders.Where(each => each.User.Id == userId);
        }

        public static IEnumerable<Order> ByProduct(this IEnumerable<Order> orders, int productId) {
            return orders.Where(each => each.Product.Id == productId);
        }
    }
}