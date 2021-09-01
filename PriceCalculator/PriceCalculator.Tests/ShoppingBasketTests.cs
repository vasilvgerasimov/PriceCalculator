using System;
using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using PriceCalculator.Discounts;
using PriceCalculator.Products;
using PriceCalculator.Services.DTO;
using Xunit;

namespace PriceCalculator.Tests
{
    public class ShoppingBasketTests
    {
        [Fact]
        public void GetRelevantDiscounts_ThereIsNoDiscountWithPriceGreaterThen0_ThereIsNoDiscountApplied()
        {
            // Arrange
            var products = GetQuantityContainer();
            var calculatedDiscount = new RelevantDiscount(0, "Text");

            var discountStrategy = A.Fake<IProductDiscountStrategy>();
            A.CallTo(() =>
                    discountStrategy.GetCalculatedDiscount(A<List<QuantityContainer>>.Ignored))
                .Returns(calculatedDiscount);

            var shoppingBasket = new ShoppingBasket(new[] { discountStrategy });
            shoppingBasket.AddProducts(products);

            //Act
            var discounts = shoppingBasket.GetRelevantDiscounts();

            //Assert
            A.CallTo(() =>
                discountStrategy.GetCalculatedDiscount(A<List<QuantityContainer>>.Ignored)).MustHaveHappenedOnceExactly();

            Assert.Single(discounts);
            Assert.NotEqual(calculatedDiscount, discounts.FirstOrDefault());
            Assert.Equal(0, discounts.FirstOrDefault().Price);
            Assert.Equal("(No offers available)", discounts.FirstOrDefault().Text);
        }

        [Fact]
        public void GetRelevantDiscounts_StrategyCalculatedDiscountIsCalledOnce_TheSameDiscountIsReturned()
        {
            // Arrange
            var products = GetQuantityContainer();
            var calculatedDiscount = new RelevantDiscount(1, "Text");

            var discountStrategy = A.Fake<IProductDiscountStrategy>();
            A.CallTo(() =>
                    discountStrategy.GetCalculatedDiscount(A<List<QuantityContainer>>.Ignored))
                .Returns(calculatedDiscount);

            var shoppingBasket = new ShoppingBasket(new[] { discountStrategy });
            shoppingBasket.AddProducts(products);

            //Act
            var discounts = shoppingBasket.GetRelevantDiscounts();

            //Assert
            A.CallTo(() =>
                discountStrategy.GetCalculatedDiscount(A<List<QuantityContainer>>.Ignored)).MustHaveHappenedOnceExactly();

            Assert.Single(discounts);
            Assert.Equal(calculatedDiscount, discounts.FirstOrDefault());
        }


        [Fact]
        public void GetCalculatedDiscount_InvalidDiscountsParameter_ThrowArgumentException()
        {
            // Arrange
            IProductDiscountStrategy[] discount = null;

            //Act &  Assert
            Assert.Throws<ArgumentNullException>(() => new ShoppingBasket(discount));
        }


        [Fact]
        public void Constructor_NoProductsGiven_NoDiscount()
        {
            // Arrange
            var products = GetQuantityContainer();
            var discountStrategy = A.Fake<IProductDiscountStrategy>();


            var shoppingBasket = new ShoppingBasket(new[] { discountStrategy });


            //Act
            var calculatedDiscounts = shoppingBasket.GetRelevantDiscounts();

            //Assert
            var discount = calculatedDiscounts.FirstOrDefault();
            Assert.Equal(0, discount.Price);
            Assert.Equal("(No offers available)", discount.Text);
        }

        [Fact]
        public void Constructor_EmptyProductsGiven_ThrowArgumentException()
        {
            // Arrange
            var discountStrategies = new IProductDiscountStrategy[0];
            var products = GetQuantityContainer();
            var shoppingBasket = new ShoppingBasket(discountStrategies);
            shoppingBasket.AddProducts(products);

            //Act &  Assert
            var calculatedDiscounts = shoppingBasket.GetRelevantDiscounts();

            var discount = calculatedDiscounts.FirstOrDefault();
            Assert.Equal(0, discount.Price);
            Assert.Equal("(No offers available)", discount.Text);
        }


        [Fact]
        public void Subtotals_GivenProductsWithDiscount_NoDiscountApplied()
        {
            // Arrange
            var products = new[]
            {
                new QuantityContainer(new Product(1, "Item1"),2),
                new QuantityContainer(new Product(5, "Item2"),6)
            };
            var calculatedDiscount = new RelevantDiscount(1, "Text");

            var discountStrategy = A.Fake<IProductDiscountStrategy>();
            A.CallTo(() =>
                    discountStrategy.GetCalculatedDiscount(A<QuantityContainer[]>.That.IsEqualTo(products)))
                .Returns(calculatedDiscount);

            var shoppingBasket = new ShoppingBasket(new[] { discountStrategy });
            shoppingBasket.AddProducts(products);

            //Act
            var subTotal = shoppingBasket.SubTotal;

            //Assert
            Assert.Equal(32, subTotal);
        }

        private QuantityContainer[] GetQuantityContainer()
        {
            return new[]
            {
                new QuantityContainer(new Product(1, "Item1"),2)
            };
        }
    }
}
