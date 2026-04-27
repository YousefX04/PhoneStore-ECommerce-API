using FluentValidation;
using PhoneStore.Application.DTOs.Product;

namespace PhoneStore.Application.Validators.Product
{
    public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name is required")
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Product description is required")
                .MaximumLength(500);

            RuleFor(x => x.CPU)
                .NotEmpty().WithMessage("CPU information is required")
                .MaximumLength(50);

            RuleFor(x => x.RAM)
                .NotEmpty().WithMessage("RAM information is required")
                .MaximumLength(20);

            RuleFor(x => x.Storage)
                .NotEmpty().WithMessage("Storage information is required")
                .MaximumLength(50);

            RuleFor(x => x.Battery)
                .NotEmpty().WithMessage("Battery information is required")
                .MaximumLength(50);

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0");

            RuleFor(x => x.StockQuantity)
                .GreaterThanOrEqualTo(0).WithMessage("Stock can't be negative");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Category is required");
        }
    }
}
