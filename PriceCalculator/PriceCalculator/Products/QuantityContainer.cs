using PriceCalculator.Services.DTO;

namespace PriceCalculator.Products
{
    public class QuantityContainer
    {
        public Product Product { get; }
        public int Quantity { get; set; }

        public QuantityContainer(Product product, int quantity = 0)
        {
            Product = product;
            Quantity = quantity;
        }
    }
}
