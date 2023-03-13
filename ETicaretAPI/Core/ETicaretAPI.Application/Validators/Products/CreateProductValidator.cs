using ETicaretAPI.Application.ViewModels.Products;
using FluentValidation;

namespace ETicaretAPI.Application.Validators.Products
{
    public class CreateProductValidator : AbstractValidator<VM_Create_Product>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Can't accept empty name value!")
                .MaximumLength(150)
                .MinimumLength(3)
                    .WithMessage("Please enter a value that is between 3 and 150 char!");
            RuleFor(p => p.Stock)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Can't accept empty stock value!")
                .Must(s => s >= 0)
                    .WithMessage("Entered stock value can't be less than zero!");
            RuleFor(p => p.Price)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Can't accept empty price value!")
                .Must(s => s >= 0)
                    .WithMessage("Entered price value can't be less than zero!");
        }
    }
}
