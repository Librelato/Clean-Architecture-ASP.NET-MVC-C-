using CleanArchMvc.Domain.Entities;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Domain.Tests
{
    public class ProductUnitTest1
    {
        [Fact(DisplayName = "Create Product with valid state")]
        public void CreateProduct_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Product(1, "Produto 01", "Produto 01 Description", 12 , 45, "product image url");
            action.Should().NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Product with negative Id value")]
        public void CreateProduct_NegativeIdValue_DomainExceptionInvalidId()
        {
            Action action = () => new Product(-1, "Produto 01", "Produto 01 Description", 12, 45, "product image url");
            action.Should()
                  .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                  .WithMessage("Invalid Id value!");
        }

        [Fact(DisplayName = "Create Product with short name")]
        public void CreateProduct_ShortNameValue_DomainExceptionNameTooShort()
        {
            Action action = () => new Product(1, "Pr", "Produto 01 Description", 12, 45, "product image url");
            action.Should()
                  .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                  .WithMessage("Invalid name. Too short, minimum 3 characters!");
        }

        [Fact(DisplayName = "Create Product with missing name value")]
        public void CreateProduct_MissingNameValue_DomainExceptionNameIsRequired()
        {
            Action action = () => new Product(1, "", "Produto 01 Description", 12, 45, "product image url");
            action.Should()
                  .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                  .WithMessage("Invalid Name. Name is required!");
        }

        [Fact(DisplayName = "Create Product with null name value")]
        public void CreateProduct_NullNameValue_DomainExceptionNameIsRequired()
        {
            Action action = () => new Product(1, null, "Produto 01 Description", 12, 45, "product image url");
            action.Should()
                  .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                  .WithMessage("Invalid Name. Name is required!");
        }

        [Fact(DisplayName = "Create Product with invalid price value")]
        public void CreateProduct_InvalidPriceValue_DomainExceptionInvalidPriceValue()
        {
            Action action = () => new Product(1, "Produto 01", "Produto 01 Description", -1, 45, "product image url");
            action.Should()
                  .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                  .WithMessage("Invalid price value!");
        }

        [Fact(DisplayName = "Create Product with invalid stock value")]
        public void CreateProduct_InvalidStockValue_DomainExceptionInvalidStockValue()
        {
            Action action = () => new Product(1, "Produto 01", "Produto 01 Description", 12, -1, "product image url");
            action.Should()
                  .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                  .WithMessage("Invalid stock value!");
        }

        [Fact(DisplayName = "Create Product with Image value too long")]
        public void CreateProduct_InvalidImageValue_DomainExceptionImageValueTooLong()
        {
            Action action = () => new Product(1, "Produto 01", "Produto 01 Description", 12, 45, (new string('a',256)));
            action.Should()
                  .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                  .WithMessage("Ivalid image name. Too long, maximium 250 characters!");
        }

        [Fact(DisplayName = "Create Product with null Image value")]
        public void CreateProduct_NullImageValue_NoDomainException()
        {
            Action action = () => new Product(1, "Produto 01", "Produto 01 Description", 12, 45, null);
            action.Should()
                  .NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Product with empty Image value")]
        public void CreateProduct_EmptyImageValue_NoDomainException()
        {
            Action action = () => new Product(1, "Produto 01", "Produto 01 Description", 12, 45, "");
            action.Should()
                  .NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Product with null Image value")]
        public void CreateProduct_NullImageValue_NullReferenceException()
        {
            Action action = () => new Product(1, "Produto 01", "Produto 01 Description", 12, 45, null);
            action.Should()
                  .NotThrow<NullReferenceException>();
        }

    }
}
