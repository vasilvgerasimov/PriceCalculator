using System;
using System.Collections.Generic;
using System.Linq;
using PriceCalculator.Discounts;
using PriceCalculator.Products;

namespace PriceCalculator
{
    public class ShoppingBasket
    {
        private readonly IProductDiscountStrategy[] _discounts;
        private List<QuantityContainer> _quantityContainers;
        public ShoppingBasket(IProductDiscountStrategy[] discounts)
        {
            _discounts = discounts ?? throw new ArgumentNullException(nameof(discounts));
            _quantityContainers = new List<QuantityContainer>();
        }

        public decimal SubTotal => _quantityContainers.Sum(p => p.Product.Price * p.Quantity);

        public void AddProducts(QuantityContainer[] products)
        {
            _quantityContainers.AddRange(products);
        }

        public IEnumerable<RelevantDiscount> GetRelevantDiscounts()
        {
            var relevantDiscounts = new List<RelevantDiscount>();

            if (_quantityContainers == null || !_quantityContainers.Any() || _discounts == null || !_discounts.Any())
            {
                relevantDiscounts.Add(GetEmptyDiscount());
                return relevantDiscounts;
            }


            relevantDiscounts.AddRange(_discounts.Select(discount => discount.GetCalculatedDiscount(_quantityContainers))
                .Where(p=>p.Price>0));


            if (!relevantDiscounts.Any())
            {
                relevantDiscounts.Add(GetEmptyDiscount());
            }

            return relevantDiscounts;
        }

        private RelevantDiscount GetEmptyDiscount()
        {
            return new RelevantDiscount(0, "(No offers available)");
        }
    }
}

