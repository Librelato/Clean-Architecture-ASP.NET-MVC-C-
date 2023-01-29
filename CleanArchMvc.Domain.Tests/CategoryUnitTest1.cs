using CleanArchMvc.Domain.Entities;
using FluentAssertions;

namespace CleanArchMvc.Domain.Tests
{
    public class CategoryUnitTest1
    {
        [Fact(DisplayName = "Create Category with valid state")]
        public void CreateCategory_WithValidParameters_ResultObjectValidaState()
        {
            Action action = () => new Category(1, "Categoria 01");
            action.Should()
                .NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();

        }

        [Fact(DisplayName = "Create Category with negative Id value")]
        public void CreateCategory_NegativeIdValue_DomainExceptionInvalidId()
        {
            Action action = () => new Category(-1, "Categoria 01");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid Id value");

        }
        
        [Fact(DisplayName = "Create Category with short Name value")]
        public void CreateCategory_ShortNameValue_DomainExceptionShortName()
        {
            Action action = () => new Category(1, "Ca");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid name. Name too short, minimum 3 characters!");

        }

        [Fact(DisplayName = "Create Category with missing Name value")]
        public void CreateCategory_MissingNameValue_DomainExceptionRequiredName()
        {
            Action action = () => new Category(1, "");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid name. Name is required!");

        }

        [Fact(DisplayName = "Create Category with null Name value")]
        public void CreateCategory_NullNameValue_DomainExceptionInvalidName()
        {
            Action action = () => new Category(1, "");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
                //.WithMessage("Invalid name. Name is required!");

        }
    }
}