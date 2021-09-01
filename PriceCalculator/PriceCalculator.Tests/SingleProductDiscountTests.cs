using System;
using PriceCalculator.Discounts;
using PriceCalculator.Products;
using PriceCalculator.Services.DTO;
using Xunit;

namespace PriceCalculator.Tests
{
    public class SingleProductDiscountTests
    {
        [Theory]
        [InlineData("item1", 10, 1 , 0.1, "item1 10% off: -10p")]
        [InlineData("item1", 20, 2, 0.4, "item1 20% off: -40p")]
        [InlineData("item1", 10, 10, 1, "item1 10% off: -£1.00")]
        [InlineData("item1", 0.5, 10, 0.05, "item1 0.5% off: -5p")]
        public void GetCalculatedDiscount_ProductHasValidDiscount_DiscountIsAppliedCorrectly(string discountedProduct,decimal percentage,int quantity, decimal expectedPrice, string expectedText)
        {
            // Arrange
            var singleProductDiscount = new SingleProductDiscount(discountedProduct, percentage);
            var products = new[] 
            {
                new QuantityContainer(new Product(1,"Item1"), quantity) 
            };

            //Act
            var discount = singleProductDiscount.GetCalculatedDiscount(products);

            //Assert
            Assert.Equal(expectedPrice,discount.Price);
            Assert.Equal(expectedText, discount.Text);
            
        }

        [Fact]
        public void GetCalculatedDiscount_SeveralProductsAreGiven_DiscountIsAppliedOnlyOnCorrectProduct()
        {
            // Arrange
            var singleProductDiscount = new SingleProductDiscount("Item1", 10);
            var products = new[]
            {
                new QuantityContainer(new Product(1,"Item1"), 5),
                new QuantityContainer(new Product(1,"Item2"), 2)
            };

            //Act
            var discount = singleProductDiscount.GetCalculatedDiscount(products);

            //Assert
            Assert.Equal(0.5M, discount.Price);
        }

        [Fact]
        public void GetCalculatedDiscount_SeveralProductsAreGivenWithoutDiscount_DiscountIsNotApplied()
        {
            // Arrange
            var singleProductDiscount = new SingleProductDiscount("Item1", 10);
            var products = new[]
            {
                new QuantityContainer(new Product(1,"Item2"), 5),
                new QuantityContainer(new Product(1,"Item3"), 2)
            };

            //Act
            var discount = singleProductDiscount.GetCalculatedDiscount(products);

            //Assert
            Assert.Equal(0, discount.Price);
        }

        [Fact]
        public void Constructor_DiscountedProduct_IsNull_ThrowsArgumentException()
        {
            //arrange
            string discountedProduct = null;
            decimal percentage = 1;

            //act & assert
            Assert.Throws<ArgumentNullException>(() => new SingleProductDiscount(discountedProduct, percentage));
        }

        [Fact]
        public void Constructor_DiscountedPercentage_LessThen0_ThrowsArgumentException()
        {
            //arrange
            var discountedProduct = "Item1";
            decimal percentage = -1;

            //act & assert
            Assert.Throws<ArgumentException>(() => new SingleProductDiscount(discountedProduct, percentage));
        }
    }
}
