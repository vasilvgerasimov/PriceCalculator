using PriceCalculator.Discounts;

namespace PriceCalculator.Services
{
    public class DiscountService : IDiscountService
    {
        //That is a mock product service for getting products in the given challenge.
        public IProductDiscountStrategy[] GetDiscounts()
        {
            return new IProductDiscountStrategy[]
            {
                new SingleProductDiscount("Apples", 10),
                new RelatedProductsDiscount(new RequiredProduct("Beans",2), "Bread", 50)
            };
        }
    }
}