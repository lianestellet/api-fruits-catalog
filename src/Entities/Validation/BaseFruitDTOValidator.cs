using Entities.DTOs;
using FluentValidation;

namespace Entities.Validation
{
    public abstract class BaseFruitDTOValidator<T> : AbstractValidator<T> where T : BaseFruitDTO
    {
        protected BaseFruitDTOValidator()
        {
            RuleFor(f => f.Name).NotEmpty().WithMessage(ValidationMessages.FruitNameRequired);
            RuleFor(f => f.Description).NotEmpty().WithMessage(ValidationMessages.FruitDescriptionRequired);
            RuleFor(f => f.Description).MinimumLength(10).WithMessage(ValidationMessages.FruitDescriptionMinLength)
                .Unless(f => string.IsNullOrEmpty(f.Description));
            RuleFor(f => f.FruitTypeId).NotEmpty().WithMessage(ValidationMessages.FruitTypeIdRequired);
        }
    }
}
