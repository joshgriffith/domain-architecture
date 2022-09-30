namespace DomainArchitecture.Domain.Products
{
    public class ProductSales
    {
        public Product Product { get; set; }
        public float Units { get; set; }
        public float Total { get; set; }
    }

    public static class ProductSalesExtensions
    {
        public static IQueryable<ProductSales> ByBestSelling(this IQueryable<ProductSales> query, int top = 5)
        {
            return query.OrderByDescending(each => each.Total).Take(top);
        }
    }
}