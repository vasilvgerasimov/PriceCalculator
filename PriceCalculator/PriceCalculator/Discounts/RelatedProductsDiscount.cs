using System;
using System.Collections.Generic;
using System.Linq;
using PriceCalculator.Extensions;
using PriceCalculator.Products;

namespace PriceCalculator.Discounts
{
    public class RelatedProductsDiscount : IProductDiscountStrategy
    {
        private readonly RequiredProduct _requiredProducts;
        private readonly string _discountedProduct;
        private readonly decimal _discountPercentage;

        public RelatedProductsDiscount(RequiredProduct requiredProduct, string discountedProduct, int discountPercentage)
        {
            _requiredProducts = requiredProduct ?? throw new ArgumentNullException(nameof(requiredProduct));
            _discountedProduct = discountedProduct ?? throw new ArgumentNullException(nameof(discountedProduct));

            if (_requiredProducts.Quantity < 0)
                throw new ArgumentException(nameof(_requiredProducts));

            if (discountPercentage < 0)
                throw new ArgumentException(nameof(discountPercentage));

            _discountPercentage = discountPercentage;
        }

        public RelevantDiscount GetCalculatedDiscount(IEnumerable<QuantityContainer> quantityContainers)
        {
            var discount = 0M;

            if (quantityContainers == null || !quantityContainers.Any())
            {
                return CreateDiscount(discount);
            }

            var requiredProduct = quantityContainers.FirstOrDefault(product => string.Equals(product.Product.Type, _requiredProducts.Type, StringComparison.CurrentCultureIgnoreCase));
            var discountedProduct = quantityContainers.FirstOrDefault(product => string.Equals(product.Product.Type, _discountedProduct, StringComparison.CurrentCultureIgnoreCase));

            if (requiredProduct == null || discountedProduct == null || requiredProduct.Quantity < 1 ||
                discountedProduct.Quantity < 0 || discountedProduct.Product.Price <= 0)
                return CreateDiscount(discount);

            var requiredProductQuantity = requiredProduct.Quantity;
            var discountsProductQuantity = discountedProduct.Quantity;

            if (requiredProductQuantity >= _requiredProducts.Quantity)
            {
                discount = CalculateDiscount(requiredProductQuantity, discountsProductQuantity, discountedProduct);
            }

            return CreateDiscount(discount);
        }

        private decimal CalculateDiscount(int requiredProductQuantity, int discountsProductQuantity,
            QuantityContainer discountedProduct)
        {
            var discountedNumbers = requiredProductQuantity / _requiredProducts.Quantity;

            var productsQuantityToDiscount =
                discountsProductQuantity >= discountedNumbers ? discountedNumbers : discountsProductQuantity;

            var priceForDiscount = discountedProduct.Product.Price;
            return (productsQuantityToDiscount * priceForDiscount) * (_discountPercentage.ToPercentage());
        }

        private RelevantDiscount CreateDiscount(decimal discount)
        {
            return new RelevantDiscount(discount,
                $"{_requiredProducts.Type} give {_discountedProduct} {_discountPercentage}% off: -{discount.ToCurrencyWithPence()}");
        }
    }
}
