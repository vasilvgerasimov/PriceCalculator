using PriceCalculator.Services.DTO;

namespace PriceCalculator.Services
{
    public interface IProductService
    {
        Product[] GetExistingProducts();
    }
}
