using DomainArchitecture.Domain.Orders;
using DomainArchitecture.Domain.Products;
using DomainArchitecture.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace DomainArchitecture.Controllers
{

    [Route("products")]
    public class ProductController : ControllerBase {

        private readonly Repository<Order> _orders;

        public ProductController(Repository<Order> orders) {
            _orders = orders;
        }

        [HttpGet("best-selling")]
        public ActionResult<List<ProductSales>> GetBestSellingProducts() {
            return _orders.Get().ToProductSales().ByBestSelling().ToList();
        }
    }
}