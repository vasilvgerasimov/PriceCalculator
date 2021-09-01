using PriceCalculator.Services.DTO;

namespace PriceCalculator.Services
{
    public class ProductService : IProductService
    {
        //I assume that the existing products will be requested from separate MicroService app, DataBase, File, etc.
        //That is a mock product service for getting products in the given challenge.
        public Product[] GetExistingProducts()
        {
            return new []
            {
                new Product(0.65m, "beans"),
                new Product(0.80m, "bread"),
                new Product(1.30m, "milk"),
                new Product(1.00m, "apples"),
            };
        }
    }
}
