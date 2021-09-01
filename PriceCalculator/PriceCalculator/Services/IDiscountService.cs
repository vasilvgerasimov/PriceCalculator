using PriceCalculator.Discounts;

namespace PriceCalculator.Services
{
    public interface IDiscountService
    {
        IProductDiscountStrategy[] GetDiscounts();
    }
}
