using System;
using System.Collections.Generic;
using PriceCalculator.Extensions;
using PriceCalculator.Products;

namespace PriceCalculator.Discounts
{
    public class SingleProductDiscount : IProductDiscountStrategy
    {
        private readonly string _discountedProduct;
        private readonly decimal _percentage;

        public SingleProductDiscount(string discountedProduct, decimal percentage)
        {
            _discountedProduct = discountedProduct ?? throw new ArgumentNullException(nameof(discountedProduct));
            if (percentage < 0)
                throw new ArgumentException(nameof(percentage));
            _percentage = percentage;
        }

        public RelevantDiscount GetCalculatedDiscount(IEnumerable<QuantityContainer> products)
        {
            var discount = 0m;
            foreach (var product in products)
            {
                if (string.Equals(product.Product.Type, _discountedProduct, StringComparison.CurrentCultureIgnoreCase))
                {
                    discount += (product.Product.Price * product.Quantity) * _percentage.ToPercentage();
                }
            }

            return new RelevantDiscount(discount, string.Format($"{_discountedProduct} {_percentage}% off: -{discount.ToCurrencyWithPence()}"));
        }
    }
}
