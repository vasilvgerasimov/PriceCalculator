using System;
using System.Collections.Generic;
using System.Linq;
using PriceCalculator.Services;

namespace PriceCalculator.Validators
{
    public class InputValidator: IInputValidator
    {
        private readonly IProductService _productService;
        public InputValidator(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }
        public string[] Validate(string[] products)
        {
            var errors = new List<string>();
            if(products==null || !products.Any())
            {
                errors.Add("The basket is empty. Please add some products.");
                return errors.ToArray();
            }

            var availableProducts = _productService.GetExistingProducts().Select(s => s.Type.ToLower().Trim());

            foreach (var product in products)
            {                   
                if (!availableProducts.Contains(product.ToLower().Trim())) {
                    errors.Add($"Product {product} is not valid. You can order: {string.Join(',', availableProducts)}.");
                }                
            }

            return errors.ToArray();
        }
    }
}
