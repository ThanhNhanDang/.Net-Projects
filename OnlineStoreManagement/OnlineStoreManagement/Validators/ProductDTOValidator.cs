using FluentValidation;
using OnlineStoreManagement.DTOs;

namespace OnlineStoreManagement.Validators
{
    public class ProductDTOValidator : AbstractValidator<ProductDTO>
    {
        public ProductDTOValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Product name is required")
                .MaximumLength(100).WithMessage("Product name must not exceed 100 characters");

            RuleFor(p => p.Description)
                .MaximumLength(500).WithMessage("Product description must not exceed 500 characters");

            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0");

            RuleFor(p => p.StockQuantity)
                .GreaterThanOrEqualTo(0).WithMessage("Stock quantity must be greater than or equal to 0");

            RuleFor(p => p.Category)
                .MaximumLength(50).WithMessage("Category must not exceed 50 characters");
        }
    }
}