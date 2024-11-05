using Entities.DTOs;
using FluentValidation;
using FluentValidation.Validators;

namespace Entities.Validation
{
    public class FruitTypeDTOValidator : AbstractValidator<FruitTypeDTO>
    {
        public FruitTypeDTOValidator()
        {
            RuleFor(ft => ft.Name).NotEmpty().WithMessage(ValidationMessages.FruitTypeNameRequired);
            RuleFor(ft => ft.Description).NotEmpty().WithMessage(ValidationMessages.FruitTypeDescriptionRequired);
        }
    }
}
