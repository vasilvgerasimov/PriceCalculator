using System.Linq;
using PriceCalculator.Products;
using PriceCalculator.Services;
using PriceCalculator.Validators;

namespace PriceCalculator
{
    public class PriceCalculator : IPriceCalculator
    {
        private readonly IInputValidator _inputValidator;
        private readonly IProductService _productService;
        private readonly IDiscountService _discountService;
        private readonly IOutputWriter _outputWriter;

        public PriceCalculator(IInputValidator inputValidator, IProductService productService,
            IDiscountService discountService, IOutputWriter outputWriter)
        {
            _inputValidator = inputValidator;
            _productService = productService;
            _discountService = discountService;
            _outputWriter = outputWriter;
        }
        public void Run(string[] orderedProducts)
        {
            var validationErrors = _inputValidator.Validate(orderedProducts);
            if (validationErrors.Any())
            {
                _outputWriter.ShowErrors(validationErrors);
                return;
            }

            var quantityContainers = QuantityCounterBuilder.GetProductQuantityContainers(orderedProducts, _productService);
            var discounts = _discountService.GetDiscounts();

            var basket = new ShoppingBasket(discounts);
            basket.AddProducts(quantityContainers);
          
            var discountsToApply = basket.GetRelevantDiscounts();

            var subTotal = basket.SubTotal;
            var totalPrice = subTotal - discountsToApply.Sum(discount => discount.Price);

            _outputWriter.ShowReceipt(subTotal, discountsToApply, totalPrice);
        }
    }
}
