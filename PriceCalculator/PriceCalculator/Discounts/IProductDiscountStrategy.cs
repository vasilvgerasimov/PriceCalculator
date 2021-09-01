using System.Collections.Generic;
using PriceCalculator.Products;

namespace PriceCalculator.Discounts
{
    public interface IProductDiscountStrategy
    {
        RelevantDiscount GetCalculatedDiscount(IEnumerable<QuantityContainer> products);
    }
}
