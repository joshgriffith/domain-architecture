using DomainArchitecture.Domain.Orders;
using DomainArchitecture.Domain.Products;
using DomainArchitecture.Domain.Users;
using DomainArchitecture.Infrastructure.Data;
using DomainArchitecture.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DomainArchitecture.Controllers {

    [Route("orders")]
    public class OrderController : ControllerBase {

        private readonly User _currentUser;
        private readonly Repository<Product> _products;
        private readonly Repository<Order> _orders;

        public OrderController(User currentUser, Repository<Product> products, Repository<Order> orders) {
            _currentUser = currentUser;
            _products = products;
            _orders = orders;
        }

        [HttpPost]
        public ActionResult<Order> CreateOrder(int productId, int quantity) {
            var product = _products.Get().ById(productId);
            return _currentUser.Purchase(product, quantity);
        }

        [HttpDelete("{orderId}")]
        public ActionResult CancelOrder([FromRoute] int orderId) {
            var order = _orders.Get().ById(orderId);

            if (order == null)
                return NotFound();

            order.Cancel();
            return Ok();
        }
    }
}