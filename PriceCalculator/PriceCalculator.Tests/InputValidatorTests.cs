﻿using System;
using System.Linq;
using FakeItEasy;
using PriceCalculator.Services;
using PriceCalculator.Services.DTO;
using PriceCalculator.Validators;
using Xunit;

namespace PriceCalculator.Tests
{
    public class InputValidatorTests
    {
        private readonly IProductService _fakeProductService;
        public InputValidatorTests()
        {
            _fakeProductService = A.Fake<IProductService>();
        }

        [Fact]
        public void InputValidatorValidate_EmptyProductsAreGiven_ReturnAnErrorForEmptyProducts()
        {
            //Arrange
            var validator = new InputValidator(_fakeProductService);

            //Act
            var errors = validator.Validate(new string[0]);

            //Assert
            Assert.Single(errors);
            Assert.Equal("The basket is empty. Please add some products.", errors.First());
        }

        [Fact]
        public void InputValidatorValidate_ProductNotExistInTheSystem_ShowErrorForInvalidProduct()
        {
            //Arrange
            var validator = new InputValidator(_fakeProductService);
            SetupProductService();

            //Act
            var errors = validator.Validate(new[] { "NoItem", "SecondItem" });

            //Assert
            Assert.Equal("Product NoItem is not valid. You can order: item1,item2,item3,item4.", errors[0]);
            Assert.Equal("Product SecondItem is not valid. You can order: item1,item2,item3,item4.", errors[1]);
        }
       

        [Fact]
        public void InputValidatorValidate_ValidProductIsGiven_NoErrorsReturned()
        {
            //Arrange
            var validator = new InputValidator(_fakeProductService);
            SetupProductService();

            //Act
            var errors = validator.Validate(new[] { "Item1" });

            //Assert
            Assert.Empty(errors);
        }


        [Fact]
        public void InputValidatorConstructor_ProductServiceIsNull_ThrowArgumentNullException()
        {
            //Arrange
            IProductService productService = null;

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => new InputValidator(productService));
            
        }


        private void SetupProductService()
        {
            A.CallTo(() =>
                    _fakeProductService.GetExistingProducts())
                .Returns(new []
                {
                    new Product(0.65m, "Item1"),
                    new Product(0.80m, "Item2"),
                    new Product(1.30m, "Item3"),
                    new Product(1.00m, "Item4"),
                });
        }
    }
}