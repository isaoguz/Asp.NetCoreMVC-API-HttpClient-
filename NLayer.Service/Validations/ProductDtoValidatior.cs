using FluentValidation;
using NLayer.Core.Dtos;

namespace NLayer.Service.Validation
{
    public class ProductDtoValidatior : AbstractValidator<ProductDto>
    {
        public ProductDtoValidatior()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("{PropertyName} is required ").NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(x => x.Price).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater 0 ");
            RuleFor(x => x.Stock).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater 0 ");

            RuleFor(x => x.CategoryId).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
            //DropDownLissten çekiyoruz boş olamaz zaten.
            //RuleFor(x => x.CategoryId).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater 0 ");

        }
    }
}
