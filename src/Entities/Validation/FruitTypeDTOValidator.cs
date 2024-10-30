using Entities.DTOs;
using FluentValidation;

namespace Entities.Validation
{
    public class FruitTypeDTOValidator : AbstractValidator<FruitTypeDTO>
    {
        public FruitTypeDTOValidator()
        {
            RuleFor(ft => ft.Name).NotEmpty().WithMessage("Fruit type name is required.");
            RuleFor(ft => ft.Description).NotEmpty().WithMessage("Fruit type description is required.");
        }
    }
}
