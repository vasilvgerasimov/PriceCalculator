using System;
using PriceCalculator.Discounts;
using PriceCalculator.Products;
using PriceCalculator.Services.DTO;
using Xunit;

namespace PriceCalculator.Tests
{
    public class RelatedProductsDiscountTests
    {
        [Fact]
        public void GetCalculatedDiscount_ProductHasValidDiscount_DiscountIsAppliedCorrectly()
        {
            //Arrange
            var relatedProductsDiscount = GetRelatedProductsDiscount();
            
            var products = new[] 
            {
                new QuantityContainer(new Product(1,"Item1"), 2),
                new QuantityContainer(new Product(10,"Item2"), 1)
            };

            //Act
            var discount = relatedProductsDiscount.GetCalculatedDiscount(products);

            //Assert
            Assert.Equal(5,discount.Price);
            Assert.Equal("Item1 give Item2 50% off: -£5.00", discount.Text);
            
        }

        [Fact]
        public void GetCalculatedDiscount_OnePairOfRequiredProducts_TwoDiscountedProductsAdded_DiscountIsAppliedOnlyOnOneProduct()
        {
            // Arrange
            var relatedProductsDiscount = GetRelatedProductsDiscount();

            var products = new[]
            {
                new QuantityContainer(new Product(1,"Item1"), 2),
                new QuantityContainer(new Product(10,"Item2"), 2)
            };

            //Act
            var discount = relatedProductsDiscount.GetCalculatedDiscount(products);

            //Assert
            Assert.Equal(5, discount.Price);
            Assert.Equal("Item1 give Item2 50% off: -£5.00", discount.Text);
        }

        private static RelatedProductsDiscount GetRelatedProductsDiscount()
        {
            return new RelatedProductsDiscount(new RequiredProduct("Item1", 2),
                "Item2", 50);
        }

        [Fact]
        public void GetCalculatedDiscount_TwoPairOfRequiredProducts_OneDiscountedProduct_DiscountIsAppliedOnlyOnce()
        {
            // Arrange
            var relatedProductsDiscount = GetRelatedProductsDiscount();

            var products = new[]
            {
                new QuantityContainer(new Product(1,"Item1"), 4),
                new QuantityContainer(new Product(10,"Item2"), 1)
            };

            //Act
            var discount = relatedProductsDiscount.GetCalculatedDiscount(products);

            //Assert
            Assert.Equal(5, discount.Price);
            Assert.Equal("Item1 give Item2 50% off: -£5.00", discount.Text);
        }

        [Fact]
        public void GetCalculatedDiscount_ProductsAreEmpty_NoDiscountApplied()
        {
            // Arrange
            var relatedProductsDiscount = GetRelatedProductsDiscount();

            var products = new QuantityContainer[0];

            //Act
            var discount = relatedProductsDiscount.GetCalculatedDiscount(products);

            //Assert
            Assert.Equal(0, discount.Price);
        }

        [Fact]
        public void GetCalculatedDiscount_ProductsAreNull_NoDiscountApplied()
        {
            // Arrange
            var relatedProductsDiscount = new RelatedProductsDiscount(new RequiredProduct("Item1", 2),
                "Item2", 50);

            QuantityContainer[] products = null;

            //Act
            var discount = relatedProductsDiscount.GetCalculatedDiscount(products);

            //Assert
            Assert.Equal(0, discount.Price);
        }

        [Fact]
        public void Constructor_RequiredProduct_IsNull_ThrowsArgumentNullException()
        {
            //Arrange
            var discountedProduct = "Item3";
            var discountPercentage = 50;

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => new RelatedProductsDiscount(null, discountedProduct, discountPercentage));
        }

        [Fact]
        public void Constructor_DiscountedProduct_IsNull_ThrowsArgumentNullException()
        {
            //Arrange
            var requiredProduct = "Item1";
            string discountedProduct = null;
            var requiredQuantity = 2;
            var discountPercentage = 50;

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => new RelatedProductsDiscount(new RequiredProduct(requiredProduct, requiredQuantity), discountedProduct, discountPercentage));
        }

        [Fact]
        public void Constructor_RequiredQuantityIsNegativeNumber_ThrowsArgumentException()
        {
            //Arrange
            var requiredProduct = "Item1";
            string discountedProduct = "Item2";
            var requiredQuantity = -1;
            var discountPercentage = 50;

            //Act & Assert
            Assert.Throws<ArgumentException>(() => new RelatedProductsDiscount(new RequiredProduct(requiredProduct, requiredQuantity), discountedProduct,  discountPercentage));
        }

        [Fact]
        public void Constructor_DiscountPercentageIsNegativeNumber_ThrowsArgumentException()
        {
            //Arrange
            var requiredProduct = "Item1";
            string discountedProduct = "Item2";
            var requiredQuantity = 1;
            var discountPercentage = -50;

            //Act & Assert
            Assert.Throws<ArgumentException>(() => new RelatedProductsDiscount(new RequiredProduct(requiredProduct, requiredQuantity), discountedProduct, discountPercentage));
        }
    }
}
