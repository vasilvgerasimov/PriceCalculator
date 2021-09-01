using System;
using System.Collections.Generic;
using System.Linq;
using PriceCalculator.Services;

namespace PriceCalculator.Products
{
    public static class QuantityCounterBuilder
    {
        public static QuantityContainer[] GetProductQuantityContainers(IEnumerable<string> orderedProducts, IProductService productService)
        {
            var existingProducts = productService.GetExistingProducts();
            var quantityContainers = new List<QuantityContainer>();

            foreach (var product in orderedProducts)
            {
                var dtoProduct = existingProducts.FirstOrDefault(s => string.Equals(s.Type.Trim(), product.Trim(), StringComparison.CurrentCultureIgnoreCase));
                
                if (dtoProduct == null)
                {
                    throw new ArgumentException($"Invalid product type: {product}");
                }
               
                var currentQuantityContainer = quantityContainers.FirstOrDefault(p =>
                    string.Equals(p.Product.Type, dtoProduct.Type, StringComparison.CurrentCultureIgnoreCase));

                if (currentQuantityContainer == null)
                {
                    if(dtoProduct.Price<=0)
                        throw new InvalidOperationException("Insufficient price");

                    quantityContainers.Add(new QuantityContainer(dtoProduct, 1));
                }
                else
                {
                    currentQuantityContainer.Quantity++;
                }
            }

            return quantityContainers.ToArray();
        }
    }
}
